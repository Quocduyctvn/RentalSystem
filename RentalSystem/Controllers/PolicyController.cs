using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
	public class PolicyController : ControllerBase
	{
		public PolicyController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("chi-tiet-chinh-sach")]
		public IActionResult Index(int id)
		{
			var policy = _BookingDbContext.AppPolicy.Find(id);
			return View(policy);
		}
	}
}
