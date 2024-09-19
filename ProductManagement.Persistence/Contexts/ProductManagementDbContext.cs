using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;

namespace ProductManagement.Persistence.Contexts
{
    public class ProductManagementDbContext : IdentityDbContext
    {
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        // Log tablosu için DbSet
        public DbSet<LogEntry> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Log tablosu konfigürasyonu (isteğe bağlı)
        }
    }
   
}
