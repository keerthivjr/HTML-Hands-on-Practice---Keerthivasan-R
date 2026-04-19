using Shared.Models;

namespace OrderService.Services
{
    public interface IProductServiceClient
    {
        Task<Product?> GetProductById(int id);
        Task<bool> UpdateStock(int productId, int quantity);
    }
}