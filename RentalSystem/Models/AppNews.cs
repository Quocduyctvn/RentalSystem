using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppNews
	{
		[Key]
		public int IdNews { get; set; }

		[MaxLength(200)]
		public string Title { get; set; }

		[MaxLength(300)]
		public string CoverImg { get; set; }

		[MaxLength(500)]
		public string Summary { get; set; }
		[StringLength(10000)]
		public string Content { get; set; }
		public DateTime CreateDate { get; set; }

		public int IdCateNews { get; set; }
		public AppCategoryNews appCategoryNews { get; set; }
		public int IdUser { get; set; }
		public AppUsers appUser { get; set; }

	}
}
