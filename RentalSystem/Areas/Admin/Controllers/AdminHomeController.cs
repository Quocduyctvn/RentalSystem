using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminControllerBase
    {
        public AdminHomeController(RentalSystemDBConText DbContext) : base(DbContext)
        {
        }

		public IActionResult Index()
        {
			ViewBag.Post = _BookingDbContext.AppPost
									.ToList();
			return View();
        }
    }
}
