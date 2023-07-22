using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IDiscountInterface
    {
        ICollection<Discount> GetDiscounts();
        Discount GetDiscount(int id);
        bool DiscountExists(int id);
        bool CreateDiscount(Discount discount);
        bool Save();
    }
}
