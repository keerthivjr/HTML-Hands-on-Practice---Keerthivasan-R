using Microsoft.AspNetCore.Mvc;
using ProductMvcApp.Models;
using System.Collections.Generic;

namespace ProductMvcApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Dummy data (no database)
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 50000 },
                new Product { Id = 2, Name = "Mobile", Price = 20000 },
                new Product { Id = 3, Name = "Headphones", Price = 3000 }
            };

            return View(products);
        }
    }
}
