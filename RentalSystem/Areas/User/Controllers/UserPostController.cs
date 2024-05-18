using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Areas.User.ViewModels;
using RentalSystem.Models;
using RentalSystem.WebConfig.Constant;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using X.PagedList;

namespace RentalSystem.Areas.User.Controllers
{
	public class UserPostController : UserControllerBase
	{
		public UserPostController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}

		public IActionResult Index(int? selectedCategoryId, AppPostStatus? status, int? page)
		{
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim.Value);
			var _user = _BookingDbContext.AppUser.Find(userId);
			IQueryable<AppPosts> listPost = _BookingDbContext.AppPost
								.Include(i => i.appHistoryPayments)
								.AsQueryable(); // Thay đổi từ IEnumerable sang IQueryable


			ViewBag.TotalPost = listPost.ToList();
			var Cate = new AppCategory();
			if (selectedCategoryId != null && selectedCategoryId > 0)
			{
				listPost = listPost.Where(i => i.IdCategory == selectedCategoryId);
				Cate = _BookingDbContext.AppCategory.Find(selectedCategoryId);
				ViewBag.searchName = Cate.Name;
				ViewBag.searched = "searched";
			}
			if (status != null)
			{
				listPost = listPost.Where(i => i.PostStatus == status);

				Cate = _BookingDbContext.AppCategory.Find(selectedCategoryId);
				List<AppPostStatus> listStatus = Enum.GetValues(typeof(AppPostStatus))
										 .Cast<AppPostStatus>()
										 .ToList();

				var StatusName = "";
				foreach (var item in listStatus)
				{
					if (item == status)
					{
						if (item == AppPostStatus.APPROVED)
						{
							StatusName = "Đang hoạt động";
						}
						if (item == AppPostStatus.PENDING)
						{
							StatusName = "Đang chờ duyệt";
						}
						if (item == AppPostStatus.EXPIRED)
						{
							StatusName = "Hết hạn";
						}
						if (item == AppPostStatus.BOOKED)
						{
							StatusName = "Đã đặt";
						}
						if (item == AppPostStatus.HIDDEN)
						{
							StatusName = "Ẩn";
						}
						if (item == AppPostStatus.UNPAID)
						{
							StatusName = "Chưa thanh toán";
						}
					}
				}


				if (Cate != null)
				{
					ViewBag.searchName += StatusName;
				}
				else
				{
					ViewBag.searchName = StatusName;
				}
				ViewBag.searched += "searched";
			}

			// Thực hiện các Include và chuyển đổi IQueryable thành List
			var filteredList = listPost.Where(i => i.IdUser == _user.IdUser)
									.Include(i => i.appTypeOfService)
								   .Include(i => i.appCategory)
								   .Include(i => i.appAddress)
								   .Include(i => i.appHistoryPayments)
								   .Include(i => i.appRequest)
								   .ToList();


			ViewBag.Category = _BookingDbContext.AppCategory
								.OrderByDescending(i => i.Price)
								.ToList();

