using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Areas.User.Views.Shared.Components.AccountUser
{
    public class AccountUserViewComponent   : ViewComponent
    {
        protected readonly RentalSystemDBConText _BookingDbContext;

        public AccountUserViewComponent(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUsers _user = null;

            if (User.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Claim userIdClaim = identity.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim.Value);
                    _user = _BookingDbContext.AppUser.Find(userId);
                }
            }
            TempData["User"] = _user;
            return View();
        }
    }
}
