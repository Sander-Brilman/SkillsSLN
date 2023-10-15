using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WieEetErMee.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DefaultPresentOnMonday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnTuesday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnWednesday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnThursday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnFriday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnSaturday = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPresentOnSunday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserPresense",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPresense", x => new { x.DayId, x.UserName });
                    table.ForeignKey(
                        name: "FK_UserPresense_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPresense_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Name", "DefaultPresentOnFriday", "DefaultPresentOnMonday", "DefaultPresentOnSaturday", "DefaultPresentOnSunday", "DefaultPresentOnThursday", "DefaultPresentOnTuesday", "DefaultPresentOnWednesday" },
                values: new object[,]
                {
                    { "Jasper", true, false, true, true, false, false, false },
                    { "Sander", true, true, true, true, true, true, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPresense_UserName",
                table: "UserPresense",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPresense");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
