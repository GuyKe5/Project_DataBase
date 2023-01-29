﻿using Project_DataBase.Classes;
using Project_DataBase.DAL;
using Project_DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Project_DataBase.BLL
{
    public class UserService
    { 

        public static string GetUserDataBLL(string username,string password)
        {
            DataTable data = UserServiceDAL.GetUserDataDAL(username,password);
            if (data == null || data.Rows.Count == 0)// check if login incorrect
            {
                return null;
            }
           User user = new User(data);
            return Functions.DataTableToJSON(data);
        }

        //Update

        //delete
    }
}