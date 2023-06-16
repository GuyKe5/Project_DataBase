namespace Project_DataBase.Classes
{
    public class Course
    {
        public string name { get; set; }
        public string description { get; set; }
        public string date{ get; set; }
        public int id { get; set; }
        public int writer { get; set; }
        public int enrolled { get; set; }
    }

    public class PaidCourse : Course
    {
        public double price { get; set; }
        public double discount { get; set; }
    }

}
