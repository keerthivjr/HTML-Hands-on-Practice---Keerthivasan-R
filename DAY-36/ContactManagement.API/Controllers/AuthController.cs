using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContactManagement.DAL.DbContext;
using ContactManagement.DAL.Models;
using ContactManagement.API.DTOs;
using ContactManagement.API.Exceptions;

namespace ContactManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// Primary Constructor: Injection happens right here in the class header
public class AuthController(
    ApplicationDbContext context,
    IConfiguration configuration,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var userExists = await context.Users.AnyAsync(u => u.Username == registerDto.Username);
        if (userExists)
        {
            throw new BadRequestException("User already exists");
        }

        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = HashPassword(registerDto.Password), // Calling static method
            Role = registerDto.Role == "Admin" ? "Admin" : "User"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        logger.LogInformation("New user registered: {Username} with role {Role}", user.Username, user.Role);

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

        if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for user: {Username}", loginDto.Username);
            throw new UnauthorizedException("Invalid username or password");
        }

        var token = GenerateJwtToken(user);
        logger.LogInformation("User logged in: {Username}", user.Username);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        });
    }

    private string GenerateJwtToken(User user)
    {
        // Note: This still uses 'configuration' from the primary constructor
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "ThisIsASecretKeyForJWTTokenThatIsAtLeast32CharsLong!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Marked as static because it only relies on the input string
    private static string HashPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }

    // Marked as static because it only relies on the input string and hash
    private static bool VerifyPassword(string password, string hash)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password)) == hash;
    }
}