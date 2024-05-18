using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppRentalObjects
	{
		[Key]
		public int IdRentalObject { get; set; }
		public string Name { get; set; }
		public DateTime? CreatedDate { get; set; }

		public ICollection<AppPosts> appPosts { get; set; }
	}
}
