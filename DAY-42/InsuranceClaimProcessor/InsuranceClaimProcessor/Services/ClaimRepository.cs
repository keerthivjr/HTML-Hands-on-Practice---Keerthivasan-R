using System.Data;
using Dapper;
using InsuranceClaimProcessor.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InsuranceClaimProcessor.Services;

/// <summary>
/// Handles database operations for claims
/// </summary>
public class ClaimRepository : IClaimRepository
{
    private readonly string _connectionString;
    private readonly ILogger<ClaimRepository> _logger;

    public ClaimRepository(IConfiguration configuration, ILogger<ClaimRepository> logger)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(logger);

        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
        _logger = logger;
    }

    public async Task<IReadOnlyList<Claim>> GetPendingClaimsAsync()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = "SELECT Id, CustomerName, CustomerEmail, ClaimAmount, Description, Status, CreatedAt FROM Claims WHERE Status = 'Pending'";

            var claims = await connection.QueryAsync<Claim>(sql);
            var claimList = claims.AsList();

            _logger.LogInformation("Retrieved {Count} pending claims", claimList.Count);
            return claimList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending claims");
            throw;
        }
    }

    public async Task<bool> UpdateClaimStatusAsync(int id, string status)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = "UPDATE Claims SET Status = @Status WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Status = status, Id = id });

            _logger.LogInformation("Claim {Id} status updated to {Status}. Rows affected: {Rows}",
                id, status, rowsAffected);

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating claim {Id} status to {Status}", id, status);
            throw;
        }
    }
}