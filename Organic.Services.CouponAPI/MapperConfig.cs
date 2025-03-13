using AutoMapper;
using Organic.Services.CouponAPI.Models;
using Organic.Services.CouponAPI.Models.Dto;

namespace Organic.Services.CouponAPI
{
	public class MapperConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CouponDto, Coupon>();
				config.CreateMap<Coupon, CouponDto>();
			});
			return mappingConfig;
		}
	}
}
