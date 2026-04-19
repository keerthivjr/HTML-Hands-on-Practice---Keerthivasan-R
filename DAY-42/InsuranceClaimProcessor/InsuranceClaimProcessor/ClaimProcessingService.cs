using System.Runtime.CompilerServices;
using InsuranceClaimProcessor.DTOs;
using InsuranceClaimProcessor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InsuranceClaimProcessor;

/// <summary>
/// Background service that processes pending insurance claims every 10 minutes
/// </summary>
public sealed class ClaimProcessingService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ClaimProcessingService> _logger;
    private readonly TimeSpan _processingInterval = TimeSpan.FromMinutes(10);

    public ClaimProcessingService(IServiceProvider serviceProvider, ILogger<ClaimProcessingService> logger)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(logger);

        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Claim Processing Service started at {StartTime}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Checking for pending claims at {CheckTime}", DateTimeOffset.Now);

            try
            {
                await ProcessPendingClaimsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing claims");
            }

            _logger.LogInformation("Next check scheduled in {Interval} minutes", _processingInterval.TotalMinutes);
            await Task.Delay(_processingInterval, stoppingToken);
        }

        _logger.LogInformation("Claim Processing Service stopped at {StopTime}", DateTimeOffset.Now);
    }

    private async Task ProcessPendingClaimsAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var claimRepository = scope.ServiceProvider.GetRequiredService<IClaimRepository>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var pendingClaims = await claimRepository.GetPendingClaimsAsync();

        if (pendingClaims.Count == 0)
        {
            _logger.LogInformation("No pending claims found");
            return;
        }

        _logger.LogInformation("Processing {Count} pending claims", pendingClaims.Count);

        foreach (var claim in pendingClaims)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            _logger.LogInformation("Processing claim {ClaimId} for {CustomerName} (Amount: ${Amount})",
                claim.Id, claim.CustomerName, claim.ClaimAmount);

            // Send email notification
            var emailRequest = new EmailRequest(claim.CustomerEmail, claim.CustomerName, claim.ClaimAmount);
            var emailSent = await emailService.SendApprovalEmailAsync(emailRequest);

            if (emailSent)
            {
                // Update claim status
                var updated = await claimRepository.UpdateClaimStatusAsync(claim.Id, "Processed");

                if (updated)
                {
                    _logger.LogInformation("Successfully processed claim {ClaimId}", claim.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to update status for claim {ClaimId}", claim.Id);
                }
            }
            else
            {
                _logger.LogWarning("Email sending failed for claim {ClaimId}, status not updated", claim.Id);
            }
        }

        _logger.LogInformation("Completed processing batch of {Count} claims", pendingClaims.Count);
    }
}