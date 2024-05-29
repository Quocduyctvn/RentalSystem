using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddtableCateNewsAndNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppcategoryNews",
                columns: table => new
                {
                    IdCateNews = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppcategoryNews", x => x.IdCateNews);
                });

            migrationBuilder.CreateTable(
                name: "AppNews",
                columns: table => new
                {
                    IdNews = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CoverImg = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCateNews = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNews", x => x.IdNews);
                    table.ForeignKey(
                        name: "FK_AppNews_AppUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUser",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppNews_AppcategoryNews_IdCateNews",
                        column: x => x.IdCateNews,
                        principalTable: "AppcategoryNews",
                        principalColumn: "IdCateNews",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405261", "$2a$10$44MSIW2huVt39wPNeEuiv.ir7UbyyJcSREU897aeRbRy3Ka9cz1I2" });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405262", "$2a$10$44MSIW2huVt39wPNeEuiv.ir7UbyyJcSREU897aeRbRy3Ka9cz1I2" });

            migrationBuilder.CreateIndex(
                name: "IX_AppNews_IdCateNews",
                table: "AppNews",
                column: "IdCateNews");

            migrationBuilder.CreateIndex(
                name: "IX_AppNews_IdUser",
                table: "AppNews",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppNews");

            migrationBuilder.DropTable(
                name: "AppcategoryNews");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 1,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405171", "$2a$10$4OzTt/JLv2uNdwFR.WhCk.ApyPTCG3TlTHPpSJw/tEPZYdiU0e7/K" });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "IdUser",
                keyValue: 2,
                columns: new[] { "Code", "Password" },
                values: new object[] { "US2405172", "$2a$10$4OzTt/JLv2uNdwFR.WhCk.ApyPTCG3TlTHPpSJw/tEPZYdiU0e7/K" });
        }
    }
}
