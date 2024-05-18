using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppTypeOfService
	{
		[Key]
		public int IdTypeOfService {  get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(50)]
		public string Name { get; set; }  // phòng trọ, nhà trọ , căn hộ
		public DateTime? CreatedDate { get; set; }


		//=================================================
		public ICollection<AppPosts> appPosts { get; set; }  // 1-n 
	}
}
