using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalSystem.Models;
using RentalSystem.WebConfig.Constant;
using static RentalSystem.WebConfig.Constant.AuthConst;

namespace RentalSystem.DataSeeders
{
    public static class AppPermissionSeeder
    {
        public static void SeedData(this EntityTypeBuilder<AppPermissions> builder)
        {
            var now = new DateTime(year: 2024, month: 3, day: 10);
            var groupName = "";

            #region Cấu hình bảng AppRole 
            groupName = "Quản lý phân quyền";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppRoles.CREATE,
                    Code = "CREATE",
                    Table = "AppRole",
                    GroupName = groupName,
                    Desc = "Thêm quyền",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppRoles.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppRole",
                    GroupName = groupName,
                    Desc = "Xem danh sách quyền",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppRoles.UPDATE,
                    Code = "UPDATE",
                    Table = "AppRole",
                    GroupName = groupName,
                    Desc = "Cập nhật quyền",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppRoles.DELETE,
                    Code = "UPDATE",
                    Table = "AppRole",
                    GroupName = groupName,
                    Desc = "Xóa quyền",
                    CreatedDate = now
                }
            );
            #endregion

            #region Cấu hình bảng  AppCategory
            groupName = "Quản lý Dịch vụ";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppCategory.CREATE,
                    Code = "CREATE",
                    Table = "AppCategory",
                    GroupName = groupName,
                    Desc = "Thêm",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppCategory.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = "AppCategory",
                    GroupName = groupName,
                    Desc = "Xem chi tiết",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppCategory.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppCategory",
                    GroupName = groupName,
                    Desc = "Xem danh sách",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppCategory.UPDATE,
                    Code = "UPDATE",
                    Table = "AppCategory",
                    GroupName = groupName,
                    Desc = "Cập nhật",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppCategory.DELETE,
                    Code = "DELETE",
                    Table = "AppCategory",
                    GroupName = groupName,
                    Desc = "Xóa",
                    CreatedDate = now
                }
            );
            #endregion

            #region Cấu hình bảng Contact
            groupName = "Quản lý Liên hệ";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppContacts.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppContact",
                    GroupName = groupName,
                    Desc = "Xem danh sách",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppContacts.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = "AppContact",
                    GroupName = groupName,
                    Desc = "Xem chi tiết",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppContacts.FEEDBACK,
                    Code = "FEEDBACK",
                    Table = "AppContact",
                    GroupName = groupName,
                    Desc = "Phản hồi",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppContacts.DELETE,
                    Code = "DELETE",
                    Table = "AppContact",
                    GroupName = groupName,
                    Desc = "Xóa",
                    CreatedDate = now
                }
            );
            #endregion

            //#region Cấu hình bảng AppHistoryPayments
            //groupName = "Quản lý Lịch sử Thanh toán";
            //builder.HasData(
            //    new AppPermissions
            //    {
            //        IdPermission = AuthConst.AppHistoryPayments.VIEW_LIST,
            //        Code = "VIEW_LIST",
            //        Table = "AppHistoryPayment",
            //        GroupName = groupName,
            //        Desc = "Xem danh sách Lịch sử thanh toán",
            //        CreatedDate = now
            //    },
            //    new AppPermissions
            //    {
            //        IdPermission = AuthConst.AppHistoryPayments.VIEW_DETAIL,
            //        Code = "VIEW_DETAIL",
            //        Table = "AppHistoryPayment",
            //        GroupName = groupName,
            //        Desc = "Xem chi tiết Lịch sử thanh toán",
            //        CreatedDate = now
            //    }
            //);
            //#endregion

            #region Cấu hình bảng AppPosts
            groupName = "Quản lý Đăng bài";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Xem danh sách",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Xem chi tiết",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.CREATE,
                    Code = "CREATE",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Thêm mới",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.UPDATE,
                    Code = "UPDATE",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Cập nhật",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.DELETE,
                    Code = "DELETE",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Xóa bài viết",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppPosts.PUBLIC,
                    Code = "PUBLIC",
                    Table = "AppPost",
                    GroupName = groupName,
                    Desc = "Thay đổi hiển thị",
                    CreatedDate = now
                },
				new AppPermissions
				{
					IdPermission = AuthConst.AppPosts.REVIEW,
					Code = "REVIEW",
					Table = "AppPost",
					GroupName = groupName,
					Desc = "Duyệt bài",
					CreatedDate = now
				}
			);
            #endregion

            #region Cấu hình bảng TypeOfServices 
            groupName = "Quản lý Danh mục thuê";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppTypeOfService.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppTypeOfService",
                    GroupName = groupName,
                    Desc = "Xem danh sách",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppTypeOfService.CREATE,
                    Code = "CREATE",
                    Table = "AppTypeOfService",
                    GroupName = groupName,
                    Desc = "Tạo mới",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppTypeOfService.UPDATE,
                    Code = "UPDATE",
                    Table = "AppTypeOfService",
                    GroupName = groupName,
                    Desc = "Cập nhật",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppTypeOfService.DELETE,
                    Code = "DELETE",
                    Table = "AppTypeOfService",
                    GroupName = groupName,
                    Desc = "Xóa",
                    CreatedDate = now
                }
            );
            #endregion

            #region Cấu hình bảng AppUser
            groupName = "Quản lý người dùng";
            builder.HasData(
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Xem danh sách",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.CREATE,
                    Code = "CREATE",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Tạo mới tài khoản",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.UPDATE,
                    Code = "UPDATE",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Cập nhật  tài khoản",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.DELETE,
                    Code = "DELETE",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Xóa  tài khoản",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.IsCHANGEBLOCK,
                    Code = "IsCHANGEBLOCK",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Thay đổi Block",
                    CreatedDate = now
                },
                new AppPermissions
                {
                    IdPermission = AuthConst.AppUsers.UPDATE_PWD,
                    Code = "UPDATE_PWD",
                    Table = "AppUser",
                    GroupName = groupName,
                    Desc = "Cập nhật Mật khẩu",
                    CreatedDate = now
                }
            );
			#endregion

			#region Cấu hình bảng AppPolicy
			groupName = "Quản lý Chính sách";
			builder.HasData(
				new AppPermissions
				{
					IdPermission = AuthConst.AppPolicy.VIEW_LIST,
					Code = "VIEW_LIST",
					Table = "AppPolicy",
					GroupName = groupName,
					Desc = "Xem danh sách Chính sách",
					CreatedDate = now
				},
				new AppPermissions
				{
					IdPermission = AuthConst.AppPolicy.CREATE,
					Code = "CREATE",
					Table = "AppPolicy",
					GroupName = groupName,
					Desc = "Tạo mới Chính sách",
					CreatedDate = now
				},
				new AppPermissions
				{
					IdPermission = AuthConst.AppPolicy.UPDATE,
					Code = "UPDATE",
					Table = "AppPolicy",
					GroupName = groupName,
					Desc = "Cập nhật Chính sách",
					CreatedDate = now
				},
				new AppPermissions
				{
					IdPermission = AuthConst.AppPolicy.DELETE,
					Code = "DELETE",
					Table = "AppPolicy",
					GroupName = groupName,
					Desc = "Xóa Chính sách",
					CreatedDate = now
				},
				new AppPermissions
				{
					IdPermission = AuthConst.AppPolicy.DETAIL,
					Code = "DETAIL",
					Table = "AppPolicy",
					GroupName = groupName,
					Desc = "Xem chi tiết",
					CreatedDate = now
				}
			);
			#endregion

		}

	}
}
