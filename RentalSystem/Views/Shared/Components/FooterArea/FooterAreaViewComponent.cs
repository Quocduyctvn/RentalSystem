using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.FooterArea
{
	public class FooterAreaViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public FooterAreaViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var ToSer = _BookingDbContext.AppTypeOfService.Take(4).ToList();
			return View(ToSer);
		}
	}
}
