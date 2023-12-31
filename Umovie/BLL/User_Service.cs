﻿using Models;
using DALL;

namespace BLL
{
    public class User_Service
    {
        User_Repository userRepository = new User_Repository();
        
        public List<User> TryGetUsers()
        {
            return userRepository.GetUsers();
        }
        public User TryGetCurrentUser(int id)
        {
            return userRepository.GetCurrentUser(id);
        }
        public User TryRegisterUser(User user)
        {           
            return userRepository.AddUserAndLogin(user); ;
        }
        public User TryLoginUser(User user)
        {          
            return userRepository.VerifyUser(user);
        }
    }
}
