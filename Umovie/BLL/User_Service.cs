using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void TryRegisterUser(User user)
        {
            userRepository.AddUserAndLogin(user);
        }

        //public User TryRegisterUser(User user)
        //{
        //    return userRepository.RegisterUser();
        //}
        //public User TryLoginUser(User user)
        //{
        //    return userRepository.LoginUser();
        //}
    }
}
