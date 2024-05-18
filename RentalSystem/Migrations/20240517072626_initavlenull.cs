using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class initavlenull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppContactFeedback_IdRequest",
                table: "AppContactFeedback");

            migrationBuilder.AlterColumn<int>(
                name: "IdRequest",
                table: "AppContactFeedback",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$4OzTt/JLv2uNdwFR.WhCk.ApyPTCG3TlTHPpSJw/tEPZYdiU0e7/K");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$4OzTt/JLv2uNdwFR.WhCk.ApyPTCG3TlTHPpSJw/tEPZYdiU0e7/K");

            migrationBuilder.CreateIndex(
                name: "IX_AppContactFeedback_IdRequest",
                table: "AppContactFeedback",
                column: "IdRequest",
                unique: true,
                filter: "[IdRequest] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppContactFeedback_IdRequest",
                table: "AppContactFeedback");

            migrationBuilder.AlterColumn<int>(
                name: "IdRequest",
                table: "AppContactFeedback",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$9mZ6KmmnlVcjfAgXA5.AZeOUuKgXDJjJWMlBDBKTD8PDbDP4epKju");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$9mZ6KmmnlVcjfAgXA5.AZeOUuKgXDJjJWMlBDBKTD8PDbDP4epKju");

            migrationBuilder.CreateIndex(
                name: "IX_AppContactFeedback_IdRequest",
                table: "AppContactFeedback",
                column: "IdRequest",
                unique: true);
        }
    }
}
