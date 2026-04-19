using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using PaymentService.Services;
using System.Security.Claims;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpPost("process")]
        public async Task<ActionResult<ApiResponse<Payment>>> ProcessPayment([FromBody] ProcessPaymentDTO paymentDto)
        {
            paymentDto.UserId = GetCurrentUserId();
            var payment = await _paymentService.ProcessPayment(paymentDto);

            if (payment == null)
            {
                return BadRequest(ApiResponse<Payment>.ErrorResponse("Payment processing failed"));
            }

            return Ok(ApiResponse<Payment>.SuccessResponse(payment, "Payment processed successfully"));
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<ApiResponse<Payment>>> GetPaymentByOrderId(int orderId)
        {
            var payment = await _paymentService.GetPaymentStatus(orderId);
            if (payment == null)
            {
                return NotFound(ApiResponse<Payment>.ErrorResponse("Payment not found for this order"));
            }

            // Check authorization
            var userId = GetCurrentUserId();
            if (payment.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(ApiResponse<Payment>.SuccessResponse(payment));
        }

        [HttpGet("my-payments")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Payment>>>> GetMyPayments()
        {
            var userId = GetCurrentUserId();
            var payments = await _paymentService.GetPaymentsByUserId(userId);
            return Ok(ApiResponse<IEnumerable<Payment>>.SuccessResponse(payments));
        }
    }
}