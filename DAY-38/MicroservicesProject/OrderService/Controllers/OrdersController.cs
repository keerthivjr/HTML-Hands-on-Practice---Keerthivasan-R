using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using OrderService.Services;
using System.Security.Claims;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Order>>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound(ApiResponse<Order>.ErrorResponse("Order not found"));
            }

            // Check if user owns this order
            var userId = GetCurrentUserId();
            if (order.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(ApiResponse<Order>.SuccessResponse(order));
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Order>>>> GetMyOrders()
        {
            var userId = GetCurrentUserId();
            var orders = await _orderService.GetOrdersByUserId(userId);
            return Ok(ApiResponse<IEnumerable<Order>>.SuccessResponse(orders));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Order>>> CreateOrder([FromBody] CreateOrderDTO orderDto)
        {
            orderDto.UserId = GetCurrentUserId();
            var order = await _orderService.CreateOrder(orderDto);

            if (order == null)
            {
                return BadRequest(ApiResponse<Order>.ErrorResponse("Failed to create order. Check product availability or stock."));
            }

            return Ok(ApiResponse<Order>.SuccessResponse(order, "Order created successfully"));
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<Order>>> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var order = await _orderService.UpdateOrderStatus(id, status);
            if (order == null)
            {
                return NotFound(ApiResponse<Order>.ErrorResponse("Order not found"));
            }
            return Ok(ApiResponse<Order>.SuccessResponse(order, "Order status updated"));
        }
    }
}