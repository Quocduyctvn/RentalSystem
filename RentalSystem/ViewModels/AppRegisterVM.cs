using System.ComponentModel.DataAnnotations;

namespace RentalSystem.ViewModels
{
    public class AppRegisterVM
    {
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "Họ tên phải lớn 3 ký tự")]
        public string Name { get; set; }

        [StringLength(15)]
        [MinLength(10, ErrorMessage = "Số điện thoại phải tối thiểu 10 ký tự ")]
        public string PhoneNumberZL { get; set; }

        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(200)]
        [MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
        public string Password { get; set; }
    }
}
