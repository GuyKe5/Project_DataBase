using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Project_DataBase.DAL
{
    public class UserServiceDAL
    {
        public static DataTable GetUserDataDAL(string username,string password)
        {
            string query = $"exec [GetUserData] {username},{password}";
            DataTable result=SQLHelper.SelectData(query);
            return result;

        }
    }
}