using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.NavArea
{
	public class NavAreaViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public NavAreaViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
