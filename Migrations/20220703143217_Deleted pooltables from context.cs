using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class Deletedpooltablesfromcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolTables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cost",
                table: "Cost");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc2dbbd7-f769-46b9-bb73-3bb31a03e673"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cebe3f55-e086-413b-a031-2ffdc3708530"));

            migrationBuilder.RenameTable(
                name: "Cost",
                newName: "Costs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costs",
                table: "Costs",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Costs",
                table: "Costs");

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

            migrationBuilder.RenameTable(
                name: "Costs",
                newName: "Cost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cost",
                table: "Cost",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PoolTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoolTables_Cost_CostId",
                        column: x => x.CostId,
                        principalTable: "Cost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PoolTables",
                columns: new[] { "Id", "CostId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("88820976-6bc6-40e2-b3af-583c56459ea1"), null, "Pierwszy od okna", "Stół 1" },
                    { new Guid("8d971194-dfa1-4288-97e8-21af2736a5da"), null, "Przy obrazie Johnego K Asteroida", "Stół 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "Name", "Salt" },
                values: new object[,]
                {
                    { new Guid("cebe3f55-e086-413b-a031-2ffdc3708530"), "6319515c904e91c9b5563aa82ffb248725630f741b58ea900f7ef088dbf4f703", true, "Zbyszek", "rY8wXiQK4kw+Mpcw" },
                    { new Guid("cc2dbbd7-f769-46b9-bb73-3bb31a03e673"), "1cb09975dfe03db330b6a4688b932727d2ffc4f55a9b4d7db933a728eec33a76", false, "Marcel", "p/Nta71H44LTcd8Z" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolTables_CostId",
                table: "PoolTables",
                column: "CostId");
        }
    }
}
