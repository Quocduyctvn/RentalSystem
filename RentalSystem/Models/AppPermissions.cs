using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppPermissions
	{
		[Key]
		public int IdPermission { get; set; }
		public string Code { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string Table { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string GroupName { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string Desc { get; set; }
		public DateTime? CreatedDate { get; set; }


		public ICollection<AppRolePermission> appRolePermissions { get; set; }
	}
}
