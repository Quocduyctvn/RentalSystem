using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalSystem.Models;

namespace RentalSystem.DataSeeders
{
    public static class AppRolesSeeder
    {

        public static void SeedData(this EntityTypeBuilder<AppRoles> builder)
        {
            var now = new DateTime(year: 2024, month: 3, day: 10);

            // Tạo vai trò
            var roleCustomer = new AppRoles
            {
                IdRole = 1,
                Name = "Khách hàng",
                Desc = "Khách hàng",
                CreatedDate = now,
            };
            var roleAdmin = new AppRoles
            {
                IdRole = 2,
                Name = "Quản trị hệ thống",
                Desc = "Quản trị toàn bộ hệ thống",
                CreatedDate = now,
            };

            builder.HasData(roleCustomer, roleAdmin);
        }
    }
}
