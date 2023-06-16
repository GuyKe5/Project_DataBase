using Project_DataBase.Classes;
using System.Data;
using System.Runtime.InteropServices;
namespace Project_DataBase.DAL
{
    public class QuestionServiceDAL
    {

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
