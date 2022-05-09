using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class AddedPoolTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88cdc4f6-28e6-4631-ba27-4f7b10ee0e2d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("91c1656a-a8c8-44ee-a240-c932ef949716"));

            migrationBuilder.CreateTable(
                name: "PoolTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolTables", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PoolTables",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6a6b9908-a634-4ee4-b709-6a1d11d9c617"), "Stół 1" },
                    { new Guid("31bccfbe-1b8f-42a8-abf9-9507f3e9e95c"), "Stół 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[,]
                {
                    { new Guid("9a2569be-2e85-4a74-83cc-617488aaef13"), "eb6074157928e8247f4961d2986755af56be13d471c505f78aad24d69d0776ac", true, "Zbyszek", "nJubHNpBjFzAGl0Y" },
                    { new Guid("0e6f3ffd-d62d-428f-b73a-9784d27af0ba"), "1a7ca80aa0ea0078155976de82b382a437b1e604267bfb23ffc8ba85743a6d22", false, "Marcel", "stX0BAQMrBQ+gOkf" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolTables");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0e6f3ffd-d62d-428f-b73a-9784d27af0ba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9a2569be-2e85-4a74-83cc-617488aaef13"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("91c1656a-a8c8-44ee-a240-c932ef949716"), "065169b685c379acbec4694db0ac9e4a56adb633b83704fd3870457295e160e2", true, "Zbyszek", "82lgUI2ME1Ebxk1u" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[] { new Guid("88cdc4f6-28e6-4631-ba27-4f7b10ee0e2d"), "4a3b2ecba8a891d8ea46bc1aded65e12d0bf3bec0f416f7db227e1e439d4fc2d", false, "Marcel", "gS+8rsMoszFIjAeC" });
        }
    }
}
