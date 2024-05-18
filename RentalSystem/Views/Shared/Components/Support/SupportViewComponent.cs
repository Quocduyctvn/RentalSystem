using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.Support
{
	public class SupportViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public SupportViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var policy = _BookingDbContext.AppPolicy
									.Take(5)
									.ToList();
			return View(policy);
		}
	}
}
