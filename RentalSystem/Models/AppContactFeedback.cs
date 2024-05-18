using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppContactFeedback
	{
		[Key]
		public int IdContactFback { get; set;}
		[StringLength(200)]
		public string Title { get; set; }
		[StringLength(5000)]
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? IdContact { get; set; }
		public AppContacts appContacts { get; set; }
		public int IdUser { get; set; }
		public AppUsers appUsers { get; set; }
        public int? IdRequest { get; set; }
        public AppRequest  appRequest { get; set; }
    }
}
