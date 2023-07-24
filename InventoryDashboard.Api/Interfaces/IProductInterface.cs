using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IProductInterface
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int id);
        //Product GetProduct(string name);
        Product GetProductDiscount(int id);
        bool ProductExists(int id);
        bool CreateProduct(int discountId,int inventoryId,int categoryId,int variantId, Product product);
        bool Save();
         
    }
}
