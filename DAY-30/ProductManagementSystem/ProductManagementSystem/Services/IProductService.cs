using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        void AddProduct(Product product);

        void DeleteProduct(int id);
    }
}
