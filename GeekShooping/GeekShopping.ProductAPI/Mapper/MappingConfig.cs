using AutoMapper;
using GeekShopping.ProductAPI.Data.Dto;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Mapper
{
    public class MappingConfig
    {

        public static MapperConfiguration ResgisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config=>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
