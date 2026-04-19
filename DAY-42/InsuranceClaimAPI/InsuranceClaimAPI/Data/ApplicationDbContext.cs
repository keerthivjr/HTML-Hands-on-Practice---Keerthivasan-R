using InsuranceClaimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PolicyHolder> PolicyHolders { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        public DbSet<ClaimDocument> ClaimDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<InsuranceClaim>()
                .HasOne(c => c.PolicyHolder)
                .WithMany(p => p.Claims)
                .HasForeignKey(c => c.PolicyHolderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints
            modelBuilder.Entity<PolicyHolder>()
                .HasIndex(p => p.PolicyNumber)
                .IsUnique();

            modelBuilder.Entity<InsuranceClaim>()
                .HasIndex(c => c.ClaimNumber)
                .IsUnique();

            // Seed default admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@insurance.com",
                    PasswordHash = "admin123",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "claimsprocessor",
                    Email = "processor@insurance.com",
                    PasswordHash = "processor123",
                    Role = "ClaimsProcessor",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );

            // Seed sample policy holder
            modelBuilder.Entity<PolicyHolder>().HasData(
                new PolicyHolder
                {
                    Id = 1,
                    PolicyNumber = "POL-2024-001",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com",
                    PhoneNumber = "555-0101",
                    Address = "123 Main St, Anytown, AN 12345",
                    PolicyType = "Health",
                    CoverageAmount = 500000,
                    PolicyStartDate = new DateTime(2024, 1, 1),
                    PolicyEndDate = new DateTime(2025, 1, 1),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}