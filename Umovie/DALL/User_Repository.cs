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
        UmovieContext context = new UmovieContext();

        public List<User> GetUsers()
        {
            List<User> users = context.Users.Include(e => e.Role).Include(e => e.MovieRatings).ThenInclude(e => e.Movie).Include(e => e.UserFavoriteMovies).ThenInclude(e => e.Movie).ToList();

            if (users == null)
            {
                users = new List<User>();
            }
            return users;
        }

        public void AddUserAndLogin(User userToBeAdded)
        {
            User user = new User();

            List<Role> roles = context.Roles.ToList();

            user.UserName = "thijn";
            user.UserEmail = "titititi";
            user.Role = roles[0];

            context.Users.Add(userToBeAdded);
            context.SaveChanges();
        }
    }
}
