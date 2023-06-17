using Project_DataBase.Classes;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;

namespace Project_DataBase.DAL
{
    public class CourseServiceDAL
    {
        //delete course
        public static int DeleteCourse(int id)
        {
            string query = $"DELETE FROM Courses WHERE id={id}";
            int affected = SQLHelper.DoQuery(query);
            return affected;   
        }
        public static string Enroll(int userId,int CourseId)
        {
              string query = $"INSERT INTO Enrollments(user_id,course_id) VALUES ('{userId}','{CourseId}')";
            int affected = SQLHelper.DoQuery(query);
            if (affected == 1)
                return "ok";
            else
                return "Error";
        }
        public static bool CheckIfEnrolledDLL(int UserId,int CourseId)
        {
            string query=$"select * from Enrollments where user_id={UserId} and course_id={CourseId}";
            DataTable t = SQLHelper.SelectData(query);
            if (t.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public static DataTable GetCoursesIdsThatUserEnrolledDAL(int id)
        {
            string query = $"select course_id from Enrollments where user_id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }


        public static DataTable GetAllCourses()
        {
            string query = $"select * from Courses";
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

        public static string GetQuestionStatus(int questionId, int userId)
        {
            string query = $"select status from QuestionStatus where question_id={questionId} and user_id={userId}";
            string t = SQLHelper.SelectScalarToString(query);
            return t;

        }
      

        public static int AddCourseDLL(string name, string description, string date,int writer)
        {
            string query = $"INSERT INTO Courses(name,description,date,writer) VALUES ('{name}',' {description}', '{date}','{writer}'" +
                $")";
            int affected = SQLHelper.DoQuery(query);
            return affected;
        }

      

        public static DataTable GetCourseDataByWriterIdDLL(int writerId)
        {
            //string query = $"select * from Courses where writer={writerId}";
            string query = $"exec GetCourseEnrollmentCount {writerId}";

            DataTable t = SQLHelper.SelectData(query);
            return t;
        }


    }
}
