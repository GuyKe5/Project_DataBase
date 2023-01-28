using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Project_DataBase.Classes
{
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public User(DataTable data)
        {
            DataRow arr = data.Rows[0];
            this.userName = arr["UserName"].ToString();
            this.password = arr["Password"].ToString();
            this.email = arr["Email"].ToString();
            this.id = Convert.ToInt32(arr["Id"]);
        }

    }
}