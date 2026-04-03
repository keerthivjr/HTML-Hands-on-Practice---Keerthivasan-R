using Microsoft.AspNetCore.Mvc;

namespace AppUILayer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ShowContacts", "Contact");
        }
    }
}