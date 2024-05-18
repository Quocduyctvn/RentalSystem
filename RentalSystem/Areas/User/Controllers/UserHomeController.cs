using Microsoft.AspNetCore.Mvc;
using RentalSystem.Areas.User.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Areas.User.Controllers
{
    public class UserHomeController : UserControllerBase
    {
		public UserHomeController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		public IActionResult Index()
        {
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim.Value);
			var _user = _BookingDbContext.AppUser.Find(userId);

			var userVM = new UserVM();
			userVM.IdUser = _user.IdUser;
			userVM.Address = _user.Address;
			userVM.Email = _user.Email;
			userVM.Code = _user.Code;
			userVM.Name = _user.Name;
			userVM.Avatar = _user.Avatar;
			userVM.PhoneNumberZL = _user.PhoneNumberZL;
			userVM.LinkFB = _user.LinkFB;
			userVM.AccountBalance = _user.AccountBalance;

			ViewBag.Post = _BookingDbContext.AppPost
									.Where(i=>i.IdUser == _user.IdUser)
									.ToList();
			return View(userVM);
        }

		public IActionResult Update(UserVM userVM, [FromServices] IWebHostEnvironment envi)
		{


			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim.Value);
			var _user = _BookingDbContext.AppUser.Find(userId);

			_user.LinkFB = userVM.LinkFB == null ? "" : userVM.LinkFB;
			_user.Address = userVM.Address;
			_user.Email = userVM.Email == null ? "" : userVM.Email ;
			_user.PhoneNumberZL = userVM.PhoneNumberZL;
			if (userVM.FfAvatar != null)
			{
				_user.Avatar = UploadFile(userVM.FfAvatar, envi.WebRootPath);
			}

			_BookingDbContext.Update(_user);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật tài khoản thành công";
			return RedirectToAction("Index");
		}

		private string UploadFile(IFormFile file, string webRootPath)
		{
			var fName = file.FileName;
			fName = Path.GetFileNameWithoutExtension(fName)
				+ DateTime.Now.Ticks
				+ Path.GetExtension(fName);

			var directoryPath = Path.Combine(webRootPath, "Image", "User");
			Directory.CreateDirectory(directoryPath); // Đảm bảo thư mục tồn tại

			var filePath = Path.Combine(directoryPath, fName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			var relativePath = "/Image/User/" + fName;
			return relativePath;
		}
	}
}
