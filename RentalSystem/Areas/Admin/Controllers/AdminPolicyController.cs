using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminPolicyController : AdminControllerBase
	{
		public AdminPolicyController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		[Route("quan-ly-chinh-sach")]
		public IActionResult Index(string keyword, int? page)
		{
			var policy = _BookingDbContext.AppPolicy
								.Include(i => i.appUsers)
								.AsQueryable();

			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				policy = policy.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.appUsers.Name.ToUpper().Contains(keywordUpper));
				TempData["searched"] = "searched";
			}
			ViewBag.ListPolicy = _BookingDbContext.AppPolicy.ToList();


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
			return View(policy.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult AddPolicy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddPolicy(AdminPolicyVM model)
		{
			if (!ModelState.IsValid)
			{
				TempData["ToastMessageWrg"] = "Vui lòng nhập đúng định dạng";
				return View(model);

			}
			AppUsers _user = null;
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				_user = _BookingDbContext.AppUser.Find(userId);
			}
			var policy = new AppPolicy();
			policy.Name = model.Name;
			policy.Description = model.Description;
			policy.CreatedDate = DateTime.Now;
			policy.IdUser = _user.IdUser;

			_BookingDbContext.AppPolicy.Add(policy);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Thêm chính sách thành công";
			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			var policy = _BookingDbContext.AppPolicy.Find(id);
			AdminPolicyVM policyVM = new AdminPolicyVM();
			policyVM.Name = policy.Name;
			policyVM.Description = policy.Description;
			return View(policyVM);
		}


		[HttpPost]
		public IActionResult Update(AdminPolicyVM model, int id)
		{
			var listPolicy = _BookingDbContext.AppPolicy.ToList();
			foreach (var item in listPolicy)
			{
				if (item.Name == model.Name)
				{
					TempData["ToastMessageWrg"] = "Tên chính sách đã tồn tại";
					return View(model);
				}
			}

			var policy = _BookingDbContext.AppPolicy.Find(id);
			policy.Name = model.Name;
			policy.Description = model.Description;
			_BookingDbContext.AppPolicy.Update(policy);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật chính sách thành công";
			return RedirectToAction("Index");

		}

		public IActionResult Delete(string id)
		{
			int Id = int.Parse(id);
			var policy = _BookingDbContext.AppPolicy.Find(Id);
			_BookingDbContext.AppPolicy.Remove(policy);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Xóa chính sách thành công";
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
				var list = _BookingDbContext.AppPolicy
					.Where(s => ids.Contains(s.IdPolicy))
					.ToList();
				foreach (var item in list)
				{
					_BookingDbContext.AppPolicy.Remove(item);
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

		public IActionResult Detail(int id)
		{
			var policy = _BookingDbContext.AppPolicy.Find(id);
			AdminPolicyVM policyVM = new AdminPolicyVM();
			policyVM.Name = policy.Name;
			policyVM.Description = policy.Description;
			return View(policyVM);
		}
	}
}
