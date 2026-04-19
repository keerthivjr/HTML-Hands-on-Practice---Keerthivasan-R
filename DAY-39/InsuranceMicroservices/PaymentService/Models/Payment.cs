using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentId { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int PolicyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Completed";

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string PaymentFor { get; set; } = string.Empty;

        [Required]
        public int PaymentMonth { get; set; }

        [Required]
        public int PaymentYear { get; set; }
    }
}