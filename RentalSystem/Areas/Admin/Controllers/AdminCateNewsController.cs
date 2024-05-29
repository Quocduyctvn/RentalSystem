using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminCateNewsController : AdminControllerBase
	{
		public AdminCateNewsController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		public IActionResult Index(string? keyword, int? page)
		{
			var cateNews = _BookingDbContext.AppCategoryNews.AsQueryable();
			if (!string.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				cateNews = cateNews.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.Desc.ToUpper().Contains(keyword));
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



			ViewBag.ListCateNews = _BookingDbContext.AppCategoryNews.ToList();
			return View(cateNews.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		[HttpPost]
		public IActionResult AddCateNews(string Name, string Desc)
		{
			if (string.IsNullOrEmpty(Name))
			{
				TempData["ToastMessageWrg"] = "Tên thể loại tin không được để trống";
				return RedirectToAction("Index");
			}
			if (string.IsNullOrEmpty(Desc))
			{
				TempData["ToastMessageWrg"] = "Mô tả không được để trống";
				return RedirectToAction("Index");
			}
			var exit = _BookingDbContext.AppCategoryNews.Any(i => i.Name == Name);
			if (exit)
			{
				TempData["ToastMessageScs"] = "Tên thể loại tin đã tồn tại";
				return RedirectToAction("Index");
			}
			AppCategoryNews CateNews = new AppCategoryNews();
			CateNews.Name = Name;
			CateNews.Desc = Desc;
			CateNews.CreatedDate = DateTime.UtcNow;

			_BookingDbContext.AppCategoryNews.Add(CateNews);
			_BookingDbContext.SaveChanges();

			TempData["ToastMessageScs"] = "Thêm thể loại tin thành công";
			return RedirectToAction("Index");
		}
		

		public IActionResult Update(string name, string desc, string id)
		{
			int Id = int.Parse(id);
			var cate = _BookingDbContext.AppCategoryNews.FirstOrDefault(i => i.IdCateNews == Id);
			if(cate == null)
			{
				TempData["ToastMessageScs"] = "Tên thể loại tin không tồn tại";
				return RedirectToAction("Index");
			}
			if (string.IsNullOrEmpty(name))
			{
				TempData["ToastMessageWrg"] = "Tên thể loại tin không được để trống";
				return RedirectToAction("Index");
			}
			if (string.IsNullOrEmpty(desc))
			{
				TempData["ToastMessageWrg"] = "Mô tả không được để trống";
				return RedirectToAction("Index");
			}
			var exit = _BookingDbContext.AppCategoryNews.Any(i => i.Name == name);
			if (exit)
			{
				TempData["ToastMessageScs"] = "Tên thể loại tin đã tồn tại";
				return RedirectToAction("Index");
			}
			cate.Name = name;

			cate.Desc = desc;
			cate.CreatedDate = DateTime.UtcNow;
			_BookingDbContext.Update(cate);
			_BookingDbContext.SaveChanges();

			TempData["ToastMessageScs"] = "Cập nhật thể loại tin thành công";
			return RedirectToAction("Index");
		}

		public IActionResult Delete(string id)
		{
			int Id = int.Parse(id);
			var cate = _BookingDbContext.AppCategoryNews.FirstOrDefault(i => i.IdCateNews == Id);
			if (cate == null)
			{
				TempData["ToastMessageScs"] = "Tên thể loại tin không tồn tại";
				return RedirectToAction("Index");
			}
			_BookingDbContext.Remove(cate);
			_BookingDbContext.SaveChanges();

			TempData["ToastMessageScs"] = "Thêm thể loại tin thành công";
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
				var CateNews = _BookingDbContext.AppCategoryNews
					.Where(s => ids.Contains(s.IdCateNews))
					.ToList();
				foreach (var cateNews in CateNews)
				{
					_BookingDbContext.AppCategoryNews.Remove(cateNews);
				}

				_BookingDbContext.SaveChanges();

				TempData["ToastMessageScs"] = $"Đã xóa thành công [{CateNews.Count()}] item";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
			}
		}
	}
}
