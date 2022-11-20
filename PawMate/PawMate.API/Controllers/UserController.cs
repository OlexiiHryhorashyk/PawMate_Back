using Microsoft.AspNetCore.Mvc;
using PawMate.Models.UserDTOs;
using Services.Interfaces;

namespace PawMate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IServiceManager service)
        {
            _userService = service.User;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute]int userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO user)
        {
            return Ok(await _userService.UpdateUserAsync(user));
        }

        [HttpPost("New")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            return Ok(await _userService.AddUser(user));
        }

        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            return Ok(await _userService.DeleteUserById(userId));
        }
    }
}
