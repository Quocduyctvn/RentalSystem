using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminNewsController : AdminControllerBase
	{
		public AdminNewsController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		public IActionResult Index(string? keyword, int? page)
		{
			var cateNews = _BookingDbContext.AppNews
										.Include(i => i.appCategoryNews)
										.AsQueryable();
			if (!string.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				cateNews = cateNews.Where(i => i.Title.ToUpper().Contains(keywordUpper) || i.Summary.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}
			//===================PHAN QUYEN=====================
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				var user = _BookingDbContext.AppUser
									.Include(i => i.appRole)
									.FirstOrDefault(i => i.IdUser == userId);
				var IdRole = user.IdRole;
				ViewBag.RolePer = _BookingDbContext.AppRolePermission
									.Where(i => i.IdRole == IdRole)
									.ToList();

			}



			ViewBag.ListNews = _BookingDbContext.AppNews.ToList();
			return View(cateNews.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult Create()
		{
			ViewBag.ListCateNews = _BookingDbContext.AppCategoryNews.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Create(AddOrUpdateNews model, [FromServices] IWebHostEnvironment envi)
		{
			if (model.FormFileImg == null)
			{
				return RedirectToAction("Create");
			}
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			var user = new AppUsers();
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				user = _BookingDbContext.AppUser
									.Include(i => i.appRole)
									.FirstOrDefault(i => i.IdUser == userId);
			}
			var news = new AppNews();
			if (user != null)
			{
				news.Title = model.Title;
				news.Summary = model.Summary;
				news.Content = model.Content;
				news.IdCateNews = model.IdCateNews;
				DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
				news.CreateDate = vietnamTime;
				news.CoverImg = UploadFile(model.FormFileImg, envi.WebRootPath);
				news.IdUser = user.IdUser;
			}
			_BookingDbContext.Add(news);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Thêm mới thành công";
			return RedirectToAction("Index");
		}


		public IActionResult Update(int id)
		{
			ViewBag.ListCateNews = _BookingDbContext.AppCategoryNews.ToList();
			var news = _BookingDbContext.AppNews.Find(id);
			var model = new AddOrUpdateNews();
			model.Title = news.Title;
			model.Summary = news.Summary;
			model.Content = news.Content;
			model.IdCateNews = news.IdCateNews;
			model.CoverImg = news.CoverImg;
			return View(model);
		}

		[HttpPost]
		public IActionResult Update(int id, AddOrUpdateNews model, [FromServices] IWebHostEnvironment envi)
		{
			var news = _BookingDbContext.AppNews.Find(id);
			if (news == null)
			{
				TempData["ToastMessageWrg"] = "Truy cập không hợp lệ";
				return RedirectToAction("Index");
			}
			news.Title = model.Title;
			news.Summary = model.Summary;
			news.Content = model.Content;
			news.IdCateNews = model.IdCateNews;
			DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
			news.CreateDate = vietnamTime;
			if (model.FormFileImg != null)
			{
				news.CoverImg = UploadFile(model.FormFileImg, envi.WebRootPath);

				var filePath = Path.Combine(envi.WebRootPath, model.CoverImg.TrimStart('/'));
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}
			_BookingDbContext.Update(news);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật thành công";
			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult Delete(string id)
		{
			int Id = int.Parse(id);
			var news = _BookingDbContext.AppNews.FirstOrDefault(i => i.IdNews == Id);
			if (news == null)
			{
				TempData["ToastMessageScs"] = "Tin tức không tồn tại";
				return RedirectToAction("Index");
			}

			_BookingDbContext.Remove(news);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Xóa thành công";
			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult DeleteList(List<int> ids)
		{
			if (ids == null || ids.Count == 0)
			{
				return BadRequest("Không có mục nào được chọn để xóa.");
			}
			try
			{
				var News = _BookingDbContext.AppNews
					.Where(s => ids.Contains(s.IdNews))
					.ToList();
				foreach (var item in News)
				{
					_BookingDbContext.AppNews.Remove(item);
				}

				_BookingDbContext.SaveChanges();

				TempData["ToastMessageScs"] = $"Đã xóa thành công [{News.Count()}] item";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
			}
		}


		private string UploadFile(IFormFile file, string webRootPath)
		{
			var fName = file.FileName;
			fName = Path.GetFileNameWithoutExtension(fName)
				+ DateTime.Now.Ticks
				+ Path.GetExtension(fName);

			var directoryPath = Path.Combine(webRootPath, "Image", "News");
			Directory.CreateDirectory(directoryPath); // Đảm bảo thư mục tồn tại

			var filePath = Path.Combine(directoryPath, fName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			var relativePath = "/Image/News/" + fName;
			return relativePath;
		}
	}
}
