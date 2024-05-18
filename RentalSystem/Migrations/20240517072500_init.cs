using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAddress",
                columns: table => new
                {
                    IdAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    District = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Wards = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullAdress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAddress", x => x.IdAddress);
                });

            migrationBuilder.CreateTable(
                name: "AppCategory",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Advantage = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Object = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    TitleColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AutomaticBrowsing = table.Column<bool>(type: "bit", nullable: false),
                    DisplayNumberPhone = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategory", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "AppContact",
                columns: table => new
                {
                    IdContact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppContact", x => x.IdContact);
                });

            migrationBuilder.CreateTable(
                name: "AppPermission",
                columns: table => new
                {
                    IdPermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermission", x => x.IdPermission);
                });

            migrationBuilder.CreateTable(
                name: "AppRentalObject",
                columns: table => new
                {
                    IdRentalObject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRentalObject", x => x.IdRentalObject);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "AppTimes",
                columns: table => new
                {
                    IdTime = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTimes", x => x.IdTime);
                });

            migrationBuilder.CreateTable(
                name: "AppTypeOfService",
                columns: table => new
                {
                    IdTypeOfService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTypeOfService", x => x.IdTypeOfService);
                });

            migrationBuilder.CreateTable(
                name: "AppRolePermission",
                columns: table => new
                {
                    IdRolePermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    IdPermission = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRolePermission", x => x.IdRolePermission);
                    table.ForeignKey(
                        name: "FK_AppRolePermission_AppPermission_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "AppPermission",
                        principalColumn: "IdPermission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRolePermission_AppRole_IdRole",
                        column: x => x.IdRole,
                        principalTable: "AppRole",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumberZL = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LinkFB = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: true),
                    AccountBalance = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_AppUser_AppRole_IdRole",
                        column: x => x.IdRole,
                        principalTable: "AppRole",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPolicy",
                columns: table => new
                {
                    IdPolicy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPolicy", x => x.IdPolicy);
                    table.ForeignKey(
                        name: "FK_AppPolicy_AppUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUser",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppPost",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPost = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Haslable = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    RtalPrice = table.Column<double>(type: "float", nullable: false),
                    ToTforRtalPrice = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalPostTime = table.Column<double>(type: "float", nullable: false),
                    ToTforTotalPostTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    IdRentalObject = table.Column<int>(type: "int", nullable: false),
                    IdAddress = table.Column<int>(type: "int", nullable: true),
                    IdTypeOfService = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPost", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK_AppPost_AppAddress_IdAddress",
                        column: x => x.IdAddress,
                        principalTable: "AppAddress",
                        principalColumn: "IdAddress");
                    table.ForeignKey(
                        name: "FK_AppPost_AppCategory_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "AppCategory",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPost_AppRentalObject_IdRentalObject",
                        column: x => x.IdRentalObject,
                        principalTable: "AppRentalObject",
                        principalColumn: "IdRentalObject",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPost_AppTypeOfService_IdTypeOfService",
                        column: x => x.IdTypeOfService,
                        principalTable: "AppTypeOfService",
                        principalColumn: "IdTypeOfService");
                    table.ForeignKey(
                        name: "FK_AppPost_AppUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUser",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppHistoryPayment",
                columns: table => new
                {
                    IdHistoryPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPost = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountBalance = table.Column<double>(type: "float", nullable: false),
                    Fee = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHistoryPayment", x => x.IdHistoryPayment);
                    table.ForeignKey(
                        name: "FK_AppHistoryPayment_AppPost_IdPost",
                        column: x => x.IdPost,
                        principalTable: "AppPost",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppImgPost",
                columns: table => new
                {
                    IdImgPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdPost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImgPost", x => x.IdImgPost);
                    table.ForeignKey(
                        name: "FK_AppImgPost_AppPost_IdPost",
                        column: x => x.IdPost,
                        principalTable: "AppPost",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRequest",
                columns: table => new
                {
                    IdRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRequest", x => x.IdRequest);
                    table.ForeignKey(
                        name: "FK_AppRequest_AppPost_IdPost",
                        column: x => x.IdPost,
                        principalTable: "AppPost",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRequest_AppUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUser",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "AppContactFeedback",
                columns: table => new
                {
                    IdContactFback = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdContact = table.Column<int>(type: "int", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdRequest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppContactFeedback", x => x.IdContactFback);
                    table.ForeignKey(
                        name: "FK_AppContactFeedback_AppContact_IdContact",
                        column: x => x.IdContact,
                        principalTable: "AppContact",
                        principalColumn: "IdContact");
                    table.ForeignKey(
                        name: "FK_AppContactFeedback_AppRequest_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "AppRequest",
                        principalColumn: "IdRequest",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppContactFeedback_AppUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUser",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppPermission",
                columns: new[] { "IdPermission", "Code", "CreatedDate", "Desc", "GroupName", "Table" },
                values: new object[,]
                {
                    { 1101, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý Dịch vụ", "AppCategory" },
                    { 1102, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thêm", "Quản lý Dịch vụ", "AppCategory" },
                    { 1103, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật", "Quản lý Dịch vụ", "AppCategory" },
                    { 1104, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa", "Quản lý Dịch vụ", "AppCategory" },
                    { 1105, "VIEW_DETAIL", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem chi tiết", "Quản lý Dịch vụ", "AppCategory" },
                    { 1201, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý Liên hệ", "AppContact" },
                    { 1202, "VIEW_DETAIL", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem chi tiết", "Quản lý Liên hệ", "AppContact" },
                    { 1203, "FEEDBACK", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phản hồi", "Quản lý Liên hệ", "AppContact" },
                    { 1204, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa", "Quản lý Liên hệ", "AppContact" },
                    { 1501, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý Đăng bài", "AppPost" },
                    { 1502, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thêm mới", "Quản lý Đăng bài", "AppPost" },
                    { 1503, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật", "Quản lý Đăng bài", "AppPost" },
                    { 1504, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa bài viết", "Quản lý Đăng bài", "AppPost" },
                    { 1505, "PUBLIC", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thay đổi hiển thị", "Quản lý Đăng bài", "AppPost" },
                    { 1506, "VIEW_DETAIL", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem chi tiết", "Quản lý Đăng bài", "AppPost" },
                    { 1507, "REVIEW", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Duyệt bài", "Quản lý Đăng bài", "AppPost" },
                    { 1601, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách quyền", "Quản lý phân quyền", "AppRole" },
                    { 1602, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thêm quyền", "Quản lý phân quyền", "AppRole" },
                    { 1603, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật quyền", "Quản lý phân quyền", "AppRole" },
                    { 1604, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa quyền", "Quản lý phân quyền", "AppRole" },
                    { 1701, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý Danh mục thuê", "AppTypeOfService" },
                    { 1702, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tạo mới", "Quản lý Danh mục thuê", "AppTypeOfService" },
                    { 1703, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật", "Quản lý Danh mục thuê", "AppTypeOfService" },
                    { 1704, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa", "Quản lý Danh mục thuê", "AppTypeOfService" },
                    { 1801, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý người dùng", "AppUser" },
                    { 1802, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tạo mới tài khoản", "Quản lý người dùng", "AppUser" },
                    { 1803, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật  tài khoản", "Quản lý người dùng", "AppUser" },
                    { 1804, "UPDATE_PWD", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật Mật khẩu", "Quản lý người dùng", "AppUser" },
                    { 1805, "IsCHANGEBLOCK", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thay đổi Block", "Quản lý người dùng", "AppUser" },
                    { 1806, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa  tài khoản", "Quản lý người dùng", "AppUser" },
                    { 1901, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách Chính sách", "Quản lý Chính sách", "AppPolicy" },
                    { 1902, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tạo mới Chính sách", "Quản lý Chính sách", "AppPolicy" },
                    { 1903, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật Chính sách", "Quản lý Chính sách", "AppPolicy" },
                    { 1904, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa Chính sách", "Quản lý Chính sách", "AppPolicy" },
                    { 1905, "DETAIL", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem chi tiết", "Quản lý Chính sách", "AppPolicy" }
                });

            migrationBuilder.InsertData(
                table: "AppRentalObject",
                columns: new[] { "IdRentalObject", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hộ gia đình" },
                    { 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhân viên công sở" },
                    { 3, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Học sinh - Sinh viên" },
                    { 4, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doanh nghiệp" },
                    { 5, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tùy chọn khác" }
                });

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "IdRole", "CreatedDate", "Desc", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khách hàng", "Khách hàng" },
                    { 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quản trị toàn bộ hệ thống", "Quản trị hệ thống" }
                });

            migrationBuilder.InsertData(
                table: "AppTimes",
                columns: new[] { "IdTime", "CreatedDate", "Name", "Time" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giờ", 0 },
                    { 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngày", 1 },
                    { 3, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tháng", 30 },
                    { 4, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Năm", 365 }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "IdUser", "AccountBalance", "Address", "Avatar", "Code", "CreatedDate", "Email", "IdRole", "IsBlock", "LinkFB", "Name", "Password", "PhoneNumberZL" },
                values: new object[,]
                {
                    { 1, 0.0, "Thành phố Cần thơ", "/Image/AvatarDefault.png", "US2405171", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "quocduyctvn@gmail.com", 2, false, "https://www.facebook.com/profile.php?id=100066548050837&mibextid=ZbWKwL", "admin", "$2a$10$9mZ6KmmnlVcjfAgXA5.AZeOUuKgXDJjJWMlBDBKTD8PDbDP4epKju", "0901007221" },
                    { 2, 0.0, "Thành phố Cần thơ", "/Image/AvatarDefault.png", "US2405172", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin_test@gmail.com", 2, false, "", "admin02", "$2a$10$9mZ6KmmnlVcjfAgXA5.AZeOUuKgXDJjJWMlBDBKTD8PDbDP4epKju", "0945255664" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppContactFeedback_IdContact",
                table: "AppContactFeedback",
                column: "IdContact",
                unique: true,
                filter: "[IdContact] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppContactFeedback_IdRequest",
                table: "AppContactFeedback",
                column: "IdRequest",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppContactFeedback_IdUser",
                table: "AppContactFeedback",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_AppHistoryPayment_IdPost",
                table: "AppHistoryPayment",
                column: "IdPost",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppImgPost_IdPost",
                table: "AppImgPost",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_AppPolicy_IdUser",
                table: "AppPolicy",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_AppPost_IdAddress",
                table: "AppPost",
                column: "IdAddress",
                unique: true,
                filter: "[IdAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppPost_IdCategory",
                table: "AppPost",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_AppPost_IdRentalObject",
                table: "AppPost",
                column: "IdRentalObject");

            migrationBuilder.CreateIndex(
                name: "IX_AppPost_IdTypeOfService",
                table: "AppPost",
                column: "IdTypeOfService");

            migrationBuilder.CreateIndex(
                name: "IX_AppPost_IdUser",
                table: "AppPost",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_AppRequest_IdPost",
                table: "AppRequest",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_AppRequest_IdUser",
                table: "AppRequest",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_AppRolePermission_IdPermission",
                table: "AppRolePermission",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_AppRolePermission_IdRole",
                table: "AppRolePermission",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_IdRole",
                table: "AppUser",
                column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppContactFeedback");

            migrationBuilder.DropTable(
                name: "AppHistoryPayment");

            migrationBuilder.DropTable(
                name: "AppImgPost");

            migrationBuilder.DropTable(
                name: "AppPolicy");

            migrationBuilder.DropTable(
                name: "AppRolePermission");

            migrationBuilder.DropTable(
                name: "AppTimes");

            migrationBuilder.DropTable(
                name: "AppContact");

            migrationBuilder.DropTable(
                name: "AppRequest");

            migrationBuilder.DropTable(
                name: "AppPermission");

            migrationBuilder.DropTable(
                name: "AppPost");

            migrationBuilder.DropTable(
                name: "AppAddress");

            migrationBuilder.DropTable(
                name: "AppCategory");

            migrationBuilder.DropTable(
                name: "AppRentalObject");

            migrationBuilder.DropTable(
                name: "AppTypeOfService");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppRole");
        }
    }
}
