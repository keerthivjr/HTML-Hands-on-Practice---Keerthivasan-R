using InsuranceClaimProcessor.Models;

namespace InsuranceClaimProcessor.Services;

/// <summary>
/// Interface for claim database operations
/// </summary>
public interface IClaimRepository
{
    Task<IReadOnlyList<Claim>> GetPendingClaimsAsync();
    Task<bool> UpdateClaimStatusAsync(int id, string status);
}