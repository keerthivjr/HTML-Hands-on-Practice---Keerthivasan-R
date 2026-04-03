using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Email as Unique
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Seed some initial data
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    Name = "John Doe",
                    Email = "john@example.com",
                    Phone = "9876543210",
                    Course = "Computer Science",
                    AdmissionDate = new DateTime(2024, 1, 15)
                },
                new Student
                {
                    StudentId = 2,
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    Phone = "9876543211",
                    Course = "Mathematics",
                    AdmissionDate = new DateTime(2024, 1, 20)
                }
            );
        }
    }
}