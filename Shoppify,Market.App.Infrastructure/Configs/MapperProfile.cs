using AutoMapper;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Service.Dtos.ProductCategories;

namespace Shoppify.Market.App.Infrastructure.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
        }
    }
}
