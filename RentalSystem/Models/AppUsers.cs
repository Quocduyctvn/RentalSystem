using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppUsers
	{
		[Key]
		public int IdUser { get; set; }
		[StringLength(20)]
		public string? Code { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		[MinLength(3, ErrorMessage = "Họ tên phải lớn 3 ký tự")]
		public string Name { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string? Email { get; set; }
        [StringLength(15)]
        [MinLength(10, ErrorMessage = "Số điện thoại phải tối thiểu 10 ký tự ")]

        public string PhoneNumberZL { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(200)]
        [MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
        public string Password { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(200)]
		public string? Avatar {  get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(200)]
		public string? LinkFB { get; set; }

		public bool? IsBlock { get; set; }
		public double AccountBalance { get; set; }   // số dư 
		public DateTime CreatedDate { get; set; }


		public ICollection<AppPosts> appPosts { get; set;}
		public int IdRole { get; set; }
		public AppRoles appRole { get; set; }
		public ICollection<AppContactFeedback> appContactFeedback { get; set; }  // 1 admin có thể phản hồi nhiều yêu cầu mail
        public ICollection<AppPolicy> appPolicy { get; set; }  // 1 admin có thể tạo nhiều chính sách 
		public ICollection<AppRequest> appRequest { get; set; }
		public ICollection<AppNews> appNews { get; set; }

	}
}
