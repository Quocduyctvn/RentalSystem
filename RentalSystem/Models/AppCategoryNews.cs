using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppCategoryNews
	{
		[Key]
		public int IdCateNews { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }
		[StringLength(200)]
		public string Desc { get; set; }
		public DateTime CreatedDate { get; set; }

		public ICollection<AppNews> appNews { get; set; }
	}
}
