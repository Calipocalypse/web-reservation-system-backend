using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class AddedSaltColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("510de3e6-76d8-4809-b165-a34043d4cddc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80563b60-d587-4fac-86ee-df1c0c3dfc2c"));

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("cef78ceb-d75e-4653-8f77-a2cd14773f8d"), "a3c8a8a46f8c97f7686998978c001470cd257ea579a61e8d5af9ccc56829c4d6", true, "Zbyszek", "13ZK7MWPZRNJP/67" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("536097a0-e2f2-4c0e-9e59-254185da7273"), "f0aee99ae5d1a00725e7fad85320828ae7be00844eb071c322a839c47a73472b", false, "Marcel", "jZnBN2IPrczxdV6E" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("536097a0-e2f2-4c0e-9e59-254185da7273"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cef78ceb-d75e-4653-8f77-a2cd14773f8d"));

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name" },
                values: new object[] { new Guid("80563b60-d587-4fac-86ee-df1c0c3dfc2c"), "p2i35nhjp1ip", true, "Zbyszek" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name" },
                values: new object[] { new Guid("510de3e6-76d8-4809-b165-a34043d4cddc"), "p2i35nhjp1ip", false, "Marcel" });
        }
    }
}
