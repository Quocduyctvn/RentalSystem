using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppRequest
	{
		[Key]
		public int IdRequest { get; set; }
		public DateTime CreateDate { get; set; }
		public AppPosts appPosts { get; set; }
		public int IdPost { get; set; }
		public AppUsers appUsers { get; set; }
		public int IdUser { get; set; }
        public AppContactFeedback appContactFeedback { get; set; }
    }
}
