using InsuranceClaimProcessor.DTOs;

namespace InsuranceClaimProcessor.Services;

/// <summary>
/// Interface for email operations
/// </summary>
public interface IEmailService
{
    Task<bool> SendApprovalEmailAsync(EmailRequest emailRequest);
}