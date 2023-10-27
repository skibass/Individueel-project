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
            List<Movie> movies = context.Movies.Include(e => e.MovieRatings).ToList();

            if (movies == null)
            {
                movies = new List<Movie>();
            }
            return movies;
        }

        public void DeleteRating(User userToBeAdded)
        {
            User user = new User();

            List<Role> roles = context.Roles.ToList();

            user.UserName = "thijn";
            user.UserEmail = "titititi";
            user.Role = roles[0];

            context.Users.Add(userToBeAdded);
            context.SaveChanges();
        }

        public void DeleteAsFavorite(int toBeDeletedFavorite)
        {
            var studentbyid = context.Users.Where(x => x.UserId == toBeDeletedFavorite).FirstOrDefault();

                context.Entry(studentbyid).State = EntityState.Deleted;

                context.SaveChanges();
        }
    }
}
