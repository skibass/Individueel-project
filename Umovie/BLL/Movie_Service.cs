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
        public List<UserFavoriteMovie> TryGetFavoriteMovies(int id)
        {
            return repository.GetFavoriteMovies(id);
        }

        public double? TryGetAverageRating(Movie? movie)
        {
            return repository.GetAverageRating(movie);
        }

        public double? TryGetAmountOfFavorites(Movie? movie)
        {
            return repository.GetAmountOfFavorites(movie);
        }

        public string? TryGetCategories(Movie? movie)
        {
            return repository.GetCategories(movie);
        }

        public bool? TryFavoriteMovie(int movieId, int userId)
        {
            return repository.FavoriteMovie(movieId, userId);
        }
    }
}
