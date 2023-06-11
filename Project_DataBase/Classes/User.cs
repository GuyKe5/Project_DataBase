using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Project_DataBase.Classes
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
    

        public User(string username, string password, string email)
        {
            username= username;
            password = password;
            email = email;
        }
        public User() { }

}
  
}