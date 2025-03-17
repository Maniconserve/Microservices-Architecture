using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Organic.Services.ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Service.IService;

namespace ShoppingCartAPI.Service
{
	public class CouponService : ICouponService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IHttpContextAccessor _contextAccessor;

		public CouponService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
		{
			_httpClientFactory = clientFactory;
			_contextAccessor = contextAccessor;
		}

		public async Task<CouponDto> GetCoupon(string couponCode)
		{
			var client = _httpClientFactory.CreateClient("Coupon");
			if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JWTToken", out var token) == true)
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await client.GetAsync($"/api/CouponAPI/GetByCode/{couponCode}");
			var apiContet = await response.Content.ReadAsStringAsync();
			var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
			if (resp != null && resp.IsSuccess)
			{
				return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
			}
			return new CouponDto();
		}
	}
}