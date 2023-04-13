using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_DataBase.BLL;
using Project_DataBase.Classes;
using System.Text.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Project_DataBase.web_service
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        [HttpPut("AddQuestion")]
        public IActionResult AddQuestion([FromBody] JsonElement value)
        {

            try
            {
                string response = QuestionService.AddQuestion(value);
                if (response == "ok") { return Ok(); }
                return StatusCode(500, new { error = response });



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpPost("SubmitAndCheckCode")]
        public IActionResult SubmitAndCheckCode([FromBody]JsonElement value) // להריץ את הבדיקות על הקוד להחזיר את הבדיקות עם סטטוס עבר או לא עבר
        {
            try
            {
               List<Test> response = QuestionService.SubmitAndCheckCode(value);
                return Ok(response);



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :("+ex });
            }
        }

        [HttpGet("GetTestFromQuestionId")]
        public IActionResult GetTestFromQuestionId(int id)
        {
            try
            {
                List<Test> response = QuestionService.GetTestFromQuestionId(id);
                return Ok(response);



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" + ex });
            }
        }
    }
}
