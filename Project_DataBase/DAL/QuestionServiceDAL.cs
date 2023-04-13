using Project_DataBase.Classes;
using System.Data;
using System.Runtime.InteropServices;
namespace Project_DataBase.DAL
{
    public class QuestionServiceDAL
    {

        public static DataTable GetTestFromQuestionId(int id)
        {
            string query = $"select * from Tests where questionId={id}"; 
            DataTable t = SQLHelper.SelectData(query);
            return t;
        }
    }
}
