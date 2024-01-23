using DALL;
using Microsoft.EntityFrameworkCore;
using Models;
using BLL;
using System.Transactions;

namespace UMovieTests.DBTests
{
    [TestClass]
    public class MovieTests
    {
        Random random = new();
        private MockMovie_Service CreateMovieServiceWithMockContext()
        {
            var options = new DbContextOptionsBuilder<MockUmovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid().ToString())
                .Options;

            var mockContext = new MockUmovieContext(options);

            return new MockMovie_Service(mockContext);
        }       


        [TestMethod]
        public void ShouldReturnAllMovies()
        {
            using (var scope = new TransactionScope())
            {
                // Arrange
                var service = CreateMovieServiceWithMockContext();

                for (int i = 1; i <= 5; i++)
                {
                    var newMovie = new Movie
                    {
                        MovieId = random.Next(1, 9999), // Assuming MovieId needs to be unique
                        MovieName = $"Test Movie {i}",
                        MovieDescription = $"Description {i}",
                        // Add other properties as needed
                    };

                    service.TryAddMovie(newMovie, null, null, null);
                }

                // Act
                var result = service.TryGetMovies();

                // Assert
                Assert.IsTrue(result.Count() == 5);
            }
        }

        [TestMethod]
        public void AShouldAddMovieToDatabase()
        {
            using (var scope = new TransactionScope())
            {
                // Arrange
                var service = CreateMovieServiceWithMockContext();

                var oldMovies = service.TryGetMovies();

                // Act
                var newMovie = new Movie
                {
                    MovieId = random.Next(1, 9999),
                    MovieName = "Test Movie",
                    MovieDescription = "Test Movie",
                };

                var result = service.TryAddMovie(newMovie, null, null, null);

                var newMovies = service.TryGetMovies();

                // Assert

                Assert.IsTrue(newMovies.Count == oldMovies.Count + 1);
                Assert.IsTrue(newMovies.Any(movie => movie.MovieName == "Test Movie"));
            }
        }

        [TestMethod]
        public void ShouldDeleteMovie()
        {
            using (var scope = new TransactionScope())
            {
                // Arrange
                var service = CreateMovieServiceWithMockContext();

                // Act
                var newMovie = new Movie
                {
                    MovieId = random.Next(1, 9999),
                    MovieName = "Test Movie",
                    MovieDescription = "Test Movie",
                };

                service.TryAddMovie(newMovie, null, null, null);
                var retrievedMovie = service._context.Movies.FirstOrDefault(m => m.MovieId == newMovie.MovieId);

                Assert.IsNotNull(retrievedMovie);

                service.DeleteMovie(newMovie.MovieId);
                var deletedMovie = service._context.Movies.FirstOrDefault(m => m.MovieId == newMovie.MovieId);

                // Assert
                Assert.IsNull(deletedMovie);
            }
        }

        [TestMethod]
        public void ShouldEditMovie()
        {
            using (var scope = new TransactionScope())
            {
                // Arrange
                var service = CreateMovieServiceWithMockContext();

                // Act
                var newMovie = new Movie
                {
                    MovieId = random.Next(1, 9999),
                    MovieName = "Test Movie",
                    MovieDescription = "Test Movie",
                };

                service.TryAddMovie(newMovie, null, null, null);

                var retrievedMovie = service._context.Movies.FirstOrDefault(m => m.MovieId == newMovie.MovieId);

                string oldName = retrievedMovie.MovieName;

                newMovie.MovieName = "Edited name";

                service.TryEditMovie(newMovie, null, null, null);

                // Assert
                Assert.IsFalse(retrievedMovie.MovieName == oldName);
            }
        }

        //[TestMethod]
        //public void CanGetMovies()
        //{
        //    UmovieContext context = new UmovieContext();

        //    List<Movie> movies = context.Movies.Include(e => e.MovieRatings).Include(e => e.UserFavoriteMovies).Include(e => e.MovieCategories).ThenInclude(e => e.Categorie).ToList();

        //    if (movies == null)
        //    {
        //        movies = new List<Movie>();
        //    }
        //    Console.WriteLine($"Amount of retrieved movies {movies.Count}");
        //    Assert.IsTrue(movies != null);
        //}

        //[TestMethod]
        //public void CanAddMovie()
        //{
        //    // Arrange
        //    UmovieContext context = new UmovieContext();

