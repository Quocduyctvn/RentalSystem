using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using X.PagedList;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminAccountController : AdminControllerBase
	{
		public AdminAccountController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		[Route("quan-ly-tai-khoan")]
		public IActionResult Index(string keyword, int? page)
		{

			ViewBag.ListUser = _BookingDbContext.AppUser
										.Include(i => i.appRole)
										.ToList();
			var listUser = _BookingDbContext.AppUser
									.Include(i => i.appRole)		
									.AsQueryable();
			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				listUser = listUser.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.appRole.Name.ToUpper().Contains(keywordUpper) || i.Email.ToUpper().Contains(keywordUpper));
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
			return View(listUser.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		[Route("them-tai-khoan")]
		public IActionResult AddUser()
		{
			ViewBag.ListRole = _BookingDbContext.AppRole.ToList();
			return View();
		}
		[HttpPost]
		[Route("them-tai-khoan")]
		public IActionResult AddUser(UserVM userVM)
		{

			if (!ModelState.IsValid)
			{
				TempData["ToastMessageWrg"] = "Vui lòng kiểm tra lại dữ liệu: ";
				return RedirectToAction("Index", userVM);
			}



			var exit = _BookingDbContext.AppUser.Any(i => i.PhoneNumberZL == userVM.PhoneNumberZL || i.Name == userVM.Name);
			if (exit)
			{
				TempData["ToastMessageWrg"] = "Tài khoản đã tồn tại";
				return RedirectToAction("AddUser", userVM);
			}
			var user = new AppUsers();
			user.Name = userVM.Name;
			user.Email = userVM.Email;
			user.Address = userVM.Address;
			user.Password = BCrypt.Net.BCrypt.HashPassword(userVM.Password);
			user.PhoneNumberZL = userVM.PhoneNumberZL;
			user.Avatar = "/Image/AvatarDefault.png";
			user.AccountBalance = 0;
			user.CreatedDate = DateTime.Now;
			user.IdRole = userVM.IdRole;
			user.IsBlock = false;
			user.LinkFB = userVM.LinkFB == null ? "" : userVM.LinkFB;
			_BookingDbContext.AppUser.Add(user);
			_BookingDbContext.SaveChanges();

			user.Code = "US" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + user.IdUser.ToString();
			_BookingDbContext.AppUser.Update(user);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Thêm tài khoản thành công";
			return RedirectToAction("Index");
		}


		[Route("cap-nhat-tai-khoan")]
		public IActionResult UpdateUser(int? id)
		{
			ViewBag.ListRole = _BookingDbContext.AppRole.ToList();
			var user = _BookingDbContext.AppUser.Find(id);
			if (user == null)
			{
				TempData["ToastMessageWrg"] = "Đã xảy ra lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}
			var userVM = new UserVM();
			userVM.Address = user.Address;
			userVM.Password = user.Password;
			userVM.Cfm_Password = user.Password;
			userVM.Email = user.Email;
			userVM.LinkFB = user.LinkFB;
			userVM.PhoneNumberZL = user.PhoneNumberZL;
			userVM.Name = user.Name;
			userVM.IdRole = user.IdRole;

			return View(userVM);
		}
		[HttpPost]
		public IActionResult UpdateUser(int? id, UserVM userVM)
		{
			var user = _BookingDbContext.AppUser.Find(id);
			user.Name = userVM.Name;
			user.Email = userVM.Email;
			user.Address = userVM.Address;
			user.PhoneNumberZL = userVM.PhoneNumberZL;
			user.CreatedDate = DateTime.Now;
			user.IdRole = userVM.IdRole;
			user.LinkFB = userVM.LinkFB == null ? "" : userVM.LinkFB;
			_BookingDbContext.AppUser.Update(user);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật thành công";

			return RedirectToAction("Index");
		}


		public IActionResult DeleteUser(string id, [FromServices] IWebHostEnvironment envi)
		{
			int Id = int.Parse(id);
			var user = _BookingDbContext.AppUser.Find(Id);

			if (!string.IsNullOrEmpty(user.Avatar))
			{
				System.IO.File.Delete(Path.Combine(envi.WebRootPath, user.Avatar.TrimStart('/')));
			}
			_BookingDbContext.Remove(user);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Xóa tài khoản thành công";
			return RedirectToAction("Index");
		}

		public IActionResult UnBlock(int id) // mở khóa 
		{
			var user = _BookingDbContext.AppUser.Find(id);
			user.IsBlock = false;
			_BookingDbContext.Update(user);
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminAccount");
		}
		public IActionResult Block(int id)
		{
			var user = _BookingDbContext.AppUser.Find(id);
			user.IsBlock = true;
			_BookingDbContext.Update(user);
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminAccount");
		}
	}
}
