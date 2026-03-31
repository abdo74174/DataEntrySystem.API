using DataEntrySystem.API.Models.DTOs;
using DataEntrySystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid username, password, or role" });
            }
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result)
            {
                return BadRequest(new { message = "Username already exists" });
            }
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateDto)
        {
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value 
                             ?? User.FindFirst("sub")?.Value 
                             ?? "0";
            var userId = int.Parse(userIdStr);
            var result = await _authService.UpdateProfileAsync(userId, updateDto);
            if (!result)
            {
                return BadRequest(new { message = "Update failed (user not found or username taken)" });
            }
            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpGet("all-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("update-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateProfileDto updateDto)
        {
            var result = await _authService.UpdateProfileAsync(id, updateDto);
            if (!result)
            {
                return BadRequest(new { message = "Update failed" });
            }
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("delete-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "Delete failed" });
            }
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
