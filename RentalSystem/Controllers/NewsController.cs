using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
	public class NewsController : ControllerBase
	{
		public NewsController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		public IActionResult Index()
		{
			var news = _BookingDbContext.AppNews
											.Include(i=> i.appCategoryNews)
											.ToList();
			return View(news);
		}
		public IActionResult Detail(int id)
		{
			var news = _BookingDbContext.AppNews
											.Include(i => i.appCategoryNews)
											.FirstOrDefault(i=> i.IdNews == id);
			return View(news);
		}
	}
}
