using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RentalSystem.Areas.User.ViewModels;
using RentalSystem.Models;
using RentalSystem.WebConfig.Constant;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminPostController : AdminControllerBase
	{
		public AdminPostController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("quan-ly-bai-dang")]
		public IActionResult Index(string? keyword, int? selectedCategoryId, AppPostStatus? status, int? page)
		{

			ViewBag.sumList = _BookingDbContext.AppPost.ToList();
			var post = _BookingDbContext.AppPost
							.Include(i => i.appCategory)
							.Include(i => i.appTypeOfService)
							.Include(i => i.appUsers)
							.Include(i => i.appRentalObject)
							.Include(i => i.appHistoryPayments)
							.OrderByDescending(i=> i.CreatedDate)
							.ToList();
			// check  gói tin hết hạn 
			foreach(var item in post)
			{
				if(item.ExpirationDate < DateTime.Now && item.PostStatus != AppPostStatus.EXPIRED)
				{
					item.PostStatus = AppPostStatus.EXPIRED;

					TempData["ToastMessageWrg"] = "Gói tin có mã [" + item.MaPost + "] đã hết hạn đăng bài";
					_BookingDbContext.AppPost.Update(item);
					_BookingDbContext.SaveChanges();

				}
			}
			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				post = post.Where(i =>
				{
					return i.Title.ToUpper().Contains(keywordUpper) || i.appCategory.Name.ToUpper().Contains(keywordUpper) || i.MaPost.ToUpper().Contains(keywordUpper);
				}).ToList();

				TempData["searched"] = "searched";
			}
			ViewBag.Category = _BookingDbContext.AppCategory  // bộ loc 
								.OrderByDescending(i => i.Price)
								.ToList();
			// lọc bộ lọc 
			var Cate = new AppCategory();
			if (selectedCategoryId != null && selectedCategoryId > 0)
			{
				post = post.Where(i => i.IdCategory == selectedCategoryId).ToList();
				Cate = _BookingDbContext.AppCategory.Find(selectedCategoryId);
				ViewBag.searchName = Cate.Name;
				TempData["searched"] = "searched";
			}
			if (status != null)
			{
				post = post.Where(i => i.PostStatus == status).ToList();

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
				TempData["searched"] = "searched";
			}

			//===================PHAN QUYEN=====================
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = 0;
			if (userIdClaim != null)
			{
				userId = int.Parse(userIdClaim.Value);
				var user = _BookingDbContext.AppUser
									.Include(i => i.appRole)
									.FirstOrDefault(i => i.IdUser == userId);
				var IdRole = user.IdRole;
                ViewBag.RolePer = _BookingDbContext.AppRolePermission
                                    .Where(i => i.IdRole == IdRole)
                                    .ToList();

            }
			return View(post.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		[Route("them-bai-dang")]
		public IActionResult AddPost()
		{
			var postVM = new AddOrUpdatePostVM();
			ViewBag.AppTypeOfServices = _BookingDbContext.AppTypeOfService.ToList();
			ViewBag.RtalPriceOfCateTime = _BookingDbContext.AppTimes.ToList();   // lấy list  thời gian người chủ sở hữu định cho thuê
			ViewBag.RtalObject = _BookingDbContext.AppRentalObject.ToList();
			ViewBag.AppCate = _BookingDbContext.AppCategory.OrderByDescending(i => i.Price).ToList();
			return View(postVM);
		}
		[HttpPost]
		public IActionResult AddPost(AddOrUpdatePostVM postVM, [FromServices] IWebHostEnvironment envi)
		{
			if (postVM == null)
			{
				TempData["ToastMessageWrg"] = "Vui lòng kiểm tra dữ liệu hợp lệ";
				return View(postVM);
			}


			if (postVM.TotalPostTime == null)
			{
				TempData["ToastMessageWrg"] = "Thời gian đăng bài toàn bộ đang trống hoặc là null.";
				return View(postVM);
			}

			var totalPostTime = _BookingDbContext.AppTimes.Find(postVM.IdTotalPostTime);
			if (postVM.TotalPostTime < 5 && totalPostTime.Name == "Ngày")
			{
				TempData["ToastMessageWrg"] = "Bài đăng phải tối thiểu 5 ngày";
				return View(postVM);
			}

			var post = new AppPosts();
			post.Summary = postVM.Summary;
			post.Title = postVM.Title;
			post.Desc = postVM.Desc;
			post.Area = postVM.Area;  // dien tich
			post.CreatedDate = DateTime.Now.Date;
			post.IsPublic = true;
			var timeRtalPrice = _BookingDbContext.AppTimes.Find(postVM.IdTimeRtalPrice);
			post.RtalPrice = postVM.RtalPrice;
			post.ToTforRtalPrice = timeRtalPrice.Name;



			post.Haslable = postVM.Haslable;
			if (postVM.Path != null)
			{
				post.Path = UploadFile(postVM.Path, envi.WebRootPath);
			}

			post.ToTforTotalPostTime = totalPostTime?.Name;
			var Cate = _BookingDbContext.AppCategory.Find(postVM.IdCategory);
			var time = _BookingDbContext.AppTimes.Find(postVM.IdTotalPostTime);
			var listTime = _BookingDbContext.AppTimes.ToList();
			if (postVM.Haslable == true)                             // nếu có thuê Nhãn thì  lấy số ngày * 2
			{                                                                       //	||
				foreach (var item in listTime)                                      // 	||
				{                                                                   // 	||
					if (item.Name == time.Name)                                     // 	||
					{                                                               // 	||
						var soNgay = postVM.TotalPostTime * item.Time;//	||
						post.ExpirationDate = DateTime.Now.AddDays(soNgay);
						post.TotalMoney = (soNgay * 2000) + (Cate.Price * soNgay);
					}
				}
			}
			else                                // không thuê nhãn
			{
				foreach (var item in listTime)
				{
					if (item.Name == time.Name)
					{
						var soNgay = postVM.TotalPostTime * item.Time;
						post.ExpirationDate = DateTime.Now.AddDays(soNgay);
						post.TotalMoney = (Cate.Price * soNgay);
						break;
					}
				}
			}

			post.IdCategory = postVM.IdCategory;
			post.IdAddress = null;
			post.IdCategory = Cate.IdCategory;
			post.TotalPostTime = postVM.TotalPostTime;
			post.StartDay = DateTime.UtcNow;

			if (Cate.AutomaticBrowsing)                             // xét duyệt bài viết tự động
			{
				post.PostStatus = WebConfig.Constant.AppPostStatus.APPROVED; // tự đọng đăng bài 
			}
			else
			{
				post.PostStatus = WebConfig.Constant.AppPostStatus.PENDING;  // chờ duyệt 
			}
			//post.IdPaymentMethod = null;
			post.IdRentalObject = postVM.IdRentalObject;
			post.IdTypeOfService = postVM.IdTypeOfService;

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

			_BookingDbContext.Add(post);
			_BookingDbContext.SaveChanges();


			foreach (var item in postVM.appImgPosts)
			{
				if (item != null)
				{
					var img = new AppImgPosts();
					img.Path = UploadFile(item, envi.WebRootPath);
					img.CreatedDate = DateTime.Now;
					img.IdPost = post.IdPost;
					_BookingDbContext.AppImgPost.Add(img);
					_BookingDbContext.SaveChanges();
				}
			}

			var appAddress = new AppAddress();
			if (postVM.appAddress.City != null)
			{
				appAddress.City = postVM.appAddress.City;
				appAddress.District = postVM.appAddress.District;
				appAddress.Wards = postVM.appAddress.Wards;
				appAddress.Street = postVM.appAddress.Street;
				appAddress.ApartmentNumber = postVM.appAddress.ApartmentNumber;
				appAddress.CreatedDate = DateTime.Now;
				appAddress.FullAdress = postVM.appAddress.ApartmentNumber + "/ " + postVM.appAddress.Street + "/ " + postVM.appAddress.Wards + "/ " + postVM.appAddress.District + "/ " + postVM.appAddress.City;
				_BookingDbContext.Add(appAddress);
				_BookingDbContext.SaveChanges();
			}
			post.IdAddress = appAddress.IdAddress;

			post.MaPost = "PS" + DateTime.Now.ToString("yydd") + _user.IdUser.ToString() + post.IdPost.ToString();

			_BookingDbContext.Update(post);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Thêm mới bài đăng thành công";
			return RedirectToAction("Index");
		}


		public IActionResult Updatestatus(int? id)
		{
			if(id == null)
			{
				TempData["ToastMessageErr"] = "Đã xảy ra lỗi trong quá trình thực thi";
				return RedirectToAction("Index"); 
			}
			var post = _BookingDbContext.AppPost.Find(id);
			post.PostStatus = AppPostStatus.APPROVED; // cập nhật trạng thái thành Đã duyệt

			_BookingDbContext.AppPost.Update(post);
			_BookingDbContext.SaveChanges(true);
			TempData["ToastMessageScs"] = "Cập nhật nhthành công";
			return RedirectToAction("Index");
		}


		[Route("cap-nhat-bai-dang")]
		public IActionResult UpdatePost(int? id)
		{
			if(id == null)
			{
				TempData["ToastMessageScs"] = "Xảy ra lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}
			var postVM = new AddOrUpdatePostVM();
			var post = _BookingDbContext.AppPost
										.Include(i => i.appTypeOfService)
										.Include(i => i.appCategory)
										.Include(i => i.appRentalObject)
										.Include(i => i.appImgPosts)
										.FirstOrDefault(i => i.IdPost == id);

            postVM.Summary = post.Summary;
            postVM.Title = post.Title;
			postVM.Desc = post.Desc;
			postVM.MaPost = post.MaPost;
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

			ViewBag.AppTypeOfServices = _BookingDbContext.AppTypeOfService.ToList();
			ViewBag.RtalPriceOfCateTime = _BookingDbContext.AppTimes.ToList();   // lấy list  thời gian người chủ sở hữu định cho thuê
			ViewBag.RtalObject = _BookingDbContext.AppRentalObject.ToList();
			ViewBag.AppCate = _BookingDbContext.AppCategory.OrderByDescending(i => i.Price).ToList();

			return View(postVM);	
		}
		[HttpPost]
		public IActionResult UpdatePost(int? id, AddOrUpdatePostVM model, [FromServices] IWebHostEnvironment envi)
		{
			if (model == null)
			{
				TempData["ToastMessageWrg"] = "Vui lòng kiểm tra dữ liệu hợp lệ";
				return View(model);
			}

			var post = _BookingDbContext.AppPost.FirstOrDefault(i => i.IdPost == id);
			post.Summary = model.Summary;
			post.Title = model.Title;
			post.Desc = model.Desc;
			post.Area = model.Area;  // dien tich
			post.CreatedDate = DateTime.Now.Date;

			var timeRtalPrice = _BookingDbContext.AppTimes.Find(model.IdTimeRtalPrice);
			post.RtalPrice = model.RtalPrice;
			post.ToTforRtalPrice = timeRtalPrice.Name;
			post.Haslable = model.Haslable;

			if (model.Path != null)
			{
				// xóa file cũ 
				System.IO.File.Delete(Path.Combine(envi.WebRootPath + post.Path));
				// thêm file mới 
				post.Path = UploadFile(model.Path, envi.WebRootPath);
			}

			post.IdRentalObject = model.IdRentalObject;
			post.IdTypeOfService = model.IdTypeOfService;

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

			_BookingDbContext.Update(post);
			_BookingDbContext.SaveChanges();

			if (model.appImgPosts is not null)
			{
				foreach (var item in post.appImgPosts)
				{
					System.IO.File.Delete(Path.Combine(envi.WebRootPath + item.Path));
					_BookingDbContext.AppImgPost.Remove(item);
				}
				foreach (var item in model.appImgPosts)
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

			_BookingDbContext.Update(post);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật bài đăng thành công";
			return RedirectToAction("Index");
		}


		public IActionResult Delete(string id, [FromServices] IWebHostEnvironment envi)
		{
			int Id = int.Parse(id);
			var post = _BookingDbContext.AppPost
									.Include(i=> i.appAddress)
									.Include(i => i.appImgPosts)
									.Include(i=> i.appHistoryPayments)
									.FirstOrDefault(i=> i.IdPost == Id);
			if (post != null)
			{
				if(post.appImgPosts.Count > 0)
				{
					foreach (var item in post.appImgPosts)
					{
						var filePath = Path.Combine(envi.WebRootPath, item.Path.TrimStart('/'));

						if (System.IO.File.Exists(filePath))
						{
							System.IO.File.Delete(filePath);
						}
					}
					_BookingDbContext.AppImgPost.RemoveRange(post.appImgPosts);
					_BookingDbContext.SaveChanges();
				}
				if(post.appAddress != null)
				{
					_BookingDbContext.Remove(post.appAddress);
					_BookingDbContext.SaveChanges();
				}
				if(post.appHistoryPayments != null)
				{
					_BookingDbContext.AppHistoryPayment.Remove(post.appHistoryPayments);
					_BookingDbContext.SaveChanges();
				}


				var img = Path.Combine(envi.WebRootPath, post.Path.TrimStart('/'));
				if (System.IO.File.Exists(img))
				{
					System.IO.File.Delete(img);
				}

				_BookingDbContext.Remove(post);
				_BookingDbContext.SaveChanges();
				TempData["ToastMessageScs"] = "Xóa bài đăng thành công";
			}
			return RedirectToAction("Index");
		}


		public IActionResult UnPublic(int id)
		{
			var post = _BookingDbContext.AppPost.Find(id);
			post.IsPublic = false;
			_BookingDbContext.AppPost.Update(post);
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminPost");
		}
		public IActionResult Public(int id)
		{
			var post = _BookingDbContext.AppPost.Find(id);
			post.IsPublic = true;
			_BookingDbContext.AppPost.Update(post);
			_BookingDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminPost");
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
