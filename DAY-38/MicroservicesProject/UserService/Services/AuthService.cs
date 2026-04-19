using Shared.DTOs;
using Shared.Helpers;
using Shared.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponse?> Login(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            var token = _jwtHelper.GenerateToken(user.Id, user.Username, user.Email, user.Role);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public async Task<AuthResponse?> Register(RegisterRequest request)
        {
            var exists = await _userRepository.UserExists(request.Username, request.Email);
            if (exists)
            {
                return null;
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateUser(user);

            var token = _jwtHelper.GenerateToken(createdUser.Id, createdUser.Username, createdUser.Email, createdUser.Role);

            return new AuthResponse
            {
                Token = token,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Role = createdUser.Role,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}