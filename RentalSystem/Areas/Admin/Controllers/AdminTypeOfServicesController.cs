using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
    public class AdminTypeOfServicesController : AdminControllerBase
    {
        public AdminTypeOfServicesController(RentalSystemDBConText DbContext) : base(DbContext)
        {
        }

		[Route("quan-ly-danh-muc-cho-thue")]
		public IActionResult Index(string? keyword, int? page)
        {
            var services = _BookingDbContext.AppTypeOfService
                .Include(i => i.appPosts).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                var keywordUpper = keyword.ToUpper();
                services = services.Where(i => i.Name.ToUpper().Contains(keywordUpper));
                TempData["searched"] = "searched";
            }

            ViewBag.ListTypeOfService = _BookingDbContext.AppTypeOfService.ToList();

            //===================PHÂN QUYỀN=====================
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
            return View(services.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));

        }

		public IActionResult AddTypeOfServices(string services)
        {
            if (string.IsNullOrEmpty(services))
            {
                TempData["ToastMessageWrg"] = "Tên dịch vụ không được để trống";
                return RedirectToAction("Index");
            }
            var exit = _BookingDbContext.AppTypeOfService.Any(i => i.Name == services);
            if (exit)
            {
                TempData["ToastMessageScs"] = "Tên dịch vụ đã tồn tại";
                return RedirectToAction("Index");
            }
            AppTypeOfService NewSvs = new AppTypeOfService();
            NewSvs.Name = services;

            _BookingDbContext.AppTypeOfService.Add(NewSvs);
            _BookingDbContext.SaveChanges();

            TempData["ToastMessageScs"] = "Thêm dịch vụ thành công";
            return RedirectToAction("Index");
        }

		public IActionResult EditTypeOfServices(string services, string id)
        {
            int Id = int.Parse(id);
            if (services == null)
            {
                TempData["ToastMessageScs"] = "Tên dịch vụ không được để trống";
                return RedirectToAction("Index");
            }
            var exit = _BookingDbContext.AppTypeOfService.Any(i => i.Name == services);
            if (exit)
            {
                TempData["ToastMessageScs"] = "Tên dịch vụ đã tồn tại";
                return RedirectToAction("Index");
            }
            var UpdateSvc = _BookingDbContext.AppTypeOfService.FirstOrDefault(i => i.IdTypeOfService == Id);
            if (UpdateSvc == null)
            {
                TempData["ToastMessageScs"] = "Lỗi trong quá trình xử lí";
                return RedirectToAction("Index");
            }
            UpdateSvc.Name = services;
            _BookingDbContext.AppTypeOfService.Update(UpdateSvc);
            _BookingDbContext.SaveChanges();
            TempData["ToastMessageScs"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }

		public IActionResult DeleteTypeOfServices(string id)
        {
            int Id = int.Parse(id);

            var exit = _BookingDbContext.AppTypeOfService.Any(i => i.IdTypeOfService == Id);
            if (!exit)
            {
                TempData["ToastMessageScs"] = "Lỗi trong quá trình xử lí";
                return RedirectToAction("Index");
            }
            var DeleteSvc = _BookingDbContext.AppTypeOfService.FirstOrDefault(i => i.IdTypeOfService == Id);
            if (DeleteSvc == null)
            {
                TempData["ToastMessageScs"] = "Lỗi trong quá trình xử lí";
                return RedirectToAction("Index");
            }

            _BookingDbContext.AppTypeOfService.Remove(DeleteSvc);
            _BookingDbContext.SaveChanges();
            TempData["ToastMessageScs"] = $"Đã xóa [{DeleteSvc.Name}] thành công";

            return RedirectToAction("Index");
        }

        [HttpPost]
		public IActionResult DeleteList(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Không có mục nào được chọn để xóa.");
            }
            try
            {
                var servicesToDelete = _BookingDbContext.AppTypeOfService
                    .Where(s => ids.Contains(s.IdTypeOfService))
                    .ToList();
                foreach (var service in servicesToDelete)
                {
                    _BookingDbContext.AppTypeOfService.Remove(service);
                }

                _BookingDbContext.SaveChanges();

                TempData["ToastMessageScs"] = $"Đã xóa thành công [{servicesToDelete.Count()}] item";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
