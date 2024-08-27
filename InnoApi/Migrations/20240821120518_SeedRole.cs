using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b6bad22-ffa5-422d-b2e6-71c524621d06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8e7588d-c59a-4226-bb2f-ee98cdec8688");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26ceba16-d7e2-4692-8bf7-95f297f303ee", null, "Admin", "ADMIN" },
                    { "8698f59e-89b2-4072-bbb6-3eade3958a35", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26ceba16-d7e2-4692-8bf7-95f297f303ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8698f59e-89b2-4072-bbb6-3eade3958a35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b6bad22-ffa5-422d-b2e6-71c524621d06", null, "Admin", "ADMIN" },
                    { "f8e7588d-c59a-4226-bb2f-ee98cdec8688", null, "User", "USER" }
                });
        }
    }
}
