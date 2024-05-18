using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppImgPosts
	{
		[Key]
		public int IdImgPost { get; set; }
		[Required]
		[StringLength(500)]
		public string Path { get; set; }
		public DateTime? CreatedDate { get; set; }

		public int IdPost { get; set; }
		public AppPosts appPost { get; set; }

	}
}
