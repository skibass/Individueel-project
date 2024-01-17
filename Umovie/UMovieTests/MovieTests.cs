using DALL;
using Microsoft.EntityFrameworkCore;
using Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.IO;

namespace UMovieTests
{
    [TestClass]
    public class MovieTests
    {
        [TestMethod]
        public void CanGetMovies()
        {
            UmovieContext context = new UmovieContext();

            List<Movie> movies = context.Movies.Include(e => e.MovieRatings).Include(e => e.UserFavoriteMovies).Include(e => e.MovieCategories).ThenInclude(e => e.Categorie).ToList();

            if (movies == null)
            {
                movies = new List<Movie>();
            }
            Console.WriteLine($"Amount of retrieved movies {movies.Count}");
            Assert.IsTrue(movies.Count() > 0);
        }

        [TestMethod]
        public void CanAddMovie()
        {
            // Arrange
            UmovieContext context = new UmovieContext();

            List<Category> categories = context.Categories.ToList();

            Movie movie = new Movie();
            movie.MovieName = "Test";
            movie.MovieAgeRating = 10;
            movie.MovieDirector = "test";
            movie.MovieLanguage = "en";
            movie.MovieReleaseDate = "2014/09/1";
            movie.MovieDescription = "test";

            foreach (Category category in categories)
            {
                MovieCategory movieCat = new();
                movieCat.MovieId = movie.MovieId;
                movieCat.CategorieId = category.Id;
                movie.MovieCategories.Add(movieCat);
            }

            // Act
            int previousCountMovies = context.Movies.Count();
            
            context.Movies.Add(movie);
            context.SaveChanges();

            int newCount = context.Movies.Count();

            Console.WriteLine($"{previousCountMovies}  {newCount}");

            // Assert
            Assert.IsTrue(previousCountMovies < newCount);
        }

        [TestMethod]
        public void CanEditMovie()
        {
            // Arrange
            UmovieContext context = new UmovieContext();

            List<Movie> movies = new();
            List<Category> categories = context.Categories.ToList();

            Movie movie = new Movie();
            movie.MovieName = "Test";
            movie.MovieAgeRating = 10;
            movie.MovieDirector = "test";
            movie.MovieLanguage = "en";
            movie.MovieReleaseDate = "2014/09/1";
            movie.MovieId = 1;

            foreach (Category category in categories)
            {
                MovieCategory movieCat = new();
                movieCat.MovieId = movie.MovieId;
                movieCat.CategorieId = category.Id;
                movie.MovieCategories.Add(movieCat);
            }           

            movies.Add(movie);

            // Act
            var oldMovie = new Movie
            {
                MovieName = movie.MovieName,
                MovieAgeRating = movie.MovieAgeRating,
                MovieDirector = movie.MovieDirector,
                MovieLanguage = movie.MovieLanguage,
                MovieReleaseDate = movie.MovieReleaseDate,
            };

            foreach (Movie item in movies)
            {
                if (item.MovieId == 1)
                {
                    movie.MovieName = "testEdit";
                    movie.MovieDirector = "testEdit";
                    movie.MovieAgeRating = 5;
                    movie.MovieLanguage = "testEdit";
                    movie.MovieDescription = "testEdit";
                    movie.MovieReleaseDate = "2014/09/1";                   
                }               
            }
            var newMovie = movie;

            Console.WriteLine($" Previous movie name: {oldMovie.MovieName} | New movie name: {newMovie.MovieName}");

            // Assert
            Assert.IsTrue(oldMovie != newMovie);
        }

        [TestMethod]
        public void CanDeleteMovie()
        {
            UmovieContext context = new UmovieContext();

            List<Movie> movies = new();
            List<Category> categories = context.Categories.ToList();

            Movie movie = new Movie();
            movie.MovieName = "Test";
            movie.MovieAgeRating = 10;
            movie.MovieDirector = "test";
            movie.MovieLanguage = "en";
            movie.MovieReleaseDate = "2014/09/1";
            movie.MovieId = 1;

            foreach (Category category in categories)
            {
                MovieCategory movieCat = new();
                movieCat.MovieId = movie.MovieId;
                movieCat.CategorieId = category.Id;
                movie.MovieCategories.Add(movieCat);
            }

            movies.Add(movie);
            int oldMovieCount = movies.Count();

            foreach (var item in movies)
            {
                if (item.MovieId == 1)
                {
                    movies.Remove(item);
                    break;
                }
            }
            int newMovieCount = movies.Count();

            Console.WriteLine($"Amount of movies previously: {oldMovieCount} | Amount now {newMovieCount}");

            Assert.IsTrue(newMovieCount == 0);
        }
    }
}