using Microsoft.AspNetCore.Mvc;
using RentalSystem.Models;

namespace RentalSystem.Views.Shared.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        protected readonly RentalSystemDBConText _BookingDbContext;

        public MenuViewComponent(RentalSystemDBConText DbContext)
        {
            _BookingDbContext = DbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var typeOfServices = _BookingDbContext.AppTypeOfService
                                    .Take(7)
                                    .ToList();
            return View(typeOfServices);
        }


    }
}
