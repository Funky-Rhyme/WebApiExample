using Microsoft.EntityFrameworkCore;
using SmartCleaningAPI.Models;

namespace SmartCleaningAPI.Data
{
    public class ApiContext: DbContext
    {       
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public ApiContext(DbContextOptions<ApiContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
