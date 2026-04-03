using Microsoft.AspNetCore.Mvc;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Student/Register
        [HttpPost]
        public IActionResult Register(string studentName, int age, string course)
        {
            ViewBag.StudentName = studentName;
            ViewBag.Age = age;
            ViewBag.Course = course;

            return View("Display");
        }

        // GET: Student/Display
        public IActionResult Display()
        {
            return View();
        }
    }
}