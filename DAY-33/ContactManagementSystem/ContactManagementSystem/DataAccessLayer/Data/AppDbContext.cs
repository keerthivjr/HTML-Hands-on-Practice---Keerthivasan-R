using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactInfo> Contacts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Company)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CompanyId);

        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Contacts)
            .HasForeignKey(c => c.DepartmentId);

        modelBuilder.Entity<Company>().HasData(
            new Company { CompanyId = 1, CompanyName = "CTS" },
            new Company { CompanyId = 2, CompanyName = "TCS" }
        );

        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "HR" },
            new Department { DepartmentId = 2, DepartmentName = "IT" }
        );
    }

    
}