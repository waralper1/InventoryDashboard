using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(p => p.CategoryId == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(p => p.CategoryId).ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(p => p.CategoryId == id).FirstOrDefault();
        }
    }
}
