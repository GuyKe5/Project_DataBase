using System.Data;

namespace Project_DataBase.DAL
{
    public class CourseServiceDAL
    {
        public static DataTable GetCoursesIdsThatUserEnrolledDAL(int id)
        {
            string query = $"select course_id from Enrollments where user_id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }


        public static DataTable GetQuestionsFromCourseIdDLL(int id)
        {
            string query = $"select * from Questions where course_id2={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }


        public static DataTable GetCourse(int id)
        {
            string query = $"select * from Courses where id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }
    }
}
