using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blazored.Toast.Services; // Thêm namespace của Blazored.Toast
using RentalSystem.Models;
using System;
using System.Diagnostics;
using Blazored.Toast.Configuration;
using Microsoft.EntityFrameworkCore;
using RentalSystem.WebConfig.Constant;
using RentalSystem.ViewModels;
using System.Text.Json;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
namespace RentalSystem.Controllers
{
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;
		private List<AppPinVM> _pin;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(RentalSystemDBConText DbContext, IHttpContextAccessor httpContextAccessor) : base(DbContext)
		{
			_httpContextAccessor = httpContextAccessor;
		}


		public async Task<IActionResult> Index(string Address, string TypeOfServices, string price, string Area, string keyword,int TofService, int? page)
		{

			//lấy group 
			TempData["postSort"] = _BookingDbContext.AppCategory.ToList();
			var post = _BookingDbContext.AppPost
						.Include(i => i.appAddress)
						.Include(i => i.appCategory)
						.Include(i => i.appImgPosts)
						.Include(i => i.appUsers)
						.Where(i => i.PostStatus == AppPostStatus.APPROVED && i.IsPublic == true)
						.OrderByDescending(i => i.appCategory.Price)
						.AsQueryable();
			// lọc top 4
			if (!string.IsNullOrEmpty(Address))
			{
				post = post.Where(i => i.appAddress.City == Address);
				TempData["searched"] = "searched";
			}
			if (!string.IsNullOrEmpty(TypeOfServices))
			{
				post = post.Where(i => i.appTypeOfService.Name == TypeOfServices);
				TempData["searched"] = "searched";
			}
			if (!string.IsNullOrEmpty(price))
			{
				if (price == "1")
				{
					post = post.Where(i => i.RtalPrice <= 1000000);
				}
				if (price == "1-3")
				{
					post = post.Where(i => i.RtalPrice <= 3000000 && i.RtalPrice >= 1000000);
				}
				if (price == "3-5")
				{
					post = post.Where(i => i.RtalPrice <= 5000000 && i.RtalPrice >= 3000000);
				}
				if (price == "5-7")
				{
					post = post.Where(i => i.RtalPrice <= 7000000 && i.RtalPrice >= 5000000);
				}
				if (price == "7-10")
				{
					post = post.Where(i => i.RtalPrice <= 10000000 && i.RtalPrice >= 7000000);
				}
				if (price == "10-15")
				{
					post = post.Where(i => i.RtalPrice <= 15000000 && i.RtalPrice >= 10000000);
				}
				if (price == ">15")
				{
					post = post.Where(i => i.RtalPrice >= 15000000);
				}
				TempData["searched"] = "searched";
			}
			if (!string.IsNullOrEmpty(Area))
			{
				if (Area == "<20")
				{
					post = post.Where(i => i.Area < 20);
				}
				if (Area == "20-30")
				{
					post = post.Where(i => i.Area < 30 && i.Area > 20);
				}
				if (Area == "30-50")
				{
					post = post.Where(i => i.Area < 50 && i.Area > 30);
				}
				if (Area == "50-70")
				{
					post = post.Where(i => i.Area < 70 && i.Area > 50);
				}
				if (Area == "70-90")
				{
					post = post.Where(i => i.Area < 90 && i.Area > 70);
				}
				if (Area == ">90")
				{
					post = post.Where(i => i.Area > 90);
				}
				TempData["searched"] = "searched";
			}
			if (!string.IsNullOrEmpty(keyword))
			{
				if (!string.IsNullOrEmpty(keyword))
				{
					post = post.Where(i => i.appAddress.City.ToUpper().Contains(keyword.ToUpper())
										|| i.appAddress.District.ToUpper().Contains(keyword.ToUpper())
										|| i.appAddress.Wards.ToUpper().Contains(keyword.ToUpper())
										|| i.Title.ToUpper().Contains(keyword.ToUpper())
										|| i.Summary.ToUpper().Contains(keyword.ToUpper())
										|| i.Area.ToString().ToUpper().Contains(keyword.ToUpper())
									);
				}
				TempData["searched"] = "searched";
			}


			if (!string.IsNullOrEmpty(Address) && TofService > 0)
			{
				post = post.Where(i => i.appAddress.City == Address);

				post = post.Where(i => i.IdTypeOfService == TofService);

				TempData["searched"] = "searched";
			}

			// Có thể bạn quan tâm
			if (!string.IsNullOrEmpty(Address))
			{
				if (post.Count() < 7)
				{
					var cate = _BookingDbContext.AppCategory
												.OrderByDescending(i => i.Price)
												.Last();
					// interested = quan tâm đến
					TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => i.appAddress.City != Address && i.IdCategory == cate.IdCategory && i.PostStatus == AppPostStatus.APPROVED && i.IsPublic == true)
													.Take(5).ToList();
				}
			}
			if (!string.IsNullOrEmpty(TypeOfServices))
			{
				if (post.Count() < 7)
				{
					var cate = _BookingDbContext.AppCategory
												.OrderByDescending(i => i.Price)
												.Last();
					TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => i.appTypeOfService.Name != TypeOfServices && i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
				}
			}
			if (!string.IsNullOrEmpty(price))
			{
				if (post.Count() < 7)
				{
					var cate = _BookingDbContext.AppCategory
												.OrderByDescending(i => i.Price)
												.Last();
					if (price == "1")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 1000000 || i.RtalPrice <= 3000000) && i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == "1-3")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 3000000 && i.RtalPrice <= 5000000)
																|| (i.RtalPrice <= 1000000)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == "3-5")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 5000000 && i.RtalPrice <= 7000000)
																|| (i.RtalPrice <= 3000000 && i.RtalPrice >= 1000000)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == "5-7")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 7000000 && i.RtalPrice <= 10000000)
																|| (i.RtalPrice <= 5000000 && i.RtalPrice >= 3000000)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == "7-10")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 10000000 && i.RtalPrice <= 15000000)
																|| (i.RtalPrice <= 7000000 && i.RtalPrice >= 5000000)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == "10-15")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.RtalPrice >= 15000000)
																|| (i.RtalPrice <= 10000000 && i.RtalPrice >= 7000000)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (price == ">15")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
												.Include(i => i.appAddress)
												.Include(i => i.appCategory)
												.Include(i => i.appImgPosts)
												.Include(i => i.appUsers)
												.Where(i => (i.RtalPrice <= 15000000 && i.RtalPrice >= 10000000)
															&& i.IdCategory == cate.IdCategory)
												.Take(5).ToList();
						}
				}
			}
			if (!string.IsNullOrEmpty(Area))
			{
				if (post.Count() < 7)
				{
					var cate = _BookingDbContext.AppCategory
												.OrderByDescending(i => i.Price)
												.Last();
					if (Area == "<20")
					{                       // interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area > 20 && i.Area < 30)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (Area == "20-30")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area > 30 && i.Area < 50)
																|| (i.Area < 20)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (Area == "30-50")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area > 50 && i.Area < 70)
																|| (i.Area < 30 && i.Area > 20)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (Area == "50-70")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area > 70 && i.Area < 90)
																|| (i.Area < 50 && i.Area > 30)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (Area == "70-90")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area > 90)
																|| (i.Area < 70 && i.Area > 50)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
					if (Area == ">90")
					{
						// interested = quan tâm đến
						TempData["interested"] = _BookingDbContext.AppPost
													.Include(i => i.appAddress)
													.Include(i => i.appCategory)
													.Include(i => i.appImgPosts)
													.Include(i => i.appUsers)
													.Where(i => (i.Area < 90 && i.Area > 70)
																&& i.IdCategory == cate.IdCategory)
													.Take(5).ToList();
					}
				}
			}
			if (!string.IsNullOrEmpty(keyword))
			{
				if (post.Count() < 7)
				{
					var cate = _BookingDbContext.AppCategory
												.OrderByDescending(i => i.Price)
												.Last();
					TempData["interested"] = _BookingDbContext.AppPost
												.Include(i => i.appAddress)
												.Include(i => i.appCategory)
												.Include(i => i.appImgPosts)
												.Include(i => i.appUsers)
												.Where(i => i.IdCategory == cate.IdCategory)
												.Take(5).ToList();
				}
			}

			// Lấy bài ghim từ Cookies để hiển thị bài Ghim 
			var _pin = GetPostFromCookie();
			TempData["Pin"] = _pin;

			return View(post.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult Privacy()
		{
			return View();
		}



		private List<AppPinVM> GetPostFromCookie()
		{
			var cartJson = _httpContextAccessor.HttpContext.Request.Cookies[AppPinKeyCookies.KEY_COOKIES];
			return string.IsNullOrEmpty(cartJson) ? new List<AppPinVM>() : JsonSerializer.Deserialize<List<AppPinVM>>(cartJson);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
