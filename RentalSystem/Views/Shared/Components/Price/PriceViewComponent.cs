using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.Price
{
	public class PriceViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public PriceViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
