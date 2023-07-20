using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace InventoryDashboard.Api.Data
{
    public class DataContext :DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
           
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Option> Options { get; set; }

      

    }
}
