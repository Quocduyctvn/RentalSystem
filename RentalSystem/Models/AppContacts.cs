using RentalSystem.WebConfig.Constant;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppContacts
	{
		[Key]
		public int IdContact { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(200)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(500)]
		[MinLength(15, ErrorMessage = "Nội dung tối thiểu 15 từ ")]
        public string? Content { get; set; }

        public AppFeedBackStatus status { get; set; }
		public DateTime CreatedDate { get; set; }

        public AppContactFeedback? appContactFeedback { get; set; }
	}
}
