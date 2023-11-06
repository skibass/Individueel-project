using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DALL
{
    public class Movie_Repository
    {
        UmovieContext context = new UmovieContext();

        public List<Movie> GetMovies()
        {
            List<Movie> movies = context.Movies.Include(e => e.MovieRatings).Include(e => e.UserFavoriteMovies).Include(e => e.MovieCategories).ThenInclude(e => e.Categorie).ToList();

            if (movies == null)
            {
                movies = new List<Movie>();
            }
            return movies;
        }

        public void DeleteRating(User userToBeAdded)
        {
           
        }

        public void DeleteAsFavorite(Movie movie, User user)
        {
            var movieById = context.UserFavoriteMovies.Where(x => x.UserId == user.UserId).Where(x => x.MovieId == movie.MovieId).FirstOrDefault();

            context.Entry(movieById).State = EntityState.Deleted;

            context.SaveChanges();
        }

        public double? GetAverageRating(Movie? movie)
        {
            double? avarageRating = context.MovieRatings.Where(r => r.MovieId == movie.MovieId).Average(r => r.RatingNumber);

            return avarageRating;
        }

        public double? GetAmountOfFavorites(Movie? movie)
        {
            double? amountOfFavorites = context.UserFavoriteMovies.Where(r => r.MovieId == movie.MovieId).Count();

            return amountOfFavorites;
        }

        public string? GetCategories(Movie? movie)
        {
            var movieCategories = context.MovieCategories.Where(r => r.MovieId == movie.MovieId);
            string categories = "";

            foreach (var item in movieCategories)
            {
                categories += " | " + item.Categorie.Name;
            }
            return categories;
        }
    }
}
