using Microsoft.AspNetCore.Mvc;

namespace SimpleCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Calculator/Add
        [HttpPost]
        public IActionResult Add(double num1, double num2)
        {
            double result = num1 + num2;

            ViewData["Number1"] = num1;
            ViewData["Number2"] = num2;
            ViewData["Result"] = result;

            return View("Result");
        }
    }
}