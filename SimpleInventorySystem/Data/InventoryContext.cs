using Microsoft.EntityFrameworkCore;
using SimpleInventorySystem.DataLayer.Config;
using SimpleInventorySystem.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.DataLayer
{
    public class InventoryContext : DbContext
    {
       
        public InventoryContext( DbContextOptions<InventoryContext> options):base(options) 
        {
            Console.WriteLine("DB Context");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Configuring system connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

                 => optionsBuilder.UseSqlServer("Server=.;Database=SimpleInventorySystem;Trusted_Connection=True;Encrypt=true;TrustServerCertificate=yes", sqlOptions => sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,            // Maximum number of retries
                maxRetryDelay: TimeSpan.FromSeconds(4),  // Maximum delay between retries
                errorNumbersToAdd: null      // Optional: Add specific SQL error numbers to retry on
            ));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration( new Product_Configuration());
            modelBuilder.ApplyConfiguration( new Category_Configuration());
        }
    }
}
