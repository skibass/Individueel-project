using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL;
using Models;

namespace BLL
{
    
    public class Movie_Service
    {
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
    }
}
