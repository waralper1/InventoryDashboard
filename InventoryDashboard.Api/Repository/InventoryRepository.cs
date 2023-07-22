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
        public bool CreateInventory(Inventory inventory)
        {
            // Notes to self
            //Change Tracker
            //Tracker states: add, updating, modifying
            //connected - disconnected
            //Entity.State.Added 
            //
            _context.Add(inventory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
