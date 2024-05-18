using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;
using RentalSystem.WebConfig.Constant; 

namespace RentalSystem.DataSeeders
{
    public static class AppUsersSeeder
    {

        public static void SeedData(this EntityTypeBuilder<Models.AppUsers> builder) 
        {
            var now = new DateTime(year: 2024, month: 3, day: 10);

            // Tạo mật khẩu
            var defaultPassword = "1111";
            var pwdHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            // Tạo thông tin tài khoản admin
            builder.HasData(
                new Models.AppUsers
                {
                    IdUser = 1,
                    Name = "admin",
                    Password = pwdHash,
                    Address = "Thành phố Cần thơ",
                    Email = "quocduyctvn@gmail.com",
                    PhoneNumberZL = "0901007221",
                    Avatar = "/Image/AvatarDefault.png",
                    IsBlock = false,
                    LinkFB = "https://www.facebook.com/profile.php?id=100066548050837&mibextid=ZbWKwL",
                    AccountBalance = 0,
					Code = "US" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "1",
					CreatedDate = now,
                    IdRole = 2,              // Vai trò được tạo ở AppRoleSeeder
                },
                new Models.AppUsers
                {
                    IdUser = 2,
                    Name = "admin02",
                    Password = pwdHash,
                    Address = "Thành phố Cần thơ",
                    Email = "admin_test@gmail.com",
                    PhoneNumberZL = "0945255664",
                    Avatar = "/Image/AvatarDefault.png",
					Code = "US" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "2",
                    IsBlock = false,
                    LinkFB = "",
                    AccountBalance = 0,
                    CreatedDate = now,
                    IdRole = 2,              // Vai trò được tạo ở AppRoleSeeder
                }
            );
        }
    }
}
