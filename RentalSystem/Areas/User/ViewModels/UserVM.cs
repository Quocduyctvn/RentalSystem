using RentalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Areas.User.ViewModels
{
	public class UserVM
	{
		public int IdUser { get; set; }
		public string? Code { get; set; }
		public string Name { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string PhoneNumberZL { get; set; }
		public string Password { get; set; }
		public string? Avatar { get; set; }
		public IFormFile? FfAvatar { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(200)]
		public string? LinkFB { get; set; }
		public DateTime? BlockedTo { get; set; }
		public int? BlockedBy { get; set; }
		public double AccountBalance { get; set; }   // số dư 
		public DateTime? CreatedDate { get; set; }


		public ICollection<AppPosts> appPosts { get; set; }
		public int IdRole { get; set; }
		public AppRoles appRole { get; set; }
	}
}
