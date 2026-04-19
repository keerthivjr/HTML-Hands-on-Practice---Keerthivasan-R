using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);

            if (result == null)
            {
                return Unauthorized(ApiResponse<AuthResponse>.ErrorResponse("Invalid username or password"));
            }

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, "Login successful"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request);

            if (result == null)
            {
                return BadRequest(ApiResponse<AuthResponse>.ErrorResponse("Username or email already exists"));
            }

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, "Registration successful"));
        }
    }
}