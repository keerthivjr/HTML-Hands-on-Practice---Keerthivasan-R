using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find user by username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            // Verify password
            if (!_authService.VerifyPassword(loginDto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid username or password" });

            // Generate tokens
            var accessToken = _authService.GenerateJwtToken(user);
            var refreshToken = _authService.GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600,
                Username = user.Username,
                Email = user.Email
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Logged out successfully" });
        }

        //TEST ENDPOINT FOR DEBUGGING
        [Authorize]
        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                message = "Authentication successful!",
                userId = userId,
                userName = userName,
                email = email,
                timestamp = DateTime.UtcNow
            });
        }
    }
}