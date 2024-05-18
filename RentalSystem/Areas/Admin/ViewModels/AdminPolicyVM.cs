using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Areas.Admin.ViewModels
{
	public class AdminPolicyVM
	{
		public string Name { get; set; }
		[MinLength(100, ErrorMessage = "Nội dung chính sách tối thiểu 100 từ")]
		public string Description { get; set; }
	}
}
