using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryDashboard.Api.Models
{
    public class Variant
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public double Price { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("OptionId")]
        public Option Option { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
