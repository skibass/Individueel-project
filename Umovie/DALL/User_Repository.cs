﻿using Microsoft.EntityFrameworkCore;
using Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public bool AddUserAndLogin(User userToBeAdded)
        {
            List<Role> roles = context.Roles.ToList();

            userToBeAdded.Role = roles[0];
            userToBeAdded.UserPassword = BCrypt.Net.BCrypt.HashPassword(userToBeAdded.UserPassword);
            context.Users.Add(userToBeAdded);
            context.SaveChanges();

            return true;
        }
        public User VerifyUser(User user)
        {
            var userR = context.Users.SingleOrDefault(x => x.UserEmail == user.UserEmail);

            if (userR != null) 
            {
                if (userR == null || !BCrypt.Net.BCrypt.Verify(user.UserPassword, userR.UserPassword))
                {
                    return userR;
                }
            }
            return null;
        }
    }
}
