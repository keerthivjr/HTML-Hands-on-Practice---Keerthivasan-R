using InsuranceClaimProcessor;
using InsuranceClaimProcessor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Add Windows Service support
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Insurance Claim Processor";
});

// Register services
builder.Services.AddSingleton<IClaimRepository, ClaimRepository>();
builder.Services.AddSingleton<IEmailService, EmailService>(); // This will now use the fake email service

// Register background service
builder.Services.AddHostedService<ClaimProcessingService>();

var host = builder.Build();
await host.RunAsync();