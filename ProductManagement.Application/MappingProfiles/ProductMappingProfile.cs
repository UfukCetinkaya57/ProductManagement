using AutoMapper;
using ProductManagement.Core.Entities;
using ProductManagement.Application.Queries;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>(); // Product'tan ProductDto'ya dönüşüm
            CreateMap<ProductDto, Product>(); // ProductDto'dan Product'a dönüşüm (eğer gerekirse)
        }
    }
}
