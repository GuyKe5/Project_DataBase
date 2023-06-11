using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_DataBase.Classes
{
    public class Question
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int course_id { get; set; }
    }

    public class FullQuestion : Question
    {
        public string prompt { get; set; }
        public string solution { get; set; }
        public string savedWork { get; set; }
        public string baseCode { get; set; }
    }

    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public int questionId { get; set; }

        public string status { get; set; }
    }



}