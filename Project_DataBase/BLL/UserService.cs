using Project_DataBase.Classes;
using Project_DataBase.DAL;
using Project_DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using ServiceReference1;
namespace Project_DataBase.BLL
{
    public class UserService
    {

        //get all users
        public static List<User> GetAllUsersBLL()
        {
            DataTable data = UserServiceDAL.GetAllUsersDAL();
            if (data == null || data.Rows.Count == 0)
            {
                return null;
            }
            List<User> users = Functions.MapDataTableToListOfClass<User>(data);
            return users;
        }
        public static User GetUserDataBLL(string username, string password)
        {
            DataTable data = UserServiceDAL.GetUserDataDAL(username, password);

            if (data == null || data.Rows.Count == 0)// check if login incorrect
            {
                return null;
            }
            User user = Functions.MapDataTableToClass<User>(data);
            return user;
        }

        public static string Register(JsonElement json)
        {
            dynamic obj = JsonNode.Parse(json.GetRawText());
            string email = (string)obj["email"];

            try
            {
                var client = new ServiceReference1.ValidationSoapClient(ServiceReference1.ValidationSoapClient.EndpointConfiguration.ValidationSoap); // Instantiate the generated client proxy

                var response = client.isEmailAsync(email).GetAwaiter().GetResult();

                bool isEmailValid = response.Body.isEmailResult;

                if (!isEmailValid)
                {
                    return "Invalid email"; 
                }

               
                return UserServiceDAL.Register(json);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while calling the email validation service: {ex.Message}");
                return "Error during email validation";
            }
        }
    }
}