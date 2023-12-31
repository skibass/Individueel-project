﻿using Microsoft.EntityFrameworkCore;
using Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
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

        public User GetCurrentUser(int uId)
        {
            User user = context.Users.Include(e => e.Role).Where(r => r.UserId == uId).SingleOrDefault();

            if (user == null)
            {
                //user = new User();
            }
            return user;
        }

        public User AddUserAndLogin(User userToBeAdded)
        {
            User findIfUserExists = context.Users.Where(r => r.UserEmail == userToBeAdded.UserEmail).SingleOrDefault();

            if (findIfUserExists == null)
            {
                List<Role> roles = context.Roles.ToList();

                userToBeAdded.UserPassword = BCrypt.Net.BCrypt.HashPassword(userToBeAdded.UserPassword);
                userToBeAdded.Role = roles[0];

                context.Users.Add(userToBeAdded);
                context.SaveChanges();

                return userToBeAdded;
            }
            return null;
        }
        public User VerifyUser(User user)
        {
            var userR = context.Users.Include(e => e.Role).Where(r => r.UserName == user.UserName).SingleOrDefault(x => x.UserEmail == user.UserEmail);

            if (userR != null && BCrypt.Net.BCrypt.Verify(user.UserPassword, userR.UserPassword))
            {
                return userR;
            }
            return null;
        }
    }
}
