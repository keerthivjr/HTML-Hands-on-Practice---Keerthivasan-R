using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/payment
        [HttpGet]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _context.Payments
                .Select(p => new PaymentResponse
                {
                    Id = p.Id,
                    PaymentId = p.PaymentId,
                    CustomerId = p.CustomerId,
                    PolicyId = p.PolicyId,
                    PolicyNumber = p.PolicyNumber,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Status = p.Status,
                    PaymentDate = p.PaymentDate,
                    PaymentFor = p.PaymentFor,
                    PaymentMonth = p.PaymentMonth,
                    PaymentYear = p.PaymentYear
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/payment/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerPayments(int customerId)
        {
            var payments = await _context.Payments
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => new PaymentResponse
                {
                    Id = p.Id,
                    PaymentId = p.PaymentId,
                    CustomerId = p.CustomerId,
                    PolicyId = p.PolicyId,
                    PolicyNumber = p.PolicyNumber,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Status = p.Status,
                    PaymentDate = p.PaymentDate,
                    PaymentFor = p.PaymentFor,
                    PaymentMonth = p.PaymentMonth,
                    PaymentYear = p.PaymentYear
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/payment/policy/{policyId}
        [HttpGet("policy/{policyId}")]
        public async Task<IActionResult> GetPolicyPayments(int policyId)
        {
            var payments = await _context.Payments
                .Where(p => p.PolicyId == policyId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return Ok(payments);
        }

        // POST: api/payment
        [HttpPost]
        public async Task<IActionResult> MakePayment(CreatePaymentRequest request)
        {
            var payment = new Payment
            {
                PaymentId = $"PAY{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                CustomerId = request.CustomerId,
                PolicyId = request.PolicyId,
                PolicyNumber = request.PolicyNumber,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionId = request.TransactionId,
                Status = "Completed",
                PaymentFor = request.PaymentFor,
                PaymentMonth = request.PaymentMonth,
                PaymentYear = request.PaymentYear
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment processed successfully", paymentId = payment.PaymentId });
        }

        // GET: api/payment/summary/{customerId}
        [HttpGet("summary/{customerId}")]
        public async Task<IActionResult> GetPaymentSummary(int customerId)
        {
            var totalPaid = await _context.Payments
                .Where(p => p.CustomerId == customerId && p.Status == "Completed")
                .SumAsync(p => p.Amount);

            var lastPayment = await _context.Payments
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                TotalPaid = totalPaid,
                LastPaymentDate = lastPayment?.PaymentDate,
                LastPaymentAmount = lastPayment?.Amount,
                TotalPayments = await _context.Payments.CountAsync(p => p.CustomerId == customerId)
            });
        }
    }
}