using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;
using ProductManagementSystem.Services;

namespace ProductManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // 1. Display List
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        // 3. Retrieve Details by ID
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // 2. Add Product (The Form Page)
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productService.AddProduct(product);
            return RedirectToAction("Index");
        }

        // 4. Delete Product
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }




    }
}
