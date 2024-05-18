using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.NavPrice
{
	public class NavPriceViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public NavPriceViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
