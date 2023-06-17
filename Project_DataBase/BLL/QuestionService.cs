using Project_DataBase.Classes;
using System.Data;
using Project_DataBase.DAL;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System.Reflection;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System.CodeDom.Compiler;
using System.Text;

namespace Project_DataBase.BLL
{
    public class QuestionService
    {
        public static string ChangeStatus(JsonElement value)
        {
            int user_id = value.GetProperty("user_id").GetInt32();
            int question_id = int.Parse(value.GetProperty("question_id").ToString());
            string status = value.GetProperty("status").ToString();


            int affected = QuestionServiceDAL.ChangeStatusDLL(user_id, status, question_id);
            if (affected > 0)
            {
                return ("ok");
            }
            else
            {
                return ("error in the query");
            }
        }
        public static List<Test> GetTestFromQuestionId(int id)
        {

            DataTable TestDT = QuestionServiceDAL.GetTestFromQuestionId(id);
            List<Test> TestList = Functions.MapDataTableToListOfClass<Test>(TestDT);
            return TestList;
        }
        public static FullQuestion GetFullQuestionFromQuestionIdBLL(int id)
        {
            DataTable d = CourseServiceDAL.GetFullQuestionFromQuestionIdDLL(id);
            FullQuestion q = Functions.MapDataTableToClass<FullQuestion>(d);
            return q;
        }

        public static string AddQuestion(JsonElement value)
        {
            string name = value.GetProperty("questionname").GetString();
            string description = value.GetProperty("description").GetString();
            string baesCode = value.GetProperty("baseCode").GetString();
            string solution = value.GetProperty("soulutionCode").GetString();
            int writer = value.GetProperty("writerId").GetInt32();
            int courseId = value.GetProperty("courseId").GetInt32();
            int edit = value.GetProperty("edit").GetInt32();
            int questionId = CourseServiceDAL.AddQuestionDLL(name, description, writer, courseId, baesCode, solution, edit);

            if (questionId > 0)
            {
                if (value.TryGetProperty("tests", out JsonElement testsElement) && testsElement.ValueKind == JsonValueKind.Array)
                {
                    var tests = new List<Test>();
                    foreach (JsonElement testElement in testsElement.EnumerateArray())
                    {
                        Test test = new Test
                        {
                            name = testElement.GetProperty("name").GetString(),
                            input = testElement.GetProperty("input").GetString(),
                            output = testElement.GetProperty("output").GetString(),
                            questionId = questionId
                        };
                        tests.Add(test);
                    }
                    bool addTestsResult = AddTests(tests);
                    if (!addTestsResult)
                    {
                        return "error adding tests";
                    }
                }
                return "ok";
            }
            else if (questionId == 0)
            {
                return "no rows were affected";
            }
            else // questionId < 0
            {
                return "error adding question";
            }
        }


        public static bool AddTests(List<Test> tests)
        {
            foreach (Test test in tests)
            {
                int affected = CourseServiceDAL.AddTestDLL(test.name, test.input, test.output, test.questionId);
                if (affected <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<Test> SubmitAndCheckCode(JsonElement value)
        {
            dynamic obj = JsonNode.Parse(value.GetRawText());
            string code = (string)obj["code"];  // student code
            var tests = new List<Test>();

            if (value.TryGetProperty("tests", out JsonElement testsElement) && testsElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement testElement in testsElement.EnumerateArray())
                {
                    Test test = new Test
                    {
                        id = testElement.GetProperty("id").GetInt32(),
                        name = testElement.GetProperty("name").GetString(),
                        input = testElement.GetProperty("input").GetString(),
                        output = testElement.GetProperty("output").GetString(),
                    };
                    tests.Add(test);
                }
            }

            for (int i = 0; i < tests.Count; i++)
            {
                Test test = tests[i];

                // Execute the test using the student's code
                string result = ExecuteTestWithConsoleReadLine(code, test.input);

                test.status = (result == test.output) ? "V" : "X";
            }

            return tests;
        }

        public static string ExecuteTestWithConsoleReadLine(string studentCode, string input)
        {
          




            string code = @$"string input = ""{input}"";
string userInput;
string consoleOutput;

using (StringReader sr = new StringReader(input))
{{

    Console.SetIn(sr);
   

       {studentCode}

return output;


    
}}";


            string result = ExecuteCodeAsync(code);
            return result;
        }



        public static string ExecuteCodeAsync(string code)
        {
            try
            {

                    var result = CSharpScript.EvaluateAsync(code, ScriptOptions.Default.WithImports("System", "System.IO", "System.Text"));

                result.Wait();

                string resultString = result.Result.ToString();

                string TrimmedrResult = resultString.Replace("\r", "").Replace("\n", "").Replace(" ", "");
               
                return TrimmedrResult;



            }
            catch (Exception ex)
            {
                
                return ex.Message;
            }
        }



    }

}

