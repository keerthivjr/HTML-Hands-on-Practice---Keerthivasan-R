namespace InsuranceClaimProcessor.DTOs;

/// <summary>
/// Data Transfer Object for email requests
/// </summary>
public class EmailRequest
{
    public string ToEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal ClaimAmount { get; set; }

    // Constructor for required fields
    public EmailRequest(string toEmail, string customerName, decimal claimAmount)
    {
        ToEmail = toEmail;
        CustomerName = customerName;
        ClaimAmount = claimAmount;
    }

    // Parameterless constructor for serialization
    public EmailRequest() { }
}
