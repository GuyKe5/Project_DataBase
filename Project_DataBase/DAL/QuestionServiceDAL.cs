using Project_DataBase.Classes;
using System.Data;
using System.Runtime.InteropServices;
namespace Project_DataBase.DAL
{
    public class QuestionServiceDAL
    {
        public static DataTable GetFullQuestionFromQuestionIdDLL(int id)
        {
            string query = $"select * from Questions where id={id}";
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }
        public static int AddQuestionDLL(string name, string prompt, int writer, int courseId, string baseCode, string solution, int edit)
        {
            string query;
            if (edit > 0)
            {
                query = $@"UPDATE Questions
           SET name = '{name}',
               prompt = '{prompt}',
               writer = '{writer}',
               course_id = '{courseId}',
               baseCode = '{baseCode}',
               solution = '{solution}'
           WHERE id = '{edit}'";
                int sql = SQLHelper.DoQuery(query);
                               if (sql > 0) { return edit; }


                return -1;


            }
            else
            {
                query = $@"INSERT INTO Questions (name, prompt, writer, course_id,baseCode,solution)
                     OUTPUT INSERTED.id
 VALUES ('{name}', '{prompt}', '{writer}', '{courseId}','{baseCode}','{solution}')";
            }

            object questionIdObj = SQLHelper.SelectScalar(query);
            if (questionIdObj != null && questionIdObj != DBNull.Value)
            {
                return Convert.ToInt32(questionIdObj);
            }
            else
            {
                return -1; // No rows were affected
            }
        }
       

        public static void DeleteOldTests(int questionId)
        {
            string deleteQuery = $"delete from Tests where questionId={questionId}"; // delete the old tests
            SQLHelper.DoQuery(deleteQuery);
        }
        public static int AddTestDLL(string name, string input, string output, int questionId)
        {
            if(name=="" && input=="" && output=="") { return 1; }
           
            string query = $"INSERT INTO Tests(name,input,output,questionId) VALUES ('{name}','{input}','{output}', '{questionId}')";
            int affected = SQLHelper.DoQuery(query);
            return (affected);
        }


        public static int ChangeStatusDLL(int user_id,string status,int question_id)
        {
            string deleteQuery = $"delete from QuestionStatus where question_id={question_id} and user_id={user_id}";
            string query = $"insert into  QuestionStatus(question_id,user_id,status) values({question_id},{user_id},'{status}')";
            int deleteAffected = SQLHelper.DoQuery(deleteQuery); // if user already has status for the question, delete it
            int affected = SQLHelper.DoQuery(query);
            return affected;
        }
        public static DataTable GetTestFromQuestionId(int id)
        {
            string query = $"select * from Tests where questionId={id}"; 
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }
    }
}
