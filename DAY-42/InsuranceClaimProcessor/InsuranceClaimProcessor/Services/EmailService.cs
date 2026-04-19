using InsuranceClaimProcessor.DTOs;
using Microsoft.Extensions.Logging;

namespace InsuranceClaimProcessor.Services;

/// <summary>
/// Development version - logs emails instead of sending
/// </summary>
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    public Task<bool> SendApprovalEmailAsync(EmailRequest emailRequest)
    {
        ArgumentNullException.ThrowIfNull(emailRequest);

        // Log the email instead of sending
        _logger.LogInformation("""
            [EMAIL WOULD BE SENT]
            To: {Email}
            Subject: Insurance Claim Approved
            Body: Dear {Name}, your claim for ${Amount} has been approved!
            """,
            emailRequest.ToEmail,
            emailRequest.CustomerName,
            emailRequest.ClaimAmount);

        // Simulate success
        return Task.FromResult(true);
    }
}