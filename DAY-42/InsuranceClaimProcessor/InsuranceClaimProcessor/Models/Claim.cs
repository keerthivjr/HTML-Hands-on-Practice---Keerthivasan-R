namespace InsuranceClaimProcessor.Models;

/// <summary>
/// Represents an insurance claim in the database
/// </summary>
public class Claim
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;  // Non-nullable with default
    public string CustomerEmail { get; set; } = string.Empty; // Non-nullable with default
    public decimal ClaimAmount { get; set; }
    public string? Description { get; set; }  // Nullable (optional field)
    public string Status { get; set; } = "Pending";  // Non-nullable with default
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Constructor for required fields
    public Claim(string customerName, string customerEmail, decimal claimAmount)
    {
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        ClaimAmount = claimAmount;
    }

    // Parameterless constructor for Dapper
    public Claim() { }
}