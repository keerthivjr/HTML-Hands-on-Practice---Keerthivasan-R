using Microsoft.AspNetCore.Identity;

namespace SecureJWTMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}