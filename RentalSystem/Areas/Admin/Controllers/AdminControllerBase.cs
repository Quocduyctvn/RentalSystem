using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;
using RentalSystem.WebConfig.Constant;

namespace RentalSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "2")]

    [Area("Admin")]
    public class AdminControllerBase : Controller
    {
        protected readonly RentalSystemDBConText _BookingDbContext;  // khai báo protected quy định không cho phép truy cập ngoài lớp - trừ kế thừa 
		protected const int DEFAULT_PAGE_SIZE = 10; // Số phần tử trên 1 trang 
		protected const int DEFAULT_PAGE_NUMBER = 1;
		public AdminControllerBase(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }
    }
}
