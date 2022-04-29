using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class GeneratedNewUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("536097a0-e2f2-4c0e-9e59-254185da7273"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cef78ceb-d75e-4653-8f77-a2cd14773f8d"));

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cookie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("91c1656a-a8c8-44ee-a240-c932ef949716"), "065169b685c379acbec4694db0ac9e4a56adb633b83704fd3870457295e160e2", true, "Zbyszek", "82lgUI2ME1Ebxk1u" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("88cdc4f6-28e6-4631-ba27-4f7b10ee0e2d"), "4a3b2ecba8a891d8ea46bc1aded65e12d0bf3bec0f416f7db227e1e439d4fc2d", false, "Marcel", "gS+8rsMoszFIjAeC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88cdc4f6-28e6-4631-ba27-4f7b10ee0e2d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("91c1656a-a8c8-44ee-a240-c932ef949716"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("cef78ceb-d75e-4653-8f77-a2cd14773f8d"), "a3c8a8a46f8c97f7686998978c001470cd257ea579a61e8d5af9ccc56829c4d6", true, "Zbyszek", "13ZK7MWPZRNJP/67" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("536097a0-e2f2-4c0e-9e59-254185da7273"), "f0aee99ae5d1a00725e7fad85320828ae7be00844eb071c322a839c47a73472b", false, "Marcel", "jZnBN2IPrczxdV6E" });
        }
    }
}
