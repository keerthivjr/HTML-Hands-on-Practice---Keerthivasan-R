using ContactAppDI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactAppDI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
