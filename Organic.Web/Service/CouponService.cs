﻿using Organic.Web.Models;
using Organic.Web.Service.Iservice;
using Organic.Web.Utility;

namespace Organic.Web.Service
{
	public class CouponService : ICouponService
	{
		private IBaseService _baseService;
		public CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = couponDto,
				Url = SD.CouponApiBase + "/api/CouponAPI"
			});
		}

		public async Task<ResponseDto?> DeleteCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.CouponApiBase + "/api/CouponAPI/" + id,
			});
		}

		public async Task<ResponseDto?> GetAllCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponApiBase + "/api/CouponAPI",
			});
		}

		public async Task<ResponseDto?> GetCouponAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponApiBase + "/api/CouponAPI/GetByCode/"+couponCode,
			});
		}

		public async Task<ResponseDto?> GetCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponApiBase + "/api/CouponAPI/"+id,
			});
		}

		public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.PUT,
				Data = couponDto,
				Url = SD.CouponApiBase + "/api/CouponAPI"
			});
		}
	}
}
