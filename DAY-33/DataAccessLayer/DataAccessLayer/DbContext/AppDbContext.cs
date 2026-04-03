using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Explicitly define Primary Keys (Fixes the current error)
            modelBuilder.Entity<ContactInfo>().HasKey(c => c.ContactId);
            modelBuilder.Entity<Company>().HasKey(c => c.CompanyId);
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);

            // 2. Relationship: One Company → Many Contacts
            modelBuilder.Entity<ContactInfo>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Relationship: One Department → Many Contacts
            modelBuilder.Entity<ContactInfo>()
                .HasOne(d => d.Department)
                .WithMany(d => d.Contacts)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Seed some initial data
            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "ABC Infotech" },
                new Company { CompanyId = 2, CompanyName = "XYZ Solutions" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" },
                new Department { DepartmentId = 3, DepartmentName = "Sales" }
            );
        }
    }
}
