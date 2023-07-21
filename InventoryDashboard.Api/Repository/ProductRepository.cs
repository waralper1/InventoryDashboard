using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
