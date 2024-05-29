using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.NavNews
{
	public class NavNewsViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public NavNewsViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var post = _BookingDbContext.AppNews
									.Take(5)
									.OrderByDescending( i=> i.CreateDate)
									.ToList();
			return View(post);
		}
	}
}