using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.NavPost
{
	public class NavPostViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public NavPostViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var post = _BookingDbContext.AppTypeOfService.ToList();
			return View(post);
		}
	}
}