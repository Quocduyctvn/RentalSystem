using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RentalSystem.Areas.Admin.ViewModels;
using RentalSystem.Models;
using System.Security.Claims;
using X.PagedList;

namespace RentalSystem.Areas.Admin.Controllers
{
	public class AdminRoleController : AdminControllerBase
	{
		public AdminRoleController(RentalSystemDBConText DbContext) : base(DbContext)
		{
		}


		[Route("quan-ly-vai-tro")]
		public IActionResult Index(string keyword, int? page)
		{
			ViewBag.ListRole = _BookingDbContext.AppRole
										.ToList();
			var listRole = _BookingDbContext.AppRole
										.Include(i=> i.appUsers)
										.AsQueryable();
			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				listRole = listRole.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.Desc.ToUpper().Contains(keywordUpper));
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
			return View(listRole.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		[Route("them-vai-tro")]
		public IActionResult AddRole()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddRole(AddRoleVM model)
		{
			if (!ModelState.IsValid)
			{
				TempData["ToastMessageErr"] = "Vui lòng nhập đúng yêu cầu";
				return View(model);
			}
			if (model.IdPermission == null)
			{
				TempData["ToastMessageWrg"] = "Xảy ra lỗi trong quá trình xử lí";
				return View(model);
			}
			var arrIdPermission = model.IdPermission.Split(',');

			var role = new AppRoles();

			role.Name = model.Name;
			role.Desc = model.Desc;
			role.CreatedDate = DateTime.Now;
			role.appRolePermissions = new List<AppRolePermission>();

			foreach (var item in arrIdPermission)
			{
				var idPer = Convert.ToInt32(item);
				role.appRolePermissions.Add(new AppRolePermission
				{
					IdPermission = idPer
				});
			}

			_BookingDbContext.AppRole.Add(role);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Thêm vai trò thành công";
			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> UpdateRole(int? id)
		{
			var data = await _BookingDbContext.AppRole
							.Include(r => r.appRolePermissions)
							.FirstOrDefaultAsync(r => r.IdRole == id.Value);
			if (data == null)
			{
				return NotFound();
			}
			var model = new UpdateRoleVM
			{
				Id = data.IdRole,
				Name = data.Name,
				Desc = data.Desc,
				IdPermission = string.Join(',', data.appRolePermissions.Select(rp => rp.IdPermission)),
			};

			return View(model);
		}
		[HttpPost]
		public IActionResult UpdateRole(UpdateRoleVM model)
		{
			if (String.IsNullOrEmpty(model.IdPermission))
			{
				TempData["ToastMessageWrg"] = "Vai trò ít nhất 1 hành động";
				return RedirectToAction("UpdateRole");
			}


			var role = _BookingDbContext.AppRole.Find(model.Id);
			// kiểm tra có XÓA bớt hay không 
			string[] deletedIdPermission = new string[200];
			if (model.DeletedIdPermission != null)
			{
				if (model.DeletedIdPermission.Contains(","))  // kiểm tra xem có nhiều hơn 1 hay không
				{
					deletedIdPermission = model.DeletedIdPermission.Split(',');
				}
				else
				{
					deletedIdPermission[0] = model.DeletedIdPermission;
				}
			}

			if(deletedIdPermission.Length > 0)
			{
				foreach(var item in  deletedIdPermission)
				{
					if(item != null)
					{
						var idPer = Convert.ToInt32(item);
						var rolePer = _BookingDbContext.AppRolePermission
										.Where(i => i.IdRole == role.IdRole && i.IdPermission == idPer)
										.FirstOrDefault();
						_BookingDbContext.AppRolePermission.Remove(rolePer);
						_BookingDbContext.SaveChanges();
					}
				}
			}

			// kiểm tra có thêm mới hay không 
			string[] addedIdPermission = new string[200];
			if (model.AddedIdPermission != null)
			{
				if (model.AddedIdPermission.Contains(",")) // kiểm tra xem có nhiều hơn 1 hay không 
				{
					addedIdPermission = model.AddedIdPermission.Split(',');
				}
				else
				{
					addedIdPermission[0] = model.AddedIdPermission;
				}
			}

			if (addedIdPermission.Length > 0)
			{
				role.appRolePermissions = new List<AppRolePermission>();
				foreach (var item in addedIdPermission)
				{
					if (item != null)
					{
						var idPer = Convert.ToInt32(item);
						role.appRolePermissions.Add(new AppRolePermission
						{
							IdPermission = idPer
						});
					}
				}
			}	
			role.Name = model.Name;
			role.Desc = model.Desc;

			_BookingDbContext.AppRole.Update(role);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Cập nhật vai trò thành công";
			return RedirectToAction("Index");
		}


		[Route("xoa-vai-tro")]
		public IActionResult DeleteRole(int id)
		{
			var role = _BookingDbContext.AppRole
								.Include(i=> i.appUsers)
								.FirstOrDefault(i => i.IdRole == id);
			if (role == null)
			{
				TempData["ToastMessageWrg"] = "Đã xảy ra lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}
			var listUser = _BookingDbContext.AppUser
					.Where(i => i.IdRole == role.IdRole)
					.ToList();
			DeleteRoleVM deleteRoloVM = new DeleteRoleVM();
			deleteRoloVM.Name = role.Name;
			deleteRoloVM.Desc = role.Desc;
			deleteRoloVM.CreateDate = role.CreatedDate;
			deleteRoloVM.IdRole = id;
			deleteRoloVM.appUsers = new List<RoleDeleteVM_User>();

			foreach (var item in listUser)
			{
				var user = new RoleDeleteVM_User();
				user.IdUser = item.IdUser;
				user.Name = item.Name;
				deleteRoloVM.appUsers.Add(user);
			}
			ViewBag.ListRole = _BookingDbContext.AppRole.ToList();

			return View(deleteRoloVM);
		}

		[HttpPost]
		public IActionResult DeleteRole(DeleteRoleVM model, int id)
		{
			var role = _BookingDbContext.AppRole
								.FirstOrDefault(i => i.IdRole == id);
			if (role == null)
			{
				TempData["ToastMessageWrg"] = "Đã xảy ra lỗi trong quá trình xử lí";
				return RedirectToAction("Index");
			}


			// Cập nhật Role 
			var listUser = _BookingDbContext.AppUser
					.Where(i => i.IdRole == role.IdRole)
					.ToList();
			if(listUser.Count > 0 && listUser != null)
			{
				foreach (var item in listUser)
				{
					item.IdRole = model.IdNewRole;
					_BookingDbContext.AppUser.Update(item);
					_BookingDbContext.SaveChanges();
				}
			}

			// Xóa item Role ở bảng RolePermission  
			var RolePer = _BookingDbContext.AppRolePermission
												.Where(i => i.IdRole == id)
												.ToList();
			if (RolePer.Count > 0 && RolePer != null)
			{
				foreach (var item in RolePer)
				{
					_BookingDbContext.AppRolePermission.Remove(item);
					_BookingDbContext.SaveChanges();
				}
			}
			// xóa Role 
			_BookingDbContext.AppRole.Remove(role);
			_BookingDbContext.SaveChanges();
			TempData["ToastMessageScs"] = "Xóa vai trò thành công";
			return RedirectToAction("Index");
		}
	}
}
