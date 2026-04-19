using Shared.DTOs;
using Shared.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderManagementService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductServiceClient _productServiceClient;

        public OrderManagementService(IOrderRepository orderRepository, IProductServiceClient productServiceClient)
        {
            _orderRepository = orderRepository;
            _productServiceClient = productServiceClient;
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            return await _orderRepository.GetOrdersByUserId(userId);
        }

        public async Task<Order?> CreateOrder(CreateOrderDTO orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                Items = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in orderDto.Items)
            {
                // Get product details from Product Service
                var product = await _productServiceClient.GetProductById(item.ProductId);
                if (product == null)
                {
                    return null; // Product not found
                }

                // Check stock
                if (product.StockQuantity < item.Quantity)
                {
                    return null; // Insufficient stock
                }

                // Update stock in Product Service
                var stockUpdated = await _productServiceClient.UpdateStock(item.ProductId, item.Quantity);
                if (!stockUpdated)
                {
                    return null;
                }

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                order.Items.Add(orderItem);
                totalAmount += product.Price * item.Quantity;
            }

            order.TotalAmount = totalAmount;

            return await _orderRepository.CreateOrder(order);
        }

        public async Task<Order?> UpdateOrderStatus(int orderId, string status)
        {
            return await _orderRepository.UpdateOrderStatus(orderId, status);
        }
    }
}