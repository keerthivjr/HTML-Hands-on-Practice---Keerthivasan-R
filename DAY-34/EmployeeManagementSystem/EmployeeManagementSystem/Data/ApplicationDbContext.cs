using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@company.com",
                    Department = "IT",
                    Salary = 75000,
                    JobStatus = "Active",
                    HireDate = new DateTime(2023, 1, 15),
                    PhoneNumber = "1234567890",
                    Address = "123 Main St"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane@company.com",
                    Department = "HR",
                    Salary = 65000,
                    JobStatus = "Active",
                    HireDate = new DateTime(2023, 3, 20),
                    PhoneNumber = "0987654321",
                    Address = "456 Oak Ave"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Bob Johnson",
                    Email = "bob@company.com",
                    Department = "IT",
                    Salary = 85000,
                    JobStatus = "Active",
                    HireDate = new DateTime(2022, 6, 10),
                    PhoneNumber = "5555555555",
                    Address = "789 Pine Rd"
                }
            );
        }
    }
}