namespace RentalSystem.WebConfig.Constant
{
	public enum AppPostStatus
	{
		PENDING = 0,   // CHỜ DUYỆT 
		APPROVED = 1,  // ĐÃ DUYỆT
		EXPIRED = 2,   // HẾT HẠN 
		BOOKED = 3,   // ĐÃ ĐẶT 
		HIDDEN = 4,   // ẨN
		DISPLAY = 5,  // HIỂN THỊ
		CANCEL = 6,   // HỦY
		REPOST = 7,	  // ĐĂNG LẠI
		UNPAID = 8,   // chưa thanh toán
		TRADING = 9, // Đang giao dịch
	}
}