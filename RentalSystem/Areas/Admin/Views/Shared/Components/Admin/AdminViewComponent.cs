using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Areas.Admin.Views.Shared.Components.Admin
{
    public class AdminViewComponent : ViewComponent
    {
		protected readonly RentalSystemDBConText _BookingDbContext;

		public AdminViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			AppUsers _admin = null;

			if (User.Identity.IsAuthenticated)
			{
				ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
				Claim userIdClaim = identity.FindFirst("UserId");
				if (userIdClaim != null)
				{
					int userId = int.Parse(userIdClaim.Value);
					_admin = _BookingDbContext.AppUser.Find(userId);
				}
			}

			return View(_admin);
		}
	}
}
