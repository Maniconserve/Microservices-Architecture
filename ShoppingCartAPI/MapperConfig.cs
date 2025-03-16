using AutoMapper;
using ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Models;

namespace Organic.Services.ShoppingCartAPI
{
	public class MapperConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
				config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}
