using Shared.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task<Order> CreateOrder(Order order);
        Task<Order?> UpdateOrderStatus(int orderId, string status);
    }
}