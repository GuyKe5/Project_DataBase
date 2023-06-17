using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project_DataBase.BLL;
using System.Text.Json.Nodes;
using Project_DataBase.BLL;
using Project_DataBase.Classes;
using System.Text.Json;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_DataBase.web_service
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        [HttpGet("GetEnrolledCourses")]
        public IActionResult GetEnrolledCoursesById(int id)
        {
            try
            {
                return Ok(CourseService.GetEnrolledCoursesByIdBLL(id));


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }



        [HttpGet("CheckIfEnrolled")]
        public IActionResult CheckIfEnrolled(int UserId, int CourseId)
        {
            try
            {
                bool result = CourseService.CheckIfEnrolled(UserId, CourseId);
                if (result == true) { return Ok(); }
                else { return StatusCode(404, new { error = "User is not enrolled to this course" }); }



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                return Ok(CourseService.GetAllCourses());


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpGet("GetQuestionsFromCourseId")]
        public IActionResult GetQuestionsFromCourseId(int courseId, int userId)
        {
            try
            {
                return Ok(QuestionService.GetQuestionsFromCourseIdBLL(courseId, userId));


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpGet("GetFullQuestionFromQuestionId")]

        public IActionResult GetFullQuestionFromQuestionId(int qId)
        {
            try
            {
                return Ok(QuestionService.GetFullQuestionFromQuestionIdBLL(qId));


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpPut("AddCourse")]
        public IActionResult AddCourse([FromBody] JsonElement value)
        {

            try
            {
                string response = CourseService.AddCourse(value);
                if (response == "ok") { return Ok(); }
                return StatusCode(500, new { error = response });



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpPut("Enroll")]
        public IActionResult Enroll(int userId, int courseId)
        {

            try
            {
                string response = CourseService.Enroll(userId, courseId);
                if (response == "ok") { return Ok(); }
                return StatusCode(500, new { error = response });



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }



        [HttpGet("GetCourseDataByWriterId")]

        public IActionResult GetCourseDataByWriterId(int writerId)
        {
            try
            {
                return Ok(CourseService.GetCourseDataByWriterIdBLL(writerId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        //delete course
        [HttpDelete("DeleteCourse")]
        public IActionResult DeleteCourse(int courseId)
        {
            try
            {
                string response = CourseService.DeleteCourse(courseId);
                if (response == "ok") { return Ok(); }
                return StatusCode(500, new { error = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

    }
}
