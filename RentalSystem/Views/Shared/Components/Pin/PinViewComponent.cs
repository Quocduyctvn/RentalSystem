using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using RentalSystem.ViewModels;
using RentalSystem.WebConfig.Constant;
using System.Text.Json;

namespace RentalSystem.Views.Shared.Components.Pin
{
	public class PinViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;
		private List<AppPinVM> _cart;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public PinViewComponent(RentalSystemDBConText rentallSystem, IHttpContextAccessor httpContextAccessor)
		{
			_BookingDbContext = rentallSystem;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var _cart = GetPostFromCookie();

			ViewBag.Quantity = _cart.Count();
			return View();
		}
		private List<AppPinVM> GetPostFromCookie()
		{
			var cartJson = _httpContextAccessor.HttpContext.Request.Cookies[AppPinKeyCookies.KEY_COOKIES];
			return string.IsNullOrEmpty(cartJson) ? new List<AppPinVM>() : JsonSerializer.Deserialize<List<AppPinVM>>(cartJson);
		}

	}

}
