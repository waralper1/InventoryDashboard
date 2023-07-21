namespace InventoryDashboard.Api.Dto
{
    public class VariantDto
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public double Price { get; set; }
    }
}
