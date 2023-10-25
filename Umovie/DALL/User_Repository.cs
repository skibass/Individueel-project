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
        MySqlConnection db = new MySqlConnection("Server=127.0.0.1;Database=umovie;Uid=root;Pwd=;");

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            db.Open();

            MySqlCommand accountQuery = new MySqlCommand("SELECT * from users", db);

            accountQuery.ExecuteNonQuery();

            MySqlDataReader readAccounts = accountQuery.ExecuteReader();

            while (readAccounts.Read())
            {
                User user = new User();

                user.Id = (int)readAccounts["user_id"];
                user.Name = (string)readAccounts["user_name"];
                user.Email = (string)readAccounts["user_email"];
                user.RoleId = (int)readAccounts["role_id"];
               
                users.Add(user);
            }
            db.Close();

            return users;
        }
    }
}