        //    List<Category> categories = context.Categories.ToList();

        //    Movie movie = new Movie();
        //    movie.MovieName = "Test";
        //    movie.MovieAgeRating = 10;
        //    movie.MovieDirector = "test";
        //    movie.MovieLanguage = "en";
        //    movie.MovieReleaseDate = "2014/09/1";
        //    movie.MovieDescription = "test";

        //    foreach (Category category in categories)
        //    {
        //        MovieCategory movieCat = new();
        //        movieCat.MovieId = movie.MovieId;
        //        movieCat.CategorieId = category.Id;
        //        movie.MovieCategories.Add(movieCat);
        //    }

        //    // Act
        //    int previousCountMovies = context.Movies.Count();

        //    context.Movies.Add(movie);
        //    context.SaveChanges();

        //    int newCount = context.Movies.Count();

        //    Console.WriteLine($"Previous amount of movies: {previousCountMovies} | Amount after adding: {newCount}");

        //    // Assert
        //    Assert.IsTrue(previousCountMovies < newCount);
        //}

        //[TestMethod]
        //public void CanEditMovie()
        //{
        //    // Arrange
        //    UmovieContext context = new UmovieContext();

        //    List<Movie> movies = new();
        //    List<Category> categories = context.Categories.ToList();

        //    Movie movie = new Movie();
        //    movie.MovieName = "Test";
        //    movie.MovieAgeRating = 10;
        //    movie.MovieDirector = "test";
        //    movie.MovieLanguage = "en";
        //    movie.MovieReleaseDate = "2014/09/1";
        //    movie.MovieId = 1;

        //    foreach (Category category in categories)
        //    {
        //        MovieCategory movieCat = new();
        //        movieCat.MovieId = movie.MovieId;
        //        movieCat.CategorieId = category.Id;
        //        movie.MovieCategories.Add(movieCat);
        //    }

        //    movies.Add(movie);

        //    // Act
        //    var oldMovie = new Movie
        //    {
        //        MovieName = movie.MovieName,
        //        MovieAgeRating = movie.MovieAgeRating,
        //        MovieDirector = movie.MovieDirector,
        //        MovieLanguage = movie.MovieLanguage,
        //        MovieReleaseDate = movie.MovieReleaseDate,
        //    };

        //    foreach (Movie item in movies)
        //    {
        //        if (item.MovieId == 1)
        //        {
        //            movie.MovieName = "testEdit";
        //            movie.MovieDirector = "testEdit";
        //            movie.MovieAgeRating = 5;
        //            movie.MovieLanguage = "testEdit";
        //            movie.MovieDescription = "testEdit";
        //            movie.MovieReleaseDate = "2014/09/1";
        //        }
        //    }
        //    var newMovie = movie;

        //    Console.WriteLine($" Previous movie name: {oldMovie.MovieName} | New movie name: {newMovie.MovieName}");

        //    // Assert
        //    Assert.IsTrue(oldMovie != newMovie);
        //}

        //[TestMethod]
        //public void CanDeleteMovie()
        //{
        //    UmovieContext context = new UmovieContext();

        //    List<Movie> movies = new();
        //    List<Category> categories = context.Categories.ToList();

        //    Movie movie = new Movie();
        //    movie.MovieName = "Test";
        //    movie.MovieAgeRating = 10;
        //    movie.MovieDirector = "test";
        //    movie.MovieLanguage = "en";
        //    movie.MovieReleaseDate = "2014/09/1";
        //    movie.MovieId = 1;

        //    foreach (Category category in categories)
        //    {
        //        MovieCategory movieCat = new();
        //        movieCat.MovieId = movie.MovieId;
        //        movieCat.CategorieId = category.Id;
        //        movie.MovieCategories.Add(movieCat);
        //    }

        //    movies.Add(movie);
        //    int oldMovieCount = movies.Count();

        //    foreach (var item in movies)
        //    {
        //        if (item.MovieId == 1)
        //        {
        //            movies.Remove(item);
        //            break;
        //        }
        //    }
        //    int newMovieCount = movies.Count();

        //    Console.WriteLine($"Amount of movies previously: {oldMovieCount} | Amount after deleting: {newMovieCount}");

        //    Assert.IsTrue(newMovieCount == 0);
        //}


    }
}