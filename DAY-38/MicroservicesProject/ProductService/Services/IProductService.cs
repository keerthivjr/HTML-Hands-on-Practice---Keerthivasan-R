using Shared.DTOs;
using Shared.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> CreateProduct(ProductCreateDTO productDto);
        Task<Product?> UpdateProduct(ProductUpdateDTO productDto);
        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateStock(int productId, int quantity);
    }
}