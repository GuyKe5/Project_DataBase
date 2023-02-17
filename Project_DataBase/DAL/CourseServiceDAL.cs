using System.Data;
using System.Runtime.InteropServices;

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
            string query = $"select * from Questions where course_id={id}"; //change to not * but only relevent
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }


        public static DataTable GetCourse(int id)
        {
            string query = $"select * from Courses where id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }

        public static string GetQuestionStatus(int questionId,int userId)
        {
            string query = $"select status from QuestionStatus where question_id={questionId} and user_id={userId}";
            string t = SQLHelper.SelectScalarToString(query);
            return t;

        }
        public static DataTable GetFullQuestionFromQuestionIdDLL(int id)
        {
            string query = $"select * from Questions where id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }

        public static int AddCourseDLL(string name,string description,string date)
        {
            string query = $"INSERT INTO Courses(name,description,date) VALUES ('{name}',' {description}', '{date}')";
            int affected = SQLHelper.DoQuery(query);
            return affected;
        }
    }
}
