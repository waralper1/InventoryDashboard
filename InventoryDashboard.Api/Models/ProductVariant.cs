namespace InventoryDashboard.Api.Models
{
    public class ProductVariant
    {
        public int prodId { get; set; }
        public int variId { get; set; }
        public Product Product { get; set; }
        public Variant Variant { get; set; }
    }
}
