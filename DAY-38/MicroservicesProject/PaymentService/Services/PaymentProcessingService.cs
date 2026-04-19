using Shared.DTOs;
using Shared.Models;
using PaymentService.Repositories;

namespace PaymentService.Services
{
    public class PaymentProcessingService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentProcessingService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
        {
            return await _paymentRepository.GetPaymentsByUserId(userId);
        }

        public async Task<Payment?> ProcessPayment(ProcessPaymentDTO paymentDto)
        {
            // Check if payment already exists for this order
            var existingPayment = await _paymentRepository.GetPaymentByOrderId(paymentDto.OrderId);
            if (existingPayment != null)
            {
                return existingPayment;
            }

            // Simulate payment processing
            var payment = new Payment
            {
                OrderId = paymentDto.OrderId,
                UserId = paymentDto.UserId,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                Status = "Completed", // In real scenario, this would come from payment gateway
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow
            };

            return await _paymentRepository.CreatePayment(payment);
        }

        public async Task<Payment?> GetPaymentStatus(int orderId)
        {
            return await _paymentRepository.GetPaymentByOrderId(orderId);
        }
    }
}