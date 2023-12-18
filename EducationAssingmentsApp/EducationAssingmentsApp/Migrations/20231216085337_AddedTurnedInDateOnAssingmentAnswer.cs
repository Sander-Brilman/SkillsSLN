using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationAssingmentsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedTurnedInDateOnAssingmentAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c43c440-63c6-49d6-9ef6-cec4a07f79e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83cf7fd6-a2dc-4090-9512-6ac3053d80a7");

            migrationBuilder.AddColumn<DateTime>(
                name: "TurnedInAt",
                table: "AssingmentsAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b075aecb-766c-42f3-a13b-76716bedec29", null, "Admin", "ADMIN" },
                    { "e6c0aa49-4cdc-4dcb-9cdc-ac38bb10df67", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b075aecb-766c-42f3-a13b-76716bedec29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6c0aa49-4cdc-4dcb-9cdc-ac38bb10df67");

            migrationBuilder.DropColumn(
                name: "TurnedInAt",
                table: "AssingmentsAnswers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c43c440-63c6-49d6-9ef6-cec4a07f79e8", null, "Admin", "ADMIN" },
                    { "83cf7fd6-a2dc-4090-9512-6ac3053d80a7", null, "Student", "STUDENT" }
                });
        }
    }
}
