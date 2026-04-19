using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureJWTMVC.Models;

namespace SecureJWTMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    // Primary constructor defined
    public class AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        : Controller
    {
        public IActionResult Index()
        {
            // Parameters are accessed directly
            ViewBag.TotalUsers = userManager.Users.Count();
            ViewBag.TotalRoles = roleManager.Roles.Count();
            return View();
        }

        public IActionResult Users()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }
    }
}