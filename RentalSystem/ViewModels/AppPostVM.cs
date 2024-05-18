using RentalSystem.Models;
using RentalSystem.WebConfig.Constant;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.ViewModels
{
	public class AppPostVM
	{
		[Key]
		public int IdPost { get; set; }
		[StringLength(20)]
		public string? MaPost { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(200)]
		[MinLength(30, ErrorMessage = "Tên bài blog quá ngắn - tối thiểu 30 ký tự")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(300)]
		[MinLength(30, ErrorMessage = "Nội dung mô tả quá ngắn - tối thiểu 30 ký tự")]
		public string Summary { get; set; }

		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[StringLength(5000)]
		[MinLength(50, ErrorMessage = "Nội dung mô tả quá ngắn - tối thiểu 50 ký tự")]
		public string Desc { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public double Area { get; set; }  // diện tích
		[StringLength(200)]
		public string Path { get; set; }
		public bool Haslable { get; set; }  // có nhãn tin thuê nhanh


		[Required(ErrorMessage = "Thuộc tính là bắt buột")]             // thuê ở ngoài 
		public double RtalPrice { get; set; }                          // 250.000/tháng
		[StringLength(20)]
		public string ToTforRtalPrice { get; set; }

		[Required(ErrorMessage = "Thuộc tính là bắt buột")]         // thuê trên web
		public double TotalPostTime { get; set; }                       // 2 tháng
		[StringLength(20)]
		public string ToTforTotalPostTime { get; set; }

		public double TotalMoney { get; set; }
		public DateTime StartDay { get; set; }                          // ngày đăng 
		public DateTime ExpirationDate { get; set; }                        // ngày hết hạn

		public AppPostStatus PostStatus { get; set; }

		public int IdRentalObject { get; set; }
		public AppRentalObjects appRentalObject { get; set; }    // lưu trữ (nam, nữ. hộ gia đình)

		//public int? IdPaymentMethod { get; set; }
		//public AppPaymentMethods appPaymentMethod { get; set; }


		//=================================================
		public ICollection<AppImgPosts> appImgPosts { get; set; }   // 1 - n

		public int? IdAddress { get; set; }
		public AppAddress appAddress { get; set; }  // 1 - 1

		public int IdTypeOfService { get; set; }
		public AppTypeOfService appTypeOfService { get; set; }

		public AppHistoryPayments appHistoryPayments { get; set; }  // 1 - 1
		public DateTime CreatedDate { get; set; }



		public int IdUser { get; set; }
		public AppUsers appUsers { get; set; }

		public int IdCategory { get; set; }
		public AppCategory appCategory { get; set; }
	}
}
