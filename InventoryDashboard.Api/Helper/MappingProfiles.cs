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

            CreateMap<ProductDto,Product >();
            CreateMap<CategoryDto, Category>();
            CreateMap<InventoryDto,Inventory >();
            CreateMap<VariantDto,Variant >();
            CreateMap<OptionDto,Option >();
            CreateMap<DiscountDto,Discount >();
        }
    }
}
