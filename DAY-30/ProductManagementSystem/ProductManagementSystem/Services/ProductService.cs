using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 }
        };
        public IEnumerable<Product> GetAllProducts() => _products;

        public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Product product) => _products.Add(product);

        public void DeleteProduct(int id) => _products.RemoveAll(p => p.Id == id);
    }
}
