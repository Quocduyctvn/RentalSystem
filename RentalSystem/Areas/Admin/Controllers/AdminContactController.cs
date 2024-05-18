using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RentalSystem.Models;
using System.Net.Mail;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminContactController : AdminControllerBase
	{
		public AdminContactController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		[Route("quan-ly-lien-he")]
		public IActionResult Index(string keyword, int? page)
		{

			var contact = _BookingDbContext.AppContact
							.Include(i=> i.appContactFeedback)
							.AsQueryable();

			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				contact = contact.Where(i => i.FullName.ToUpper().Contains(keywordUpper) || i.Content.ToUpper().Contains(keywordUpper) || i.Email.ToUpper().Contains(keywordUpper));
				TempData["searched"] = "searched";
			}
			//===================PHAN QUYEN=====================
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				var user = _BookingDbContext.AppUser
									.Include(i => i.appRole)
									.FirstOrDefault(i => i.IdUser == userId);
				var IdRole = user.IdRole;
                ViewBag.RolePer = _BookingDbContext.AppRolePermission
                                    .Where(i => i.IdRole == IdRole)
                                    .ToList();

            }
			return View(contact.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		[Route("chi-tiet-lien-he")]
		public IActionResult Detail(int id)
		{
			var contact = _BookingDbContext.AppContact.Find(id);

			var feedback = _BookingDbContext.AppContactFeedback
												.FirstOrDefault(i => i.IdContact == id);
			if (feedback != null)
			{
				ViewBag.Feedback = feedback;
			}
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				var user = _BookingDbContext.AppUser
									.Include(i => i.appRole)
									.FirstOrDefault(i => i.IdUser == userId);
				var IdRole = user.IdRole;
				ViewBag.RolePer = _BookingDbContext.AppRolePermission
									.Where(i => i.IdRole == IdRole)
									.ToList();

			}
			return View(contact);
		}

		public IActionResult SendMail(int id, string content, string subject)
		{

			var contact = _BookingDbContext.AppContact.Find(id);

			var Username = "quocduyctvn@gmail.com";
			var Password = "fcdl pezn rchn apcp";
			var Host = "smtp.gmail.com";
			var Port = 587;
			var FromEmail = "quocduyctvn@gmail.com";


			MailMessage message = new MailMessage();

			message.From = new MailAddress(FromEmail);                      // nguoi gui 
			message.To.Add(contact.Email);                 // nguoi nhan 
			message.Subject = subject;
			message.IsBodyHtml = true;
			message.Body = content;

			SmtpClient mailClient = new SmtpClient();
			try
			{
				mailClient.EnableSsl = true;

				mailClient.UseDefaultCredentials = false;
				mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
				mailClient.Host = Host;
				mailClient.Port = Port;

				mailClient.Send(message);
			}
			catch (Exception ex)
			{

			}
			AppUsers _user = null;
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				_user = _BookingDbContext.AppUser.Find(userId);
			}

			var feedback = new AppContactFeedback();
			feedback.Title = subject;
			feedback.Description = content;
			feedback.IdContact = contact.IdContact;
			feedback.CreatedDate = DateTime.Now;
			feedback.IdUser = _user.IdUser;
			_BookingDbContext.AppContactFeedback.Add(feedback);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Đã gửi liên hệ thành công";


			contact.status = WebConfig.Constant.AppFeedBackStatus.SUCCESS;
			_BookingDbContext.AppContact.Update(contact);
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult DeleteContact(string id)
		{
			int Id = int.Parse(id);
			var feedback = _BookingDbContext.AppContactFeedback
											.FirstOrDefault(i => i.IdContact == Id);
			if (feedback != null)
			{
				_BookingDbContext.AppContactFeedback.Remove(feedback);
				_BookingDbContext.SaveChanges();
			}

			var contact = _BookingDbContext.AppContact.Find(Id);
			if (contact != null)
			{
				_BookingDbContext.AppContact.Remove(contact);
				_BookingDbContext.SaveChanges();
				TempData["ToastMessageScs"] = "Xóa bài viết hỗ trợ thành công";
			}

			return RedirectToAction("Index");
		}
	}
}
