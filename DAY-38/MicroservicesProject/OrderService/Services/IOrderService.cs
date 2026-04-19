using Shared.DTOs;
using Shared.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task<Order?> CreateOrder(CreateOrderDTO orderDto);
        Task<Order?> UpdateOrderStatus(int orderId, string status);
    }
}