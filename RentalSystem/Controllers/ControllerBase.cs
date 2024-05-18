using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly RentalSystemDBConText _BookingDbContext;  // khai báo protected quy định không cho phép truy cập ngoài lớp - trừ kế thừa 
        protected const int DEFAULT_PAGE_SIZE = 10; // Số phần tử trên 1 trang 
        protected const int DEFAULT_PAGE_NUMBER = 1;
		protected const int DEFAULT_PAGE_NUMBER_QUERY_STRING = 1;
		public ControllerBase(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }

    }
}
