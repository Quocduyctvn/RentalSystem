using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.NewPost
{
	public class NewPostViewComponent : ViewComponent
	{
		protected readonly RentalSystemDBConText _BookingDbContext;

		public NewPostViewComponent(RentalSystemDBConText DbContext)
		{
			_BookingDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var post = _BookingDbContext.AppPost
									.Include(i => i.appAddress)
									.Include(i => i.appCategory)
									.Include(i => i.appImgPosts)
									.Include(i => i.appUsers)
									.Include(i => i.appTypeOfService)
									.Take(7)
									.OrderByDescending(i=>i.IdPost)
									.ToList();
			return View(post);
		}
	
	}
}
