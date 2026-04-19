using Shared.Models;

namespace PaymentService.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
        Task<Payment?> GetPaymentByOrderId(int orderId);
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment?> UpdatePaymentStatus(int paymentId, string status, string transactionId);
    }
}