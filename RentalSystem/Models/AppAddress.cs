using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppAddress
	{
		[Key]
		public int IdAddress { get; set; }
		[Required( ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(30)]
		public string City { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(30)]
		public string District { get; set; }  // quận
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(30)]
		public string Wards { get; set; }  // xã - phường 
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(30)]
		public string Street { get; set; }// đường
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(30)]
		public string ApartmentNumber { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(100)]
		public string FullAdress {  get; set; }
		public DateTime? CreatedDate { get; set; }

		public AppPosts appPosts { get; set; }  // 1-1
	}
}
