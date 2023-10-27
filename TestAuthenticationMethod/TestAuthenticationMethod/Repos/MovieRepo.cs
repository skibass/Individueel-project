using Microsoft.EntityFrameworkCore;
using TestAuthenticationMethod.umovie;

namespace TestAuthenticationMethod.Repos
{
    public class MovieRepo
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
