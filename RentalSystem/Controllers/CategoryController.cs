using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
	public class CategoryController : ControllerBase
	{
		public CategoryController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("bang-dich-vu")]
		public IActionResult Index()
		{
			var cate = _BookingDbContext.AppCategory.ToList();
			return View(cate);
		}
	}
}
