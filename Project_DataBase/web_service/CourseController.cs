using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project_DataBase.BLL;
using System.Text.Json.Nodes;
using Project_DataBase.BLL;

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

        [HttpGet("GetQuestionsFromCourseId")]
        public IActionResult GetQuestionsFromCourseId(int id)
        {
            try
            {
                return Ok(CourseService.GetQuestionsFromCourseIdBLL(id));


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :("  });
            }
        }


    }
}
