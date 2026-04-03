using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        // Parameterless constructor for design-time
        public AppDbContext()
        {
        }

        // Constructor with options for runtime
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if not already configured (for design-time)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContactManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships using Fluent API
            modelBuilder.Entity<ContactInfo>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContactInfo>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Contacts)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure table names
            modelBuilder.Entity<ContactInfo>().ToTable("Contacts");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Department>().ToTable("Departments");

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Companies
            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "Microsoft" },
                new Company { CompanyId = 2, CompanyName = "Google" },
                new Company { CompanyId = 3, CompanyName = "Amazon" },
                new Company { CompanyId = 4, CompanyName = "Apple" },
                new Company { CompanyId = 5, CompanyName = "Facebook" }
            );

            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" },
                new Department { DepartmentId = 3, DepartmentName = "Sales" },
                new Department { DepartmentId = 4, DepartmentName = "Marketing" },
                new Department { DepartmentId = 5, DepartmentName = "Finance" },
                new Department { DepartmentId = 6, DepartmentName = "Development" }
            );
        }
    }
}