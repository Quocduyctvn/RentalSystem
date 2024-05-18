using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentalSystem.Views.Shared.Components.Account
{
    public class AccountViewComponent : ViewComponent
    {
        protected readonly RentalSystemDBConText _BookingDbContext;

        public AccountViewComponent(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUsers _kh = null;
            var logged = ""; // Đảm bảo rằng logged được gán giá trị phù hợp

            if (User.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Claim userIdClaim = identity.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim.Value);
                    _kh = _BookingDbContext.AppUser.Find(userId);
                    if(_kh.IdRole == 2)
                    {
						await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
						_kh = null;
					}
                }
            }

            if (_kh != null)
            {
                logged = "logged"; // Đảm bảo rằng logged được gán giá trị thích hợp
            }
            ViewBag.logged = logged;

            return View();
        }
    }
}
