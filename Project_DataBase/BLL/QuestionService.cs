using Project_DataBase.Classes;
using System.Data;
using Project_DataBase.DAL;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Project_DataBase.BLL
{
    public class QuestionService
    {
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

        public static string ExecuteTestWithConsoleReadLine(string code, string input)
        {
            string addLineToTest = $"Console.SetIn(new System.IO.StringReader(\"{input}\"));";

            // Find the index of the opening curly brace '{' after the method declaration
            int openingBraceIndex = code.IndexOf("Main") + 13;

            // Insert the new line at the calculated index
            string modifiedCode = code.Insert(openingBraceIndex, addLineToTest + "\n");

            string scriptCode = modifiedCode;

            try
            {
                using (var consoleOutput = new StringWriter())
                {
                    var originalIn = Console.In;
                    var originalOut = Console.Out;

                    try
                    {
                        var inputReader = new StringReader(input);
                        Console.SetIn(inputReader);
                        Console.SetOut(consoleOutput);

                        var scriptOptions = ScriptOptions.Default
                            .AddReferences(typeof(Console).Assembly)
                            .AddImports("System");

                        var script = CSharpScript.Create(scriptCode, scriptOptions);
                        script.RunAsync().Wait();

                        string output = consoleOutput.ToString();

                        return output;
                    }
                    finally
                    {
                        Console.SetIn(originalIn);
                        Console.SetOut(originalOut);
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }








    }
}
