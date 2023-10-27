using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL
{
    public class Movie_Repository
    {
        MySqlConnection db = new MySqlConnection("Server=127.0.0.1;Database=umovie;Uid=root;Pwd=;");

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();

            db.Open();

            MySqlCommand accountQuery = new MySqlCommand("SELECT * from movies", db);

            accountQuery.ExecuteNonQuery();

            MySqlDataReader readAccounts = accountQuery.ExecuteReader();

            while (readAccounts.Read())
            {
                Movie movie = new Movie();

                movie.Name = (string)readAccounts["movie_name"];
                movie.Description = (string)readAccounts["movie_description"];
                movie.Director = (string)readAccounts["movie_director"];
                movie.Release_Date = (string)readAccounts["movie_release_date"];
                movie.Language = (string)readAccounts["movie_language"];

                movies.Add(movie);
            }
            db.Close();

            return movies;
        }
    }
}
