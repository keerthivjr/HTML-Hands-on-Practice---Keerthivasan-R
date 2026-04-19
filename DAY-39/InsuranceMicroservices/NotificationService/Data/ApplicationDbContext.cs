using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

namespace NotificationService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Notification entity
            modelBuilder.Entity<Notification>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Properties configuration
                entity.Property(e => e.NotificationId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(20);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(500);

                // Indexes for better query performance
                entity.HasIndex(e => e.NotificationId)
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.Status);

                entity.HasIndex(e => new { e.CustomerId, e.CreatedAt });

                entity.HasIndex(e => new { e.Status, e.CreatedAt });
            });
        }
    }
}