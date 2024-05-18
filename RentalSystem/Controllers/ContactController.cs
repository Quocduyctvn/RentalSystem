using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
	public class ContactController : ControllerBase
	{
		public ContactController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("lien-he-voi-Booking.com")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult contact(AppContacts model)
		{
			ModelState.Remove("PhoneNumber");

			if (!ModelState.IsValid)
			{
				TempData["ToastMessageWrg"] = "Vui lòng kiểm tra lại dữ liệu";
				return View(nameof(Index), model);
			}
			var infoContact = new AppContacts();
			infoContact.FullName = model.FullName;
			infoContact.Email = model.Email;
			infoContact.Content = model.Content;
			infoContact.status = WebConfig.Constant.AppFeedBackStatus.ERROR;
			infoContact.PhoneNumber = "";
			infoContact.CreatedDate = DateTime.Now;


			_BookingDbContext.Add(infoContact);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cảm ơn bạn đã gửi liên hệ cho Booking.com";
			return RedirectToAction("Index", "Home");
		}
	}
}
