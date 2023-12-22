using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace BLL
{

    public class Movie_Service
    {
        UmovieContext context = new UmovieContext();
        Movie_Repository repository = new();
        public List<Movie> TryGetMovies()
        {
            return repository.GetMovies();
        }
        public Movie TryGetMovie(int movieId)
        {
            return repository.GetMovie(movieId);
        }
        public List<UserFavoriteMovie> TryGetFavoriteMovies(int id)
        {
            return repository.GetFavoriteMovies(id);
        }

        public double? TryGetAverageRating(int movieId)
        {
            return repository.GetAverageRating(movieId);
        }

        public double? TryGetAmountOfFavorites(int movieId)
        {
            return repository.GetAmountOfFavorites(movieId);
        }

        public string? TryGetCategories(int movieId)
        {
            return repository.GetCategories(movieId);
        }

        public bool? TryFavoriteMovie(int movieId, int userId)
        {
            return repository.FavoriteMovie(movieId, userId);
        }
        public bool? TryRateMovie(int movieId, int userId, int rating)
        {
            return repository.RateMovie(movieId, userId, rating);
        }
        public int? TryGetUserRating(int movieId, int userId)
        {
            return repository.GetUserRating(movieId, userId);
        }
        public List<MovieRating> TryGetUserRatedMovies(int userId)
        {
            return repository.GetUserRatedMovies(userId);
        }
        public List<Category> TryGetCategories()
        {
            List<Category> categories = context.Categories.ToList();
            return categories;
        }

        public async Task TryAddMovie(Movie movie, List<string> chosenCategories, string path, IFormFile Upload)
        {
            foreach (string category in chosenCategories)
            {
                MovieCategory movieCat = new();
                movieCat.MovieId = movie.MovieId;
                movieCat.CategorieId = int.Parse(category);
                movie.MovieCategories.Add(movieCat);
            }

            string Guidstring = Guid.NewGuid().ToString();
            string UploadName = Path.Combine(path, Guidstring + Upload.FileName);

            using (var fileStream = new FileStream(UploadName, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            movie.MovieImagePath = Guidstring + Upload.FileName;
            await AddMovie(movie);
        }
        public async Task TryEditMovie(Movie movie, List<string> chosenCategories, string path, IFormFile Upload)
        {
            Movie movie1 = context.Movies.Include(e => e.MovieCategories).Where(e => e.MovieId == movie.MovieId).FirstOrDefault();

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
            context.SaveChanges();
        }
        public void DeleteMovie(int movieId)
        {
            var movie = context.Movies.Where(e => e.MovieId == movieId).FirstOrDefault();

            context.Entry(movie).State = EntityState.Deleted;

            context.SaveChanges();
        }

        private async Task AddMovie(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }
    }
}
