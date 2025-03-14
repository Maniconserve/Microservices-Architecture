using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organic.Services.CouponAPI.Data;
using Organic.Services.CouponAPI.Models;
using Organic.Services.CouponAPI.Models.Dto;

namespace Organic.Services.CouponAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CouponAPIController : ControllerBase
	{
		private AppDbContext _appDbContext;
		private ResponseDto _response;
		private IMapper _mapper;
		public CouponAPIController(AppDbContext appDbContext,IMapper mapper)
		{
			_appDbContext = appDbContext;
			_response = new ResponseDto();
			_mapper = mapper;
		}
		// GET: api/<CouponAPIController>
		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				_response.Result = _mapper.Map<List<CouponDto>>(_appDbContext.coupons.ToList());
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		// GET api/<CouponAPIController>/5
		[HttpGet("{id}")]
		public ResponseDto Get(int id)
		{
			try
			{
				_response.Result =  _mapper.Map<CouponDto>(_appDbContext.coupons.First(coup => coup.CouponId == id));
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpGet("GetByCode/{couponCode}")]
		public ResponseDto GetByCode(string couponCode)
		{
			try
			{
				_response.Result = _mapper.Map<CouponDto>(_appDbContext.coupons.First(coup => coup.CouponCode.ToLower() == couponCode.ToLower()));
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		// POST api/<CouponAPIController>
		[HttpPost]
		public ResponseDto Post([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(couponDto);
				_appDbContext.coupons.Add(coupon);
				_appDbContext.SaveChanges();
				_response.Result = couponDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		// PUT api/<CouponAPIController>/5
		[HttpPut]
		public ResponseDto Put([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(couponDto);
				_appDbContext.coupons.Update(coupon);
				_appDbContext.SaveChanges();
				_response.Result = couponDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		// DELETE api/<CouponAPIController>/5
		[HttpDelete("{id}")]
		public ResponseDto Delete(int id)
		{
			try
			{
				Coupon coupon = _appDbContext.coupons.First(coup => coup.CouponId == id);
				_appDbContext.Remove(coupon);
				_appDbContext.SaveChanges();
			}
			catch (Exception ex) {
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}
