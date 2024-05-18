using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RentalSystem.Models;
using System.Net.Mail;
using System.Security.Claims;

namespace RentalSystem.Areas.User.Controllers
{
	public class UserRequestController : UserControllerBase
	{
		public UserRequestController(RentalSystemDBConText DbContext) : base(DbContext)
		{

		}
		public IActionResult Index(int id)
		{
			var request = _BookingDbContext.AppRequest
											.Include(i=> i.appPosts)
												.ThenInclude(i=> i.appCategory)
											.Include(i => i.appUsers)
                                                .ThenInclude(i => i.appContactFeedback)
											.Include(i=> i.appContactFeedback)
                                            .Where(i=> i.IdPost == id)
											.ToList();
			

			return View(request);
		}
		public IActionResult Request(int id)
		{
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim.Value);
			if (userId == null || userId <= 0)
			{
				TempData["ToastMessageErr"] = "Cảnh báo truy cập không hợp lệ";
				return RedirectToAction("Login", "Account", "");
			}
			var _user = _BookingDbContext.AppUser.Find(userId);

			var check = _BookingDbContext.AppRequest.Any(i=> i.IdUser == userId && i.IdPost == id); // kiểm tra đã gửi yêu cầy chưa
            if (check)
            {
                TempData["ToastMessageErr"] = "Yêu cầu của bạn đang trong quá trình xử lí";
                string previousurl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(previousurl);
            }

			// lấy thông tin người cho thuê 
			var UserPost = _BookingDbContext.AppPost
												.Include(i => i.appUsers)
												.FirstOrDefault(i=> i.IdPost == id);
            var request = new AppRequest();
			request.IdPost = id;
			request.IdUser = _user.IdUser;
			request.CreateDate = DateTime.UtcNow;

			_BookingDbContext.Add(request);
			_BookingDbContext.SaveChanges();

			string content = $@"
							<div class=""container bg-secondary d-flex justify-content-center"">
								<div class=""bg-light mt-1 rounded-2 p-3"" style=""width: 1200px"">
									<div class=""row px-4"">
										<div class=""col p-0"">
											<div class=""row"">Thân gửi {UserPost.appUsers.Name},</div>
											<div class=""row"">
												<span class=""p-0 col-auto fw-bold"" style=""color:#c88321"">Booking.com</span>
												<div class=""col-auto ps-1"">vừa nhận được một yêu cầu đặt phòng từ một người quan tâm đến dịch vụ bạn đang đăng trên <span style=""color:#c88321"">Booking.com</span></div>
											</div>
											<div class=""row mt-2""><i class=""p-0"">Thông tin dịch vụ:</i></div>
											<div class=""row ms-3"">Mã bài đăng: {UserPost.MaPost}</div>
											<div class=""row ms-3"">Tên bài đăng: ""{UserPost.Title}""</div>
											<div class=""row mt-2""><i class=""p-0"">Thông tin của người gửi yêu cầu như sau:</i></div>
											<div class=""row ms-3"">Tên khách hàng: {_user.Name}</div>
											<div class=""row ms-3"">Số điện thoại: {_user.PhoneNumberZL}</div>
											<div class=""row ms-3"">Email: {_user.Email}</div>
											<div class=""row mt-2""><i class=""p-0""><span class=""text-danger"">Ghi chú:</span> Qúy khách vui lòng lên Hệ thống của Booking.com để gửi phản hồi Hoặc dùng thông tin khách hàng phía trên để trao đổi.</i></div>
											<div class=""row mt-2"">Cảm ơn bạn đã hợp tác và hy vọng nhận được phản hồi sớm từ bạn.</div>
											<div class=""row mt-2"">Trân trọng cảm ơn,</div>
										</div>
									</div>
								</div>
							</div>
			";
			var Username = "quocduyctvn@gmail.com";                             // người gửi mail  
			var Password = "fcdl pezn rchn apcp";
			var Host = "smtp.gmail.com";
			var Port = 587;
			var FromEmail = "quocduyctvn@gmail.com";

			MailMessage message = new MailMessage();
			message.From = new MailAddress(FromEmail);                       // nguoi gui mail
			message.To.Add(_user.Email);                                   // nguoi nhan mail
			message.Subject = "Bạn có 1 yêu cầu nhận dịch vụ từ Khách hàng trên Booking.com";
			message.IsBodyHtml = true;
			message.Body = content;

			SmtpClient mailClient = new SmtpClient();
			mailClient.EnableSsl = true;

			mailClient.UseDefaultCredentials = false;
			mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
			mailClient.Host = Host;
			mailClient.Port = Port;

			mailClient.Send(message);

			TempData["ToastMessageScs"] = "Yêu cầu đã được gửi đi";
            string previousUrl = HttpContext.Request.Headers["Referer"].ToString();
			return Redirect(previousUrl);
		}

		public IActionResult SendMail(int idRequest, int idPost, int idUser, string content, string subject)
		{
			//var contact = _BookingDbContext.AppContact.Find(id);

			var post = _BookingDbContext.AppPost
										.Include(i=> i.appUsers)
										.FirstOrDefault(i => i.IdPost == idPost);
			var user = _BookingDbContext.AppUser.Find(idUser);				// người gửi yêu cầu

			//KIEEMR TRA MAIL 
			if(user.Email == null)
			{
                TempData["ToastMessageWrg"] = "Khách hàng chưa có mail";
                string previousUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }
            if (post.appUsers.Email == null)
            {
                TempData["ToastMessageWrg"] = "Vui lòng cpaaj nhật email của bạn";
                string previousUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(previousUrl);
            }




            var Username = post.appUsers.Email;								// người gửi mail  
			var Password = "fcdl pezn rchn apcp";
			var Host = "smtp.gmail.com";
			var Port = 587;
			var FromEmail = post.appUsers.Email;

			MailMessage message = new MailMessage();

			message.From = new MailAddress(FromEmail);						 // nguoi gui mail
			message.To.Add(user.Email);                                   // nguoi nhan mail
			message.Subject = subject;
			message.IsBodyHtml = true;
			message.Body = content;

			SmtpClient mailClient = new SmtpClient();
				mailClient.EnableSsl = true;

				mailClient.UseDefaultCredentials = false;
				mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
				mailClient.Host = Host;
				mailClient.Port = Port;

				mailClient.Send(message);

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
			feedback.IdContact = null;
			feedback.CreatedDate = DateTime.Now;
			feedback.IdRequest = idRequest;
			feedback.IdUser = _user.IdUser;
			_BookingDbContext.AppContactFeedback.Add(feedback);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Đã gửi phản hồi thành công";

			return RedirectToAction("Index", "UserPost");
		}

	}
}
