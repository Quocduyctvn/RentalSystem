using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppCategory					// vip - vip 1 ....
	{
		[Key]
		public int IdCategory {  get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string Name { get; set; }
		public double Price { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(5000)]
		public string Advantage { get; set; } // lợi thế
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(5000)]
		public string Object { get; set; }   // phù hợp đối tượng nào
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(20)]
		public string TitleColor { get; set; }   // màu tiêu đề
		public bool AutomaticBrowsing { get; set; }   // Tự đọng duyệt
		public bool DisplayNumberPhone { get; set; }
		public DateTime? CreatedDate { get; set; }
		[StringLength(200)]
		public string Path { get; set; }



		public ICollection<AppPosts> appPosts { get; set; }
	}
}
