using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppRoles
	{
		[Key]
		public int IdRole { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(20)]
		public string Name {  get; set; }
		[StringLength(50)]
		public string Desc { get; set; }
		public DateTime CreatedDate { get; set; }


		public ICollection<AppUsers> appUsers { get; set; }

		public ICollection<AppRolePermission> appRolePermissions { get; set; }
	}
}
