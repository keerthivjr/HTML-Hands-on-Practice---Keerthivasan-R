namespace PaymentService.Models
{
    public class CreatePaymentRequest
    {
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string PaymentFor { get; set; } = string.Empty;
        public int PaymentMonth { get; set; }
        public int PaymentYear { get; set; }
    }

    public class PaymentResponse
    {
        public int Id { get; set; }
        public string PaymentId { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string PaymentFor { get; set; } = string.Empty;
        public int PaymentMonth { get; set; }
        public int PaymentYear { get; set; }
    }
}