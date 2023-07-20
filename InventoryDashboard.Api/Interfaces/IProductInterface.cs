using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IProductInterface
    {
       ICollection<Product> Products { get; }
    }
}
