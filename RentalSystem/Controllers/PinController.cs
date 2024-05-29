using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using RentalSystem.ViewModels;
using RentalSystem.WebConfig.Constant;
using System.Text.Json;
using System.Web.Helpers;

namespace RentalSystem.Controllers
{
	public class PinController : ControllerBase
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public PinController(IHttpContextAccessor httpContextAccessor, RentalSystemDBConText DbContext) : base(DbContext)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			var _pin = GetPostFromCookie();

			// Lấy group 
			TempData["postSort"] = _BookingDbContext.AppCategory.ToList();

			// Lấy các ID từ danh sách _pin
			var pinnedPostIds = _pin.Select(item => item.IdPost).ToList();

			var post = _BookingDbContext.AppPost
						.Include(i => i.appAddress)
						.Include(i => i.appCategory)
						.Include(i => i.appImgPosts)
						.Include(i => i.appUsers)
						.Where(i => (i.PostStatus == AppPostStatus.APPROVED || i.PostStatus == AppPostStatus.TRADING) && pinnedPostIds.Contains(i.IdPost))
						.OrderByDescending(i => i.appCategory.Price)
						.AsQueryable();

			TempData["Pin"] = _pin;

			return View(post.ToList());
		}




		public IActionResult PinCookies(int id)
		{
			var Cookies = GetPostFromCookie();

			var item = Cookies.FirstOrDefault(i => i.IdPost == id);  // timf kiếm trong cookies 
			if (item == null)
			{
				var post = _BookingDbContext.AppPost.FirstOrDefault(i => i.IdPost == id);
				item = new AppPinVM
				{
					IdPost = post.IdPost,
					Name = post.Title,
					CreateDate = DateTime.UtcNow
				};
				Cookies.Add(item);
			}
			else
			{
				Cookies.Remove(item);
			}

			SavePostToCookie(Cookies);

			// XÓA COOKIES HIỆN TẠI
			//_httpContextAccessor.HttpContext.Response.Cookies.Delete(AppPinKeyCookies.KEY_COOKIES);
			//_httpContextAccessor.HttpContext.Response.Cookies.Append(AppPinKeyCookies.KEY_COOKIES, "", new CookieOptions
			//{
			//	Expires = DateTime.Now.AddDays(-1)
			//});


			return RedirectToAction("Index", "Home");
		}

		private List<AppPinVM> GetPostFromCookie()
		{
			var cartJson = _httpContextAccessor.HttpContext.Request.Cookies[AppPinKeyCookies.KEY_COOKIES];
			return string.IsNullOrEmpty(cartJson) ? new List<AppPinVM>() : JsonSerializer.Deserialize<List<AppPinVM>>(cartJson);
		}

		private void SavePostToCookie(List<AppPinVM> cart)
		{
			var options = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(30) // Thiết lập thời gian sống của cookie là 30 ngày
			};

			var cartJson = JsonSerializer.Serialize(cart);
			_httpContextAccessor.HttpContext.Response.Cookies.Append(AppPinKeyCookies.KEY_COOKIES, cartJson, options);
		}
	}
}
