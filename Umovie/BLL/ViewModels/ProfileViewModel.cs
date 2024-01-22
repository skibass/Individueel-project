using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ProfileViewModel
    {
        Movie_Service movieService = new();

        public User User { get; set; }
        public Movie Movie { get; set; }
        public MovieRating Rating { get; set; }
        public List<UserFavoriteMovie> FavoriteMovies { get; set; }

        public double? GetAverageRating(int movieId)
        {
            // Implement the logic for getting the average rating for a movie
            return movieService.TryGetAverageRating(movieId);
        }

        public string? GetCategories(int movieId)
        {
            // Implement the logic for getting the categories for a movie
            return movieService.TryGetCategories(movieId);
        }

        public double? GetUserRating(int movieId, int userId)
        {
            // Implement the logic for getting the categories for a movie
            return movieService.TryGetUserRating(movieId, userId);
        }
    }
}
