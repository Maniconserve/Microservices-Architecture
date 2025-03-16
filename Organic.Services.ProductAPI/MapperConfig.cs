using AutoMapper;
using Organic.Services.ProductAPI.Models;
using Organic.Services.ProductAPI.Models.Dto;

namespace Organic.Services.ProductAPI
{
	public class MapperConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<ProductDto, Product>();
				config.CreateMap<Product, ProductDto>();
			});
			return mappingConfig;
		}
	}
}
