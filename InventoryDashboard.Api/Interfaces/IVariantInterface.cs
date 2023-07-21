using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IVariantInterface
    {
        ICollection<Variant> GetVariants();
        Variant GetVariant(int id);
        bool VariantExists(int id);
    }
}
