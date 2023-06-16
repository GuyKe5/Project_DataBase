using Newtonsoft.Json.Linq;
using Project_DataBase.BLL;
using Project_DataBase.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;


namespace Project_DataBase.DAL
{
    public class UserServiceDAL
    {
        //get all users
        public static DataTable GetAllUsersDAL()
        {
       //     string query = "select * from Users";
              string query = "exec [GetAllUsers]";
            DataTable result = SQLHelper.SelectData(query);
            return result;
        }
        public static DataTable GetUserDataDAL(string username,string password)
        {
            string query = $"exec [GetUserData] '{username}','{password}'";
            DataTable result=SQLHelper.SelectData(query);
            return result;
        }

        public static string Register(JsonElement json)
        {
            try
            {


                dynamic obj = JsonNode.Parse(json.GetRawText());
                string username = (string)obj["username"];
                string password = (string)obj["password"];
                string email = (string)obj["email"];
                int affected = 0;
                string checkQuery = $"exec CheckIfUserNameTaken {username}"; 
                DataTable check = SQLHelper.SelectData(checkQuery);
                if(check.Rows.Count==0)
                {
                   
                   // string query = $"INSERT INTO Users (username, password, email) VALUES ('{username}','{password}', '{email}')";
                   string query = $"exec [Register] '{username}','{password}','{email}'";
                  affected= SQLHelper.DoQuery(query);

                }
                else
                {
                    return ("username already taken");
                }
                if (affected > 0)
                {
                    return ("ok");
                }
                else
                {
                    return ("error in the query");
                }
            }
            catch(Exception ex)
            {
                return ("unkown error");
            }
        }
    }
}