using Shared.DTOs;
using Shared.Models;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Task<Payment?> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
        Task<Payment?> ProcessPayment(ProcessPaymentDTO paymentDto);
        Task<Payment?> GetPaymentStatus(int orderId);
    }
}