using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_DataBase.Classes
{
    public class Question
    {
        public string name { get; set; }
        public string discription { get; set; }
        public User auther { get; set; }
        //public Test[] tests{ get; set; }
    }

}