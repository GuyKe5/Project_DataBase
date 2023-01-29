using Microsoft.AspNetCore.Mvc;
using Project_DataBase.BLL;
using System.Text.Json;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_DataBase.web_service
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

                // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost]
        public IActionResult GetUserData([FromBody] JsonElement value)
        {
            try
            {
                // Parse the JSON element
                dynamic obj = JsonNode.Parse(value.GetRawText());
                string username = (string)obj["username"];
                string password = (string)obj["password"];
                // Validate the username and password
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return BadRequest(new { error = "username and password are required." });
                }

                // Get the user data
                var user = UserService.GetUserDataBLL(username, password);
                if (user == null)
                {
                    return NotFound(new { error = "User not found." });
                }

                // Return the user data
                return Ok(new { user });
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 Internal Server Error
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

    }
}
