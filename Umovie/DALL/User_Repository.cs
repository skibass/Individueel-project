using Microsoft.EntityFrameworkCore;
using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL
{
    public class User_Repository
    {
        public List<User> GetUsers()
        {
            UmovieContext context = new UmovieContext();

            List<User> users = context.Users.Include(e => e.Role).Include(e => e.MovieRatings).ThenInclude(e => e.Movie).Include(e => e.UserFavoriteMovies).ThenInclude(e => e.Movie).ToList();

            if (users == null)
            {
                users = new List<User>();
            }
            return users;
        }
    }
}
