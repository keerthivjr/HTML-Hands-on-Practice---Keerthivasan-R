using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Update this connection string to match your SQL Server setup
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContactManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}