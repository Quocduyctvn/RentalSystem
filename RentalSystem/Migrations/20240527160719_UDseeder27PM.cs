using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UDseeder27PM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AppNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AppPermission",
                columns: new[] { "IdPermission", "Code", "CreatedDate", "Desc", "GroupName", "Table" },
                values: new object[,]
                {
                    { 1401, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý loại tin tức", "AppCateNews" },
                    { 1402, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thêm mới", "Quản lý loại tin tức", "AppCateNews" },
                    { 1403, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật", "Quản lý loại tin tức", "AppCateNews" },
                    { 1404, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa danh mục", "Quản lý loại tin tức", "AppCateNews" },
                    { 1405, "DELETE_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa nhiều tin", "Quản lý loại tin tức", "AppCateNews" },
                    { 2101, "VIEW_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem danh sách", "Quản lý tin tức", "AppNews" },
                    { 2102, "CREATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thêm mới", "Quản lý tin tức", "AppNews" },
                    { 2103, "UPDATE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cập nhật", "Quản lý tin tức", "AppNews" },
                    { 2104, "DELETE", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa bài viết", "Quản lý tin tức", "AppNews" },
                    { 2105, "DELETE_LIST", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xóa nhiều tin", "Quản lý tin tức", "AppNews" },
                    { 2106, "VIEW_DETAIL", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xem chi tiết", "Quản lý tin tức", "AppNews" }
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405271", "$2a$10$iRwHLr5IZBPO0Dhq1ymSiuEd8Tnj313kWLaFEuP7USAQSuoxerMCa" });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405272", "$2a$10$iRwHLr5IZBPO0Dhq1ymSiuEd8Tnj313kWLaFEuP7USAQSuoxerMCa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 1401);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 1402);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 1403);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 1404);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 1405);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2101);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2102);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2103);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2104);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2105);

            migrationBuilder.DeleteData(
                table: "AppPermission",
                keyColumn: "IdPermission",
                keyValue: 2106);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AppNews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405261", "$2a$10$5PxxJmXA0XTWmwku2bv3V.OYl1R8FK6YLiWRDh3EWziw0CpWVSBG." });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405262", "$2a$10$5PxxJmXA0XTWmwku2bv3V.OYl1R8FK6YLiWRDh3EWziw0CpWVSBG." });
        }
    }
}
