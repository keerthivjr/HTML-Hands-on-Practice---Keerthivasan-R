using Microsoft.AspNetCore.Mvc;

namespace FeedbackSystem.Controllers
{
    public class FeedbackController : Controller
    {
        [Route("")]
        [Route("Feedback")]
        [Route("Feedback/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Feedback/Submit")]
        [HttpPost]
        public IActionResult Submit(string name, string comments, int rating)
        {
            // Store feedback data in ViewData
            ViewData["Name"] = name;
            ViewData["Comments"] = comments;
            ViewData["Rating"] = rating;

            // Conditional message based on rating
            if (rating >= 4)
            {
                ViewData["Message"] = "Thank You! 🌟 We really appreciate your positive feedback!";
                ViewData["MessageType"] = "success";
            }
            else
            {
                ViewData["Message"] = "We will improve! 💪 Thank you for your honest feedback.";
                ViewData["MessageType"] = "warning";
            }

            return View("FeedbackResult");
        }
    }
}