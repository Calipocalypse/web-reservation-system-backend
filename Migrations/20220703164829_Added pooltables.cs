using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class Addedpooltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Costs",
                keyColumn: "Id",
                keyValue: new Guid("c3d18c8d-9ace-466d-a50a-a42f097e590f"));

            migrationBuilder.DeleteData(
                table: "Costs",
                keyColumn: "Id",
                keyValue: new Guid("e4764d18-8890-497c-93a1-cb14e1ce7ee6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0ceee57-9bfd-4104-b26a-c69205fa1909"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("df87e0bb-39ad-459b-be12-782ba43baed5"));

            migrationBuilder.CreateTable(
                name: "PoolTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoolTables_Costs_CostId",
                        column: x => x.CostId,
                        principalTable: "Costs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolTables_CostId",
                table: "PoolTables",
                column: "CostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolTables");

            migrationBuilder.InsertData(
                table: "Costs",
                columns: new[] { "Id", "CostValue", "Name" },
                values: new object[,]
                {
                    { new Guid("c3d18c8d-9ace-466d-a50a-a42f097e590f"), 99.99m, "Cena normalna" },
                    { new Guid("e4764d18-8890-497c-93a1-cb14e1ce7ee6"), 59.99m, "Cena rabatowa" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[,]
                {
                    { new Guid("df87e0bb-39ad-459b-be12-782ba43baed5"), "f5a1f65ca7592fb5aecdf8adf4bd1ad6d3e6f2cab6f66ee211803003f6ae4ab5", true, "Zbyszek", "DKX6KgajQggZRyRL" },
                    { new Guid("a0ceee57-9bfd-4104-b26a-c69205fa1909"), "d93ca31b626f62b95e0d833bdc1888e349a21f50de4eb35ff850f25fdcd7d304", false, "Marcel", "vY0zUSj2jc59dsQ+" }
                });
        }
    }
}
