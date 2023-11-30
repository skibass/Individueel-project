using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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

        public List<UserFavoriteMovie> GetFavoriteMovies(int uId)
        {
            List<UserFavoriteMovie> fMovies = context.UserFavoriteMovies.Include(e => e.Movie).ThenInclude(e => e.MovieCategories).ThenInclude(e => e.Categorie).Where(x => x.UserId == uId).ToList();
            
            if (fMovies == null)
            {
                fMovies = new List<UserFavoriteMovie>();
            }
            return fMovies;
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

        public bool? FavoriteMovie(int movieId, int userId)
        {
            bool favorite = false;

            UserFavoriteMovie uF = new();

            var favoriteMovie = context.UserFavoriteMovies.Where(r => r.MovieId == movieId).Where(r => r.UserId == userId).SingleOrDefault();

            // favorite
            if (favoriteMovie == null)
            {
                uF.MovieId = movieId;
                uF.UserId = userId;

                context.UserFavoriteMovies.Add(uF);

                context.SaveChanges();

                 favorite = true;
            }
            // unfavorite
            else
            {
                context.Entry(favoriteMovie).State = EntityState.Deleted;

                context.SaveChanges();

                 favorite = false;
            }

            return favorite;
        }
    }
}
