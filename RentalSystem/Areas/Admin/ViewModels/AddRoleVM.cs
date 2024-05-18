using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Areas.Admin.ViewModels
{
	public class AddRoleVM
	{
		[Required(ErrorMessage = "Tên Vai trò là bắt buột")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Desc { get; set; }
		public string IdPermission { get; set; }
	}
}
