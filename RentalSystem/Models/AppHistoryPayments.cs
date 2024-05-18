using RentalSystem.WebConfig.Constant;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
	public class AppHistoryPayments
	{
		[Key]
		public int IdHistoryPayment { get; set; }
        [StringLength(20)]
        public string MaPost { get; set; }
        [Required(ErrorMessage = "Thuộc tính là bắt buột")]
        [StringLength(50)]
        public string Category { get; set; }
		public double AccountBalance { get; set; }   // số dư
		public double Fee { get; set; }					// phí ( số tiền trả đăng bài)
		public AppTransactionStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }


		public int IdPost {  get; set; }
		public AppPosts appPosts { get; set; }
	}
}
