﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Organic.Web.Models;
using Organic.Web.Service.Iservice;

namespace Organic.Web.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		//private readonly IOrderService _orderService;
		public CartController(ICartService cartService)
		{
			_cartService = cartService;
			//_orderService = orderService;
		}

		[Authorize]
		public async Task<IActionResult> CartIndex()
		{
			return View(await LoadCartDtoBasedOnLoggedInUser());
		}

		[Authorize]
		public async Task<IActionResult> Checkout()
		{
			return View(await LoadCartDtoBasedOnLoggedInUser());
		}

		public async Task<IActionResult> Remove(int cartDetailsId)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
			ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);
			if (response != null & response.IsSuccess)
			{
				TempData["success"] = "Cart updated successfully";
				return RedirectToAction(nameof(CartIndex));
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
		{

			ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
			if (response != null & response.IsSuccess)
			{
				TempData["success"] = "Cart updated successfully";
				return RedirectToAction(nameof(CartIndex));
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> EmailCart(CartDto cartDto)
		{
			CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
			cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
			ResponseDto? response = await _cartService.EmailCart(cart);
			if (response != null & response.IsSuccess)
			{
				TempData["success"] = "Email will be processed and sent shortly.";
				return RedirectToAction(nameof(CartIndex));
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
		{
			cartDto.CartHeader.CouponCode = "";
			ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
			if (response != null & response.IsSuccess)
			{
				TempData["success"] = "Cart updated successfully";
				return RedirectToAction(nameof(CartIndex));
			}
			return View();
		}


		private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
			ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);
			if (response != null & response.IsSuccess)
			{
				CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
				return cartDto;
			}
			return new CartDto();
		}
	}
}
