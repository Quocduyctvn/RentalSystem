using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Areas.Admin.Views.Shared.Components.Permission
{
	public class PermissionViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public PermissionViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var role = _BookingDbContext.AppPermission
				.AsEnumerable()
				.GroupBy(x => x.GroupName)
				.ToList();

			return View(role);
		}
	}
}
