using RentalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Areas.Admin.ViewModels
{
	public class AddOrUpdateNews
	{
		public string Title { get; set; }
		public string CoverImg { get; set; }

		public IFormFile FormFileImg { get; set; }
		public string Summary { get; set; }
		public string Content { get; set; }
		public DateTime? CreateDate { get; set; }

		public int IdCateNews { get; set; }
		public AppCategoryNews appCategoryNews { get; set; }
		public int IdUser { get; set; }
		public AppUsers appUser { get; set; }
	}
}
