using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Repository
{
    public class InventoryRepository : IInventoryInterface
    {
        private DataContext _context;
        public InventoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool InventoryExists(int id)
        {
            return _context.Inventories.Any(p => p.InventoryId == id);
        }

        public ICollection<Inventory> GetInventories()
        {
            return _context.Inventories.OrderBy(p => p.InventoryId).ToList();
        }

        public Inventory GetInventory(int id)
        {
            return _context.Inventories.Where(p => p.InventoryId == id).FirstOrDefault();
        }
    }
}
