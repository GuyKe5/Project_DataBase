using Project_DataBase.Classes;
using System.Data;
using Project_DataBase.DAL;
using System.Text.Json.Nodes;

namespace Project_DataBase.BLL
{
    public class CourseService
    {
        public static List<Course> GetEnrolledCoursesByIdBLL(int id)
        {
            List<Course> courses = new List<Course>();
            int[] coursesIds = GetCoursesIdsThatUserEnrolled(id);
            for(int i =0; i < coursesIds.Length; i++)
            {
                DataTable courseDT = CourseServiceDAL.GetCourse(coursesIds[i]);
                Course course = Functions.MapDataTableToClass<Course>(courseDT);
                courses.Add(course);

            }

            return courses;

        }
        public static List<Question> GetQuestionsFromCourseIdBLL(int id)
        {
            DataTable QuestionDT = CourseServiceDAL.GetQuestionsFromCourseIdDLL(id);

            return Functions.MapDataTableToListOfClass<Question>(QuestionDT);
        }

        public static int[] GetCoursesIdsThatUserEnrolled(int id)
        {
            DataTable d = CourseServiceDAL.GetCoursesIdsThatUserEnrolledDAL(id);
            int[] array = d.Rows.OfType<DataRow>().Select(k => int.Parse(k[0].ToString())).ToArray();
            return array;

        }

       
    }
}
