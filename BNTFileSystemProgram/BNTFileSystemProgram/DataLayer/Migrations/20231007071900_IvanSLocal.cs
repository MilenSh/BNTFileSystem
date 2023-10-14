using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class IvanSLocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "887e5b21-6a5d-4dd3-a069-1ec895ba4297");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d250da13-1c78-4886-8a60-7e7a90eb66ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de33cd0c-a447-4bb9-9b58-0d38ce8b8d6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bcd4681-53f0-4b07-adf6-17d8aaeb1254", "34d9ad66-5ede-48ae-8f76-64433f4931b8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44f38030-f19b-41d5-a4c0-b60fd10f97da", "efabe0e4-238f-45dd-aa5d-be243b7dd67a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9373aea2-8ae1-4b11-b4d2-b86d5408e592", "67b8c1d7-3f1b-4724-932c-f9b3db77e2a4", "Editor", "EDITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bcd4681-53f0-4b07-adf6-17d8aaeb1254");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44f38030-f19b-41d5-a4c0-b60fd10f97da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9373aea2-8ae1-4b11-b4d2-b86d5408e592");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "887e5b21-6a5d-4dd3-a069-1ec895ba4297", "624527ed-f978-4dd9-a34f-58a92b522537", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d250da13-1c78-4886-8a60-7e7a90eb66ed", "82dd586d-39fe-48c4-a161-11c0bc91cbeb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de33cd0c-a447-4bb9-9b58-0d38ce8b8d6a", "026cd129-a5df-4f3e-a5e9-7968c3383ddb", "Editor", "EDITOR" });
        }
    }
}
