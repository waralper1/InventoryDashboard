using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IInventoryInterface
    {
        ICollection<Inventory> GetInventories();
        Inventory GetInventory(int id);
        bool InventoryExists(int id);
        bool CreateInventory(Inventory inventory);
        bool Save();
    }
}
