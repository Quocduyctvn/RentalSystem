using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UD2605PM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppNews_AppcategoryNews_IdCateNews",
                table: "AppNews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppcategoryNews",
                table: "AppcategoryNews");

            migrationBuilder.RenameTable(
                name: "AppcategoryNews",
                newName: "AppCategoryNews");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AppCategoryNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCategoryNews",
                table: "AppCategoryNews",
                column: "IdCateNews");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppNews_AppCategoryNews_IdCateNews",
                table: "AppNews",
                column: "IdCateNews",
                principalTable: "AppCategoryNews",
                principalColumn: "IdCateNews",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppNews_AppCategoryNews_IdCateNews",
                table: "AppNews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCategoryNews",
                table: "AppCategoryNews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AppCategoryNews");

            migrationBuilder.RenameTable(
                name: "AppCategoryNews",
                newName: "AppcategoryNews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppcategoryNews",
                table: "AppcategoryNews",
                column: "IdCateNews");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$44MSIW2huVt39wPNeEuiv.ir7UbyyJcSREU897aeRbRy3Ka9cz1I2");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$44MSIW2huVt39wPNeEuiv.ir7UbyyJcSREU897aeRbRy3Ka9cz1I2");

            migrationBuilder.AddForeignKey(
                name: "FK_AppNews_AppcategoryNews_IdCateNews",
                table: "AppNews",
                column: "IdCateNews",
                principalTable: "AppcategoryNews",
                principalColumn: "IdCateNews",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