			return View(filteredList.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult AddOrUpdatePost(int? id)
		{
			var postVM = new AddOrUpdatePostVM();
			if (id.HasValue && id > 0)
			{
				ViewBag.ActionName = "Cập nhật Gói tin";
				var post = _BookingDbContext.AppPost
										.Include(i => i.appTypeOfService)
										.Include(i => i.appCategory)
										.Include(i => i.appAddress)
										.Include(i => i.appRentalObject)
										.Include(i => i.appImgPosts)
										.FirstOrDefault(i => i.IdPost == id);
				postVM.IdPost = post.IdPost;
				postVM.Title = post.Title;
				postVM.Summary = post.Summary;
				postVM.Desc = post.Desc;
				postVM.MaPost = post.MaPost;
				postVM.appAddress = new AppAddress();
				postVM.appCategory = post.appCategory;
				postVM.appHistoryPayments = post.appHistoryPayments;
				postVM.appTypeOfService = post.appTypeOfService;
				postVM.appRentalObject = post.appRentalObject;
				postVM.appImgPostsString = post.appImgPosts != null ? post.appImgPosts.Select(img => img.Path).ToList() : new List<string>();
				postVM.Status = post.PostStatus;
				//postVM.PathVideoString = post.PathVideo;
				postVM.PathString = post.Path;
				postVM.Area = post.Area;
				postVM.ExpirationDate = post.ExpirationDate;
				postVM.Haslable = post.Haslable;
				postVM.RtalPrice = post.RtalPrice;
				postVM.ToTforRtalPrice = post.ToTforRtalPrice;
				postVM.TotalPostTime = post.TotalPostTime;
				postVM.ToTforTotalPostTime = post.ToTforTotalPostTime;
				postVM.StartDay = post.StartDay;
				postVM.IdCategory = post.IdCategory;
				postVM.IdRentalObject = post.IdRentalObject;
				//postVM.IdTimeRtalPrice = post.id
			}
			else
			{
				ViewBag.ActionName = "Thêm mới gói tin";
			}
			ViewBag.AppTypeOfServices = _BookingDbContext.AppTypeOfService.ToList();
			ViewBag.RtalPriceOfCateTime = _BookingDbContext.AppTimes.ToList();   // lấy list  thời gian người chủ sở hữu định cho thuê
			ViewBag.RtalObject = _BookingDbContext.AppRentalObject.ToList();
			ViewBag.AppCate = _BookingDbContext.AppCategory.OrderByDescending(i => i.Price).ToList();
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			if (userIdClaim != null)
			{
				int userId = int.Parse(userIdClaim.Value);
				var _user = _BookingDbContext.AppUser.Find(userId);
				TempData["User"] = _user;
			}

			return View(postVM);
		}

		[HttpPost]
		public IActionResult AddOrUpdatePost(int? id, AddOrUpdatePostVM AddOrUpdatePostVM, [FromServices] IWebHostEnvironment envi)
		{
			if (AddOrUpdatePostVM == null)
			{
				TempData["ToastMessageWrg"] = "Vui lòng kiểm tra dữ liệu hợp lệ";
				return View(AddOrUpdatePostVM);
			}


			if (AddOrUpdatePostVM.TotalPostTime == null)
			{
				TempData["ToastMessageWrg"] = "Thời gian đăng bài toàn bộ đang trống hoặc là null.";
				return View(AddOrUpdatePostVM);
			}

			var totalPostTime = _BookingDbContext.AppTimes.Find(AddOrUpdatePostVM.IdTotalPostTime);
			if (AddOrUpdatePostVM.TotalPostTime < 5 && totalPostTime.Name == "Ngày")
			{
				TempData["ToastMessageWrg"] = "Bài đăng phải tối thiểu 5 ngày";
				return View(AddOrUpdatePostVM);
			}

			var post = new AppPosts();
			if (id != null)
			{
				post = _BookingDbContext.AppPost
							.Include(i => i.appImgPosts)
							.FirstOrDefault(i => i.IdPost == id);
			}
			post.Summary = AddOrUpdatePostVM.Summary;
			post.Title = AddOrUpdatePostVM.Title;
			post.Desc = AddOrUpdatePostVM.Desc;
			post.Area = AddOrUpdatePostVM.Area;  // dien tich
			post.CreatedDate = DateTime.UtcNow;
			post.IsPublic = true;

			var timeRtalPrice = _BookingDbContext.AppTimes.Find(AddOrUpdatePostVM.IdTimeRtalPrice);
			post.RtalPrice = AddOrUpdatePostVM.RtalPrice;
			post.ToTforRtalPrice = timeRtalPrice.Name;



			post.Haslable = AddOrUpdatePostVM.Haslable;
			if (AddOrUpdatePostVM.Path != null)
			{
				if (id != null)
				{
					System.IO.File.Delete(Path.Combine(envi.WebRootPath + post.Path));
				}
				post.Path = UploadFile(AddOrUpdatePostVM.Path, envi.WebRootPath);
			}
			//if (AddOrUpdatePostVM.PathVideo != null)
			//{
			//	if (id != null)
			//	{
			//		System.IO.File.Delete(Path.Combine(envi.WebRootPath + post.PathVideo));
			//	}
			//	post.PathVideo = UploadFile(AddOrUpdatePostVM.PathVideo, envi.WebRootPath);
			//}



			post.ToTforTotalPostTime = totalPostTime?.Name;
			var Cate = _BookingDbContext.AppCategory.Find(AddOrUpdatePostVM.IdCategory);
			var time = _BookingDbContext.AppTimes.Find(AddOrUpdatePostVM.IdTotalPostTime);
			var listTime = _BookingDbContext.AppTimes.ToList();
			if (AddOrUpdatePostVM.Haslable == true)                             // nếu có thuê Nhãn thì  lấy số ngày * 2
			{                                                                       //	||
				foreach (var item in listTime)                                      // 	||
				{                                                                   // 	||
					if (item.Name == time.Name)                                     // 	||
					{                                                               // 	||
						var soNgay = AddOrUpdatePostVM.TotalPostTime * item.Time;//	||
						if (id == null)                                                     // thêm mới
						{
							post.ExpirationDate = DateTime.Now.AddDays(soNgay);
							post.TotalMoney = (soNgay * 2000) + (Cate.Price * soNgay);
						}
						if (id != null && AddOrUpdatePostVM.Status == AppPostStatus.REPOST)  // Đăng lại
						{
							if (post.ExpirationDate >= DateTime.Now) // nếu còn trong tg đặt
							{
								post.PostStatus = AppPostStatus.APPROVED; // đã duyệt ~ đăng lại
							}
							else
							{  // nếu quá hạn tg hết hạn thì lấy tg hiện tại cộng lên
								post.ExpirationDate = DateTime.Now.AddDays(soNgay);
								post.TotalMoney = (soNgay * 2000) + (Cate.Price * soNgay);
							}
						}
					}
				}
			}
			else                                // không thuê nhãn
			{
				foreach (var item in listTime)
				{
					if (item.Name == time.Name)
					{
						var soNgay = AddOrUpdatePostVM.TotalPostTime * item.Time;

						if (id == null)
						{
							post.ExpirationDate = DateTime.Now.AddDays(soNgay);
							post.TotalMoney = (Cate.Price * soNgay);
						}
						if (id != null && AddOrUpdatePostVM.Status == AppPostStatus.REPOST)  // dang lai
						{
							if (post.ExpirationDate >= DateTime.Now) // nếu còn trong tg đặt 
							{
								post.PostStatus = AppPostStatus.APPROVED; // đã duyệt ~ đăng lại
							}
							else
							{  // nếu quá hạn tg hết hạn thì lấy tg hiện tại cộng lên
								post.ExpirationDate = DateTime.Now.AddDays(soNgay);
								post.TotalMoney = (Cate.Price * soNgay);
							}
						}
						break;
					}
				}
			}
			post.IdCategory = AddOrUpdatePostVM.IdCategory;

			post.IdAddress = null;

			post.IdCategory = Cate.IdCategory;

			if (id == null)
			{
				post.TotalPostTime = AddOrUpdatePostVM.TotalPostTime;
				post.StartDay = DateTime.Now;
			}
			if (id != null && AddOrUpdatePostVM.Status == AppPostStatus.REPOST)
			{
				if (post.ExpirationDate < DateTime.Now)  // tức là đã cập nhật nagyf thuê => sẽ thanh toán 
				{
					post.StartDay = DateTime.Now;
				}

			}
			if (Cate.AutomaticBrowsing)                             // xét duyệt bài viết tự động
			{
				if (id == null)
				{
					post.PostStatus = WebConfig.Constant.AppPostStatus.UNPAID;
				}
				else
				{
					if (post.PostStatus == AppPostStatus.HIDDEN)                // từ Ẩn
					{
						if (post.StartDay < DateTime.Now)
						{
							if (post.ExpirationDate >= DateTime.Now)
							{
								post.PostStatus = AppPostStatus.APPROVED;   // sang hiện khi còn trong tg đặt
							}
							else
							{
								TempData["ToastMessageWrg"] = "Thời gian đăng bài quá hạn";
								post.PostStatus = AppPostStatus.EXPIRED;    // sang tg hết hạn do nằm ngoài tg đặt lúc trc
							}
						}
					}
					if (post.PostStatus == AppPostStatus.APPROVED)  // dang sd
					{
						if (AddOrUpdatePostVM.Status == AppPostStatus.HIDDEN)
						{
							post.PostStatus = AppPostStatus.HIDDEN;
						}
						if (AddOrUpdatePostVM.Status == AppPostStatus.BOOKED)
						{
							post.PostStatus = AppPostStatus.BOOKED;
						}
					}
					if (post.PostStatus == AppPostStatus.PENDING)
					{
						post.PostStatus = AppPostStatus.CANCEL;
					}
					if (post.PostStatus == AppPostStatus.BOOKED && AddOrUpdatePostVM.Status == AppPostStatus.REPOST)
					{
						post.PostStatus = AppPostStatus.APPROVED;
					}
				}
			}
			else
			{
				if (id == null)
				{
					post.PostStatus = WebConfig.Constant.AppPostStatus.UNPAID;
				}
			}
			//post.IdPaymentMethod = null;
			post.IdRentalObject = AddOrUpdatePostVM.IdRentalObject;
			post.IdTypeOfService = AddOrUpdatePostVM.IdTypeOfService;

			AppUsers _user = null;
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			if (userIdClaim != null)
			{
				int userId = int.Parse(userIdClaim.Value);
				_user = _BookingDbContext.AppUser.Find(userId);
				post.IdUser = _user.IdUser;
			}
			else
			{
				TempData["ToastMessageWrg"] = "Cảnh báo người dùng không hợp lệ";
			}
			post.MaPost = null;

			if (id != null)
			{
				_BookingDbContext.Update(post);
				_BookingDbContext.SaveChanges();
			}
			else
			{
				_BookingDbContext.Add(post);
				_BookingDbContext.SaveChanges();
			}




			//if (post.appImgPosts == null)
			//{
			//	post.appImgPosts = new List<AppImgPosts>();
			//}
			if (id == null)
			{
				foreach (var item in AddOrUpdatePostVM.appImgPosts)
				{
					if (item != null)
					{
						var img = new AppImgPosts();
						img.Path = UploadFile(item, envi.WebRootPath); // Gán giá trị cho img.Path trước khi xóa
						img.CreatedDate = DateTime.Now;
						img.IdPost = post.IdPost;
						_BookingDbContext.AppImgPost.Add(img);
						_BookingDbContext.SaveChanges();
					}
				}
			}
			else
			{
				if (AddOrUpdatePostVM.appImgPosts is not null)
				{
					foreach (var item in post.appImgPosts)
					{
						System.IO.File.Delete(Path.Combine(envi.WebRootPath + item.Path));
						_BookingDbContext.AppImgPost.Remove(item);
					}
					foreach (var item in AddOrUpdatePostVM.appImgPosts)
					{
						if (item != null)
						{
							var img = new AppImgPosts();
							img.Path = UploadFile(item, envi.WebRootPath); // Gán giá trị cho img.Path trước khi xóa
							img.CreatedDate = DateTime.Now;
							img.IdPost = post.IdPost;
							_BookingDbContext.AppImgPost.Add(img);
							_BookingDbContext.SaveChanges();
						}
					}
				}
			}

			if (id == null)
			{
				// lưu vô bảng Address 
				var appAddress = new AppAddress();
				if (AddOrUpdatePostVM.appAddress.City != null)
				{
					appAddress.City = AddOrUpdatePostVM.appAddress.City;
					appAddress.District = AddOrUpdatePostVM.appAddress.District;
					appAddress.Wards = AddOrUpdatePostVM.appAddress.Wards;
					appAddress.Street = AddOrUpdatePostVM.appAddress.Street;
					appAddress.ApartmentNumber = AddOrUpdatePostVM.appAddress.ApartmentNumber;
					appAddress.CreatedDate = DateTime.Now;
					appAddress.FullAdress = AddOrUpdatePostVM.appAddress.ApartmentNumber + "/ " + AddOrUpdatePostVM.appAddress.Street + "/ " + AddOrUpdatePostVM.appAddress.Wards + "/ " + AddOrUpdatePostVM.appAddress.District + "/ " + AddOrUpdatePostVM.appAddress.City;
					_BookingDbContext.Add(appAddress);
					_BookingDbContext.SaveChanges();
				}
				post.IdAddress = appAddress.IdAddress;
			}
			post.MaPost = "PS" + DateTime.Now.ToString("yydd") + _user.IdUser.ToString() + post.IdPost.ToString();
			_BookingDbContext.Update(post);
			_BookingDbContext.SaveChanges();

			if (id != null && AddOrUpdatePostVM.Status == AppPostStatus.REPOST)  // dang lai => qua trang thanh toán
			{
				if (post.StartDay == DateTime.Now)
				{
					TempData["postId"] = post.IdPost;
					return RedirectToAction("Checkout");
				}
				TempData["ToastMessageScs"] = "Cập nhật bài đăng thành công";
				TempData["Update"] = "Update";
				return RedirectToAction("Index");
			}
			if (id != null)
			{
				var history = _BookingDbContext.AppHistoryPayment.FirstOrDefault(i => i.IdPost == id);
				if (post.TotalMoney != history.Fee)  // nếu khi thêm ngày thì tiền mới ở Post sẽ lớn hơn tiền cũ ở hítory 
				{
					TempData["postId"] = post.IdPost;
					return RedirectToAction("Checkout");
				}
				TempData["ToastMessageScs"] = "Cập nhật bài đăng thành công";
				TempData["Update"] = "Update";
				return RedirectToAction("Index");
			}

			TempData["postId"] = post.IdPost;
			return RedirectToAction("Checkout");
		}

		[HttpGet]
		public IActionResult Checkout(int? id)
		{
			var postId = TempData["postId"];
			if (postId == null && id != null)
			{
				postId = id;
			}
			var post = _BookingDbContext.AppPost.Find(postId);

			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			if (userIdClaim != null)
			{
				int userId = int.Parse(userIdClaim.Value);
				var _user = _BookingDbContext.AppUser.Find(userId);
				TempData["User"] = _user;
			}
			return View(post);
		}
		[HttpPost]
		public IActionResult Checkout(string methodPayment, int id)
		{
			var post = _BookingDbContext.AppPost.Find(id);
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim.Value);
			var _user = _BookingDbContext.AppUser.Find(userId);

			if (methodPayment == null)
			{
				TempData["ToastMessageWrg"] = "Vui lòng chọn phương thức thanh toán";
				TempData["User"] = _user;
				return View(post);
			}


			if (post != null)
			{
				if (methodPayment == "Booking.com")
				{
					var Cate = _BookingDbContext.AppCategory.FirstOrDefault(i => i.IdCategory == post.IdCategory);
					var historyPayment = new AppHistoryPayments();
					var update = TempData["Update"] as string;
					int? oldHSry = 0;
					if (update == "Update")
					{
						historyPayment = _BookingDbContext.AppHistoryPayment.FirstOrDefault(i => i.IdPost == post.IdPost);
					}
					historyPayment.MaPost = post.MaPost;
					historyPayment.AccountBalance = _user.AccountBalance;
					historyPayment.Fee = post.TotalMoney;
					historyPayment.Status = AppTransactionStatus.SUCCESS;
					historyPayment.CreatedDate = DateTime.Now;
					historyPayment.Category = Cate.Name;
					historyPayment.IdPost = post.IdPost;
					if (update == "Update")
					{
						_BookingDbContext.AppHistoryPayment.Update(historyPayment);
						_BookingDbContext.SaveChanges();
					}
					else
					{
						_BookingDbContext.AppHistoryPayment.Add(historyPayment);
						_BookingDbContext.SaveChanges();
					}

					_user.AccountBalance -= post.TotalMoney;

					_BookingDbContext.AppUser.Update(_user);
					_BookingDbContext.SaveChanges();

					if (post.PostStatus == AppPostStatus.UNPAID && historyPayment.Status == AppTransactionStatus.SUCCESS)
					{
						if (post.appCategory.AutomaticBrowsing == true)
						{
							post.PostStatus = AppPostStatus.APPROVED;
						}
						else
						{
							post.PostStatus = AppPostStatus.PENDING;
						}
					}

					_BookingDbContext.AppPost.Update(post);
					_BookingDbContext.SaveChanges();
					TempData["ToastMessageScs"] = "Thanh toán thành công";
				}
				else if (methodPayment == "PayPal")
				{
				}
				else if (methodPayment == "InternationalCard")
				{
				}
				else if (methodPayment == "ShopeePay")
				{

				}
			}

			return RedirectToAction("Index", "UserPost");
		}




		private string UploadFile(IFormFile file, string webRootPath)
		{
			var fName = file.FileName;
			fName = Path.GetFileNameWithoutExtension(fName)
				+ DateTime.Now.Ticks
				+ Path.GetExtension(fName);

			var directoryPath = Path.Combine(webRootPath, "Image", "Post");
			Directory.CreateDirectory(directoryPath); // Đảm bảo thư mục tồn tại

			var filePath = Path.Combine(directoryPath, fName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			var relativePath = "/Image/Post/" + fName;
			return relativePath;
		}

	}
}
