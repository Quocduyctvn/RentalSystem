using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using System.Security.Claims;

namespace RentalSystem.Controllers
{
	public class PostController : ControllerBase
	{
		public PostController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("chi-tiet-bai-dang")]
		public IActionResult Detail(int? id)
		{
			var post = _BookingDbContext.AppPost
							.Include(i => i.appAddress)
							.Include(i => i.appCategory)
							.Include(i => i.appImgPosts)
							.Include(i => i.appUsers)
							.Include(i => i.appTypeOfService)
							.Include(i=> i.appRequest)
							.FirstOrDefault(i=> i.IdPost == id);
			if(post.appRequest.Count() > 0)  // lấy tất cả các Request của bài đăng này <> kiễm tra so với người dùng hiện tại 
			{
				ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
				Claim userIdClaim = identity.FindFirst("UserId");
				int userId = 0;
				if (userIdClaim != null)
				{
					userId = int.Parse(userIdClaim.Value);
				}
				if(userId > 0)
				{
					foreach (var item in post.appRequest)
					{
						if(item.IdUser == userId && item.IdPost == post.IdPost)
						{
							ViewBag.Requested = "true";
						}
					}
				}
			}
			return View(post);
		}

	}
}
