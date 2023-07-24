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
        public DbSet<ProductVariant> ProductVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Variant>()
                .HasKey(v => v.VariantId);
            modelBuilder.Entity<ProductVariant>()
                .HasKey(pv => new { pv.prodId, pv.variId });

            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Product)
                .WithMany(pv => pv.ProductVariants)
                .HasForeignKey(v => v.prodId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Variant)
                .WithMany(pv => pv.ProductVariants)
                .HasForeignKey(p => p.variId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
