using Shared.DTOs;
using Shared.Models;

namespace UserService.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(LoginRequest request);
        Task<AuthResponse?> Register(RegisterRequest request);
    }
}