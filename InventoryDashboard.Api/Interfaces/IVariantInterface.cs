using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IVariantInterface
    {
        ICollection<Variant> GetVariants();
        Variant GetVariant(int id);
        bool VariantExists(int id);
        bool CreateVariant(int productId, int optionId, Variant variant);
        bool UpdateVariant(int variantId, Variant variant);
        bool DeleteVariant(Variant variant);
        bool Save();
    }
}
