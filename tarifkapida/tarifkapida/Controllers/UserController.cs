using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using tarifkapida.Services;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetUsers")]
        public List<Users> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpPost("Login")]
        public Users? Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return null; // Or handle the error as needed
            }
            return userService.LoginAsync(loginRequest).Result;
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<ActionResult<Users?>> GetUserById(int userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<Users>> CreateUser([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }
            var createdUser = await userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<Users>> UpdateUser([FromBody] Users user)
        {
            if (user == null || user.UserId <= 0)
            {
                return BadRequest("Invalid user data");
            }
            var updatedUser = await userService.UpdateUserAsync(user);
            return Ok(updatedUser);
        }
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }
            var result = await userService.DeleteUserAsync(userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var registeredUser = await userService.RegisterAsync(request);
                return Ok(new { message = "Kayıt başarılı", userId = registeredUser.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
