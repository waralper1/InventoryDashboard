using AutoMapper;
using InventoryDashboard.Api.Dto;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Inventory, InventoryDto>();
            CreateMap<Variant, VariantDto>();
            CreateMap<Option, OptionDto>();
            CreateMap<Discount, DiscountDto>();
        }
    }
}
