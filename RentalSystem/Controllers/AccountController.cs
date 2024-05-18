using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using RentalSystem.ViewModels;
using System.Security.Claims;
using static RentalSystem.WebConfig.Constant.AuthConst;
using Microsoft.EntityFrameworkCore;
using RentalSystem.WebConfig.Constant;
using System.Text.RegularExpressions;

namespace RentalSystem.Controllers
{
    public class AccountController : ControllerBase
    {
        public AccountController(RentalSystemDBConText DbContext) : base(DbContext)
        {
        }


		[Route("dang-ky-tai-khoan")]
		public IActionResult Register() => View();
        [HttpPost]
		[Route("dang-ky-tai-khoan")]
		public IActionResult Register(AppRegisterVM AppRegisterVM)
        {
            if(!ModelState.IsValid)
            {
                TempData["ToastMessageScs"] = "Dữ liệu không hợp lệ vui lòng kiểm tra lại!";
                return View(AppRegisterVM);
            }
            var exit = _BookingDbContext.AppUser.Any(i => i.Name == AppRegisterVM.Name || i.PhoneNumberZL == AppRegisterVM.PhoneNumberZL);
            if(exit)
            {
                TempData["ToastMessageScs"] = "Tên đăng nhập || Số điện thoại đã tồn tại";
                return View(AppRegisterVM);
            }
            var user = new Models.AppUsers();
            user.Name = AppRegisterVM.Name;
            user.PhoneNumberZL = AppRegisterVM.PhoneNumberZL;
            user.AccountBalance = 10000;
            user.Email = "";
            user.LinkFB = "";
            user.Avatar = "/Image/Manager/AvatarDefault.png";
            user.Password = BCrypt.Net.BCrypt.HashPassword(AppRegisterVM.Password);
            user.IdRole = (int)AppEnumRole.KHACHHANG;
            user.CreatedDate = DateTime.Now;
            user.IsBlock = false;

            _BookingDbContext.AppUser.Add(user);
            _BookingDbContext.SaveChanges();

            user.Code = "US" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + user.IdUser.ToString();
			_BookingDbContext.AppUser.Update(user);
			_BookingDbContext.SaveChanges();

			TempData["ToastMessageScs"] = "Tạo tài khoản thành công";
            return RedirectToAction("Index", "Home");
		}


		///User/UserRequest/Request/2
		[HttpGet]
		public IActionResult Login(string ReturnUrl = null)
        {
            string path = ReturnUrl;
            if (!string.IsNullOrEmpty(path))
            {
				string result = Regex.Replace(path, @"\/\d+$", "");
				if (result == "/User/UserRequest/Request")
				{
					TempData["ToastMessageErr"] = "Vui lòng đăng nhập vào Booking.com";
					return RedirectToAction("Login", "Account", ReturnUrl = "");
				}
			}

			if (!string.IsNullOrEmpty(ReturnUrl))
			{
				TempData["ToastMessageErr"] = "Cảnh báo truy cập không hợp lệ";
				return RedirectToAction("Login","Account", ReturnUrl= "");
			}
			return View();
        }
        [HttpPost]
		public IActionResult Login(AppLoginVM appLoginVM)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        TempData["ToastMessageScs"] += error.ErrorMessage + " ";
                    }
                }
                return View(appLoginVM);
            }
            var user =  _BookingDbContext.AppUser.FirstOrDefault( i=> i.Name == appLoginVM.Name);
            if (user == null)
            {
                TempData["ToastMessageScs"] = "Tên tài khoản không tồn tại";
                return RedirectToAction("Index", "Home");
            }

            // check Pass
            // Kiểm tra mật khẩu nhập vào với hash mật khẩu lưu trong cơ sở dữ liệu
            var checkPass = BCrypt.Net.BCrypt.Verify(appLoginVM?.Password, user.Password);

            // Nếu mật khẩu không đúng
            if (!checkPass)
            {
                TempData["ToastMessageScs"] = "Thông tin đăng nhập không hợp lệ";
                return View();
            }


			var claims = new List<Claim>
                            {
                                new Claim("UserId", user.IdUser.ToString()),
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.MobilePhone, user.PhoneNumberZL),
                                //claim - role động
                                new Claim(ClaimTypes.Role , user.IdRole.ToString()) 
							};
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(claimsPrincipal);


            if(user.IdRole == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.IdRole == 2)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            // nếu không là khách => sau Khi ĐNhap thì trả về trang người dùng yêu cầu
            var returnUrl = Request.Query["ReturnUrl"].ToString();   // code đang vô tri 
            return Redirect(returnUrl);
        }


		[Route("quen-tai-khoan")]
		[HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string sdt)
        {
            return RedirectToAction("Index", "Home");
        }

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
