using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL;
using Microsoft.AspNetCore.Http;
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

        public async Task TryAddMovie(Movie movie, string path, IFormFile Upload)
        {
            string Guidstring = Guid.NewGuid().ToString();
            string UploadName = Path.Combine(path, Guidstring + Upload.FileName);


            // If tthe file doesnt exist, add to both the database and folder
            using (var fileStream = new FileStream(UploadName, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            movie.MovieImagePath = Guidstring + Upload.FileName;
            await AddMovie(movie);   
        }
        private async Task AddMovie(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }
    }
}
