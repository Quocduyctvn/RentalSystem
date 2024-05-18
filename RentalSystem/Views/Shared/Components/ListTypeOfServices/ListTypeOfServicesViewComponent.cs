using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.ListTypeOfServices
{
	public class ListTypeOfServicesViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public ListTypeOfServicesViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var typeOfServices = _BookingDbContext.AppTypeOfService.ToList();

			return View(typeOfServices);
		}
	}
}
