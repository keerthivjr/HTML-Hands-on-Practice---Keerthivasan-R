using Microsoft.EntityFrameworkCore;
using Shared.Models;
using PaymentService.Data;

namespace PaymentService.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
        {
            return await _context.Payments
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByOrderId(int orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> UpdatePaymentStatus(int paymentId, string status, string transactionId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null) return null;

            payment.Status = status;
            payment.TransactionId = transactionId;
            payment.PaymentDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return payment;
        }
    }
}