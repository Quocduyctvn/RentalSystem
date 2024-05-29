using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UD2605PM20h : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "AppCategoryNews",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$5PxxJmXA0XTWmwku2bv3V.OYl1R8FK6YLiWRDh3EWziw0CpWVSBG.");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$5PxxJmXA0XTWmwku2bv3V.OYl1R8FK6YLiWRDh3EWziw0CpWVSBG.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "AppCategoryNews");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$AdsbGW75NS3v/YVwSvf58OZesHzsDJDQ3bZ4dAqJuZv9HPQJues7O");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$AdsbGW75NS3v/YVwSvf58OZesHzsDJDQ3bZ4dAqJuZv9HPQJues7O");
        }
    }
}
