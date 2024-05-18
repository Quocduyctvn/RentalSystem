using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppTimes
	{
		[Key]
		public int IdTime { get; set; }
		public string Name { get; set; }
		public int Time {  get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
