using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppRolePermission
	{
		[Key]
		public int IdRolePermission { get; set; }
		public int IdRole { get; set; }
		public int IdPermission { get; set; }
		public DateTime? CreatedDate { get; set; }


		public AppRoles appRole { get; set; }
		public AppPermissions appPermission { get; set; }
	}
}
