using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.Area
{
	public class AreaViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public AreaViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}

	}
}
