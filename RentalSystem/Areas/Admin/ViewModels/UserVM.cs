using RentalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Areas.Admin.ViewModels
{
	public class UserVM
	{
		public string Name { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }

		public string PhoneNumberZL { get; set; }
		public string Password { get; set; }
		[MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
		[Compare("Password", ErrorMessage = "Mật khẩu không khớp")]

		public string Cfm_Password { get; set; }
		public string? LinkFB { get; set; }

		public int IdRole { get; set; }	
	}
}
