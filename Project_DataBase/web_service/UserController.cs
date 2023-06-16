using Microsoft.AspNetCore.Mvc;
using Project_DataBase.BLL;
using Project_DataBase.Classes;
using System.Text.Json;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_DataBase.web_service
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Get all users
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                // Get all users
                List<User> users = UserService.GetAllUsersBLL();
                if (users == null)
                {
                    return NotFound(new { error = "No users found." });
                }

                // Return the users
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }
        [HttpPost("GetUserData")]
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
                User user = UserService.GetUserDataBLL(username, password);
                if (user == null)
                {
                    return NotFound(new { error = "User not found." });
                }

                // Return the user data
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. :(" });
            }
        }

        [HttpPut("Register")]
        public IActionResult Register([FromBody] JsonElement value)
        {
            string response = UserService.Register(value);
            if (response == "ok")
            {
                return Ok();
            }
            if (response == "username already taken")
            {
                return NotFound(new { error = "Username taken." });
            }
            else if (response == "Invalid email")
            {
                // return 403  error
                return StatusCode(403, new { error = "Invalid email." });
            }
            else if (response == "Error during email validation")
            {
                return StatusCode(500, new { error = "Error during email validation." });
            }
            else
            {

                return StatusCode(500, new { error = response });
            }

        }

    }
}
