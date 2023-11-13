﻿using Models;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DALL
{
    public class Movie_Repository
    {
        string connectionString = "Server=127.0.0.1;Database=umovie;Uid=root;Pwd=;";

        public List<Movie> GetMovies()
        {

            List<Movie> movies = new List<Movie>();


            movies.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
                                        SELECT movies.movie_id, movies.movie_name, movies.movie_description, movies.movie_director, movies.movie_director, movies.movie_release_date, movies.movie_language,
       categories.name AS CategoryName, AVG(movie_ratings.rating_number) AS AverageRating, COUNT(users.user_name) AS UserFavorites
FROM movies
JOIN movie_categories ON movies.movie_id = movie_categories.movie_id
JOIN categories ON movie_categories.id = categories.id
JOIN movie_ratings ON movies.movie_id = movie_ratings.movie_id
JOIN users ON movie_ratings.user_id = users.user_id
GROUP BY movie_id, categories.id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int movieId = Convert.ToInt32(reader["movie_id"]);

                        Movie movie = movies.Find(m => m.MovieId == movieId);

                        if (movie == null)
                        {
                            movie = new Movie
                            {
                                MovieId = movieId,
                                MovieName = reader["movie_name"].ToString(),
                                MovieDescription = reader["movie_description"].ToString(),
                                MovieDirector = reader["movie_director"].ToString(),
                                MovieLanguage = reader["movie_language"].ToString(),
                                MovieReleaseDate = reader["movie_release_date"].ToString(),
                                MovieAgeRating = Convert.ToInt32(0),
                                MovieCategories = new List<MovieCategory>(),
                                MovieRatings = new List<MovieRating>(),
                                UserFavoriteMovies = new List<UserFavoriteMovie>()

                            };

                            movies.Add(movie);
                        }
                        Category category = new();

                        var categoryMovie = new Category
                        {
                            Name = reader["CategoryName"].ToString()
                        };

                        var categoryM = new MovieCategory
                        {
                            Categorie = categoryMovie,
                        };

                        movie.MovieCategories.Add(categoryM);

                        movie.MovieAverageRating = Convert.ToInt32(reader["AverageRating"]);

                        movie.UserFavorites = Convert.ToInt32(reader["UserFavorites"]);
                    }

                    reader.Close();
                }
            }
            return movies;
        }




        public void DeleteRating(User userToBeAdded)
        {

        }

        //public void DeleteAsFavorite(Movie movie, User user)
        //{
        //    var movieById = context.UserFavoriteMovies.Where(x => x.UserId == user.UserId).Where(x => x.MovieId == movie.MovieId).FirstOrDefault();

        //    context.Entry(movieById).State = EntityState.Deleted;

        //    context.SaveChanges();
        //}

        public double? GetAverageRating(Movie? movie)
        {
            double? rating = 0;

            foreach (var item in GetMovies())
            {          
                    if (item.MovieId == movie.MovieId)
                    {
                        rating = item.MovieAverageRating;
                    }              
            }

            return rating;
        }

        public int? GetAmountOfFavorites(Movie? movie)
        {
            int users = 0;
            foreach (var item in GetMovies())
            {
                if (item.MovieId == movie.MovieId)
                {
                        users = (int)item.UserFavorites;
                }
            }

            return users;
        }

        public string? GetCategories(Movie? movie)
        {
            string categories = "";

            foreach (var item in GetMovies())
            {
                if (item.MovieId == movie.MovieId)
                {
                    foreach (MovieCategory cat in item.MovieCategories)
                    {
                        categories += " | " + cat.Categorie.Name;
                    }
                }
            }
            return categories;
        }
    }
}
