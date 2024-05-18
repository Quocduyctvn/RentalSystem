using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.User.Controllers
{
    public class UserHistoryPaymentController : UserControllerBase
    {
        public UserHistoryPaymentController(RentalSystemDBConText DbContext) : base(DbContext)
        {
        }


		[Route("quan-ly-lich-su-thanh-toan")]
		public IActionResult Index(int? page)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            Claim userIdClaim = identity.FindFirst("UserId");
            int userId = 0;
            if (userIdClaim != null)
            {
                userId = int.Parse(userIdClaim.Value);
            }
            ViewBag.TotalHistory = _BookingDbContext.AppHistoryPayment
                                .Include(i => i.appPosts)
                                .Where(i => i.appPosts.IdUser == userId)
                                .ToList();
            var history = _BookingDbContext.AppHistoryPayment
                                .Include(i=>i.appPosts)
                                .ThenInclude(i=> i.appCategory)
                                .Where(i=>i.appPosts.IdUser == userId)
                                .OrderByDescending(i=> i.CreatedDate)
                                .AsQueryable();
            return View(history.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
        }
    }
}
