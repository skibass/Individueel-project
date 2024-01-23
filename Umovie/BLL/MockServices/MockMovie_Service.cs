using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DALL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace BLL
{

    public class MockMovie_Service
    {
        Movie_Repository repository = new();

        public readonly UmovieContext _context;

        public MockMovie_Service()
        {          
        }
        public MockMovie_Service(UmovieContext context)
        {
            _context = context;
        }
        public List<Movie> TryGetMovies()
        {
            List<Movie> movies = _context.Movies.Include(e => e.MovieRatings).Include(e => e.UserFavoriteMovies).Include(e => e.MovieCategories).ThenInclude(e => e.Categorie).ToList();

            if (movies == null)
            {
                movies = new List<Movie>();
            }
            return movies;
        }
        //public Movie TryGetMovie(int movieId)
        //{
        //    return repository.GetMovie(movieId);
        //}
        public List<UserFavoriteMovie> TryGetFavoriteMovies(int id)
        {
            List<UserFavoriteMovie> fMovies = _context.UserFavoriteMovies.Include(e => e.Movie).ThenInclude(e => e.MovieCategories).ThenInclude(e => e.Categorie).Where(x => x.UserId == id).ToList();

            if (fMovies == null)
            {
                fMovies = new List<UserFavoriteMovie>();
            }
            return fMovies;
        }

        public double? TryGetAverageRating(int movieId)
        {
            double? avarageRating = _context.MovieRatings.Where(r => r.MovieId == movieId).Average(r => r.RatingNumber);

            return avarageRating;
        }

        public double? TryGetAmountOfFavorites(int movieId)
        {
            double? amountOfFavorites = _context.UserFavoriteMovies.Where(r => r.MovieId == movieId).Count();

            if (amountOfFavorites != null)
            {
                return amountOfFavorites;
            }
            return null;
        }

        //public string? TryGetCategories(int movieId)
        //{
        //    return repository.GetCategories(movieId);
        //}

        public bool? TryFavoriteMovie(int movieId, int userId)
        {
            bool favorite = false;

            UserFavoriteMovie uF = new();

            var favoriteMovie = _context.UserFavoriteMovies.Where(r => r.MovieId == movieId).Where(r => r.UserId == userId).SingleOrDefault();

            // favorite
            if (favoriteMovie == null)
            {
                uF.MovieId = movieId;
                uF.UserId = userId;

                _context.UserFavoriteMovies.Add(uF);

                _context.SaveChanges();

                favorite = true;
            }
            // unfavorite
            else
            {
                _context.Entry(favoriteMovie).State = EntityState.Deleted;

                _context.SaveChanges();

                favorite = false;
            }

            return favorite;
        }
        public bool? TryRateMovie(int movieId, int userId, int rating)
        {
            MovieRating uMR = new();

            var movieRating = _context.MovieRatings.Where(r => r.MovieId == movieId).Where(r => r.UserId == userId).SingleOrDefault();

            // if movie has already been rated
            if (movieRating != null)
            {
                _context.Entry(movieRating).State = EntityState.Deleted;

                _context.SaveChanges();
            }

            uMR.MovieId = movieId;
            uMR.UserId = userId;
            uMR.RatingNumber = rating;

            _context.MovieRatings.Add(uMR);

            _context.SaveChanges();

            return true;
        }
        public int? TryGetUserRating(int movieId, int userId)
        {
            var movieRating = _context.MovieRatings.Where(r => r.MovieId == movieId).Where(r => r.UserId == userId).SingleOrDefault();

            if (movieRating != null)
            {
                return movieRating.RatingNumber;
            }
            return null;
        }
        public List<MovieRating> TryGetUserRatedMovies(int userId)
        {
            List<MovieRating> movies = _context.MovieRatings.Where(r => r.UserId == userId).Include(e => e.Movie).ToList();

            if (movies == null)
            {
                movies = new List<MovieRating>();
            }
            return movies;
        }
        public List<Category> TryGetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public async Task TryAddMovie(Movie movie, List<string?> chosenCategories, string path, IFormFile Upload)
        {
            if (chosenCategories != null)
            {
                foreach (string category in chosenCategories)
                {
                    MovieCategory movieCat = new();
                    movieCat.MovieId = movie.MovieId;
                    movieCat.CategorieId = int.Parse(category);
                    movie.MovieCategories.Add(movieCat);
                }
            }

            if (Upload != null)
            {
                string Guidstring = Guid.NewGuid().ToString();
                string UploadName = Path.Combine(path, Guidstring + Upload.FileName);

                using (var fileStream = new FileStream(UploadName, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                movie.MovieImagePath = Guidstring + Upload.FileName;
            }

            await AddMovie(movie);
        }
        public async Task TryEditMovie(Movie movie, List<string> chosenCategories, string path, IFormFile Upload)
        {
            Movie movie1 = _context.Movies.Include(e => e.MovieCategories).Where(e => e.MovieId == movie.MovieId).FirstOrDefault();

            movie1.MovieName = movie.MovieName;
            movie1.MovieDirector = movie.MovieDirector;
            movie1.MovieAgeRating = movie.MovieAgeRating;
            movie1.MovieLanguage = movie.MovieLanguage;
            movie1.MovieDescription = movie.MovieDescription;
            movie1.MovieReleaseDate = movie.MovieReleaseDate;

            if (Upload != null)
            {
                string Guidstring = Guid.NewGuid().ToString();
                string UploadName = Path.Combine(path, Guidstring + Upload.FileName);

                using (var fileStream = new FileStream(UploadName, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                movie1.MovieImagePath = Guidstring + Upload.FileName;
            }

            if (chosenCategories != null)
            {
                foreach (string category in chosenCategories)
                {
                    if (!movie1.MovieCategories.Any(mc => mc.CategorieId == int.Parse(category)))
                    {
                        MovieCategory movieCat = new MovieCategory();
                        movieCat.MovieId = movie1.MovieId;
                        movieCat.CategorieId = int.Parse(category);
                        movie1.MovieCategories.Add(movieCat);
                    }
                }
            }

            _context.SaveChanges();
        }
        public void DeleteMovie(int movieId)
        {
            var movie = _context.Movies.Where(e => e.MovieId == movieId).FirstOrDefault();

            _context.Entry(movie).State = EntityState.Deleted;

            _context.SaveChanges();
        }

        private async Task AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
    }
}
