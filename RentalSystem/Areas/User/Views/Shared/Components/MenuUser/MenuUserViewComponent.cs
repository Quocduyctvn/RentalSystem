using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Areas.User.Views.Shared.Components.MenuUser
{
    public class MenuUserViewComponent : ViewComponent
    {
        protected readonly RentalSystemDBConText _BookingDbContext;

        public MenuUserViewComponent(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var typeOfServices = _BookingDbContext.AppTypeOfService
                        .Take(5)
                        .ToList();
            return View(typeOfServices);
        }


    }
}
