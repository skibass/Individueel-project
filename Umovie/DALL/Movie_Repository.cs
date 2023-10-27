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
        public List<Movie> GetMovies()
        {
            UmovieContext context = new UmovieContext();

            List<Movie> movies = context.Movies.Include(e => e.MovieRatings).ToList();

            if (movies == null)
            {
                movies = new List<Movie>();
            }
            return movies;
        }
    }
}
