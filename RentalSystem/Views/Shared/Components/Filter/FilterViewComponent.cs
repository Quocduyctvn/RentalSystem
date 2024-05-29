using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.Filter
{
	public class FilterViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public FilterViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
