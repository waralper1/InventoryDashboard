using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryDashboard.Api.Repository
{
    public class ProductRepository : IProductInterface
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.ProductId).ToList();
        }
        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }
        //public Product GetProduct(string name)
        //{
        //    return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        //}
        public Product GetProductDiscount(int id)
        {
            _context.Products.Include(p => p.Discount).FirstOrDefault(p => p.ProductId == id);
            return _context.Products.Include(p => p.Discount).FirstOrDefault(p => p.ProductId == id);

        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.ProductId == id);
        }

        public bool CreateProduct(int discountId, int inventoryId,int categoryId, int variantId, Product product)
        {
            var variantEntity = _context.Variants.Where(v => v.VariantId == variantId).FirstOrDefault();
            var productVariant = new ProductVariant
            {
                Variant = variantEntity,
                Product = product
            };
            _context.Add(productVariant);
            _context.Add(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true :false;
        }
    }
}
