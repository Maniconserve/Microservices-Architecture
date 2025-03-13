using Organic.Web.Models;

namespace Organic.Web.Service.Iservice
{
	public interface ICouponService
	{
		Task<ResponseDto?> GetAllCouponsAsync();
		Task<ResponseDto?> GetCouponByIdAsync(int id);
		Task<ResponseDto?> GetCouponAsync(String couponCode);
		Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
		Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
		Task<ResponseDto?> DeleteCouponAsync(int id);

	}
}
