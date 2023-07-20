using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryDashboard.Api.Repository
{
    public class ProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext _context)
        {
            context = _context;
        }
        public ICollection<Product> GetProducts()
        {
            return context.Products.OrderBy(p => p.ProductId).ToList();
        }


    }
}
