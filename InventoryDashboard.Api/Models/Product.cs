using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryDashboard.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int InventoryId { get; set; }
        public int DiscountId { get; set; }
        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
