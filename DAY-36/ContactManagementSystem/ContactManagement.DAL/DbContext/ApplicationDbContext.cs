using ContactManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.DbContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial data for Companies
            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "Microsoft" },
                new Company { CompanyId = 2, CompanyName = "Google" },
                new Company { CompanyId = 3, CompanyName = "Amazon" }
            );

            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" },
                new Department { DepartmentId = 3, DepartmentName = "Sales" }
            );

            // Seed a default Admin user (Password: admin123)
            // The hash is for "admin123" using simple Base64 encoding
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("admin123")),
                    Role = "Admin"
                }
            );
        }
    }
}