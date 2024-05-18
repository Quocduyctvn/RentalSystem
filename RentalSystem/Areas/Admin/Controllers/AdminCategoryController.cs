using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
    public class AdminCategoryController : AdminControllerBase
	{
		public AdminCategoryController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		[Route("quan-ly-dich-vu")]
		public IActionResult Index(string? keyword, int? page)
        {
			var Cate = _BookingDbContext.AppCategory.AsQueryable();
			if (!string.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				Cate = Cate.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.Advantage.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}
			ViewBag.ListCategory = _BookingDbContext.AppCategory.ToList();

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
			return View(Cate.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
        }

		public IActionResult AddOrUpCategory(int? id)
		{
			var cate = new AddCateVM();
			bool isEditing = false; // Đánh dấu xem đang sửa hay thêm mới

			if (id.HasValue && id > 0)
			{
				ViewBag.ActionName = "Cập nhật Gói tin";
				var oldCate = _BookingDbContext.AppCategory.FirstOrDefault(i => i.IdCategory == id);
				if (oldCate != null)
				{
					isEditing = true;
					cate.PathString = oldCate.Path;
					cate.Name = oldCate.Name;
					cate.CreatedDate = oldCate.CreatedDate;
					cate.Object = oldCate.Object;
					cate.DisplayNumberPhone = oldCate.DisplayNumberPhone;
					cate.Advantage = oldCate.Advantage; // ưu điểm
					cate.Price = Convert.ToDouble(oldCate.Price);
					cate.AutomaticBrowsing = oldCate.AutomaticBrowsing;
					cate.TitleColor = oldCate.TitleColor;
				}
			}
			else
			{
				ViewBag.ActionName = "Thêm mới gói tin";
			}
			ViewBag.IsEditing = isEditing; // Truyền biến isEditing sang view
			return View(cate);
		}


		[HttpPost]
		public IActionResult AddOrUpCategory(int? id, AddCateVM addCateVM, [FromServices] IWebHostEnvironment envi)
		{
			//if(!ModelState.IsValid)
			//{
			//	TempData["ToastMessageErr"] = "Lỗi dữ liệu - vui lòng nhập đúng cú pháp";
			//	return View(addCateVM);
			//}
			var cate = new AppCategory();
			if (id != null)
			{
				cate = _BookingDbContext.AppCategory.FirstOrDefault(i => i.IdCategory == id);
			}
			if(addCateVM.Path == null)  // nếu admin không đổi ảnh
			{
				cate.Path = cate.Path;
			}
			else
			{
				cate.Path = UploadFile(addCateVM.Path, envi.WebRootPath);
			}
			cate.Name = addCateVM.Name;
			cate.CreatedDate = DateTime.Now;
			cate.Object = addCateVM.Object;
			cate.DisplayNumberPhone = addCateVM.DisplayNumberPhone;
			cate.Advantage = addCateVM.Advantage; // ưu điểm
			cate.Price = Convert.ToDouble(addCateVM.Price);
			cate.AutomaticBrowsing = addCateVM.AutomaticBrowsing;
			cate.TitleColor = addCateVM.TitleColor;

			if (id.HasValue && id > 0)
			{
				_BookingDbContext.AppCategory.Update(cate);
				TempData["ToastMessageScs"] = "Cập nhật gói tin thành công";
			}
			else
			{
				_BookingDbContext.AppCategory.Add(cate);
				TempData["ToastMessageScs"] = "Thêm gói tin thành công";
			}
			
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminCategory");
		}

		public IActionResult DeleteCategory(string id, [FromServices] IWebHostEnvironment envi)
		{
			int Id = int.Parse(id);

			var exit = _BookingDbContext.AppCategory.Any(i => i.IdCategory == Id);
			if (!exit)
			{
				TempData["ToastMessageScs"] = "Lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}
			var DeleteCate = _BookingDbContext.AppCategory.FirstOrDefault(i => i.IdCategory == Id);
			if (DeleteCate == null)
			{
				TempData["ToastMessageScs"] = "Lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}
			System.IO.File.Delete(Path.Combine(envi.WebRootPath, DeleteCate.Path.TrimStart('/')));  // xóa trong ổ đĩa

			_BookingDbContext.AppCategory.Remove(DeleteCate);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = $"Đã xóa [{DeleteCate.Name}] thành công";

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
				var list = _BookingDbContext.AppCategory
					.Where(s => ids.Contains(s.IdCategory))
					.ToList();
				foreach (var service in list)
				{
					_BookingDbContext.AppCategory.Remove(service);
				}

				_BookingDbContext.SaveChanges();

				TempData["ToastMessageScs"] = $"Đã xóa thành công [{list.Count()}] item";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
			}
		}


		private string UploadFile(IFormFile file, string dir)// kieu dữ liệu của file ảnh đc gửi lên server là IFormFile
		{
			var fName = file.FileName;
			fName = Path.GetFileNameWithoutExtension(fName)
				+ DateTime.Now.Ticks
				+ Path.GetExtension(fName);

			var res = "/Category/" + fName;
			fName = Path.Combine(dir, "Category", fName);    //Path.Conbine() dùng để kết hợp các đường dẫn thành 1 
			var stream = System.IO.File.Create(fName);
			file.CopyTo(stream);
			stream.Dispose();
			return res;
		}
	}
}
