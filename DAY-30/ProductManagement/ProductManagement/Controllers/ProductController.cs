using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();

        [Route("")]
        [Route("Product")]
        [Route("Product/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            // Store products list in ViewBag to display in table
            ViewBag.ProductList = products;
            return View();
        }

        [Route("Product/Add")]
        [HttpPost]
        public IActionResult Add(string productName, decimal price, int quantity)
        {
            // Add new product to the list
            Product newProduct = new Product
            {
                Id = products.Count + 1,
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };

            products.Add(newProduct);

            // Store updated list in ViewBag
            ViewBag.ProductList = products;
            ViewBag.Message = $"Product '{productName}' added successfully!";

            return View("Index");
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue => Price * Quantity;
    }
}