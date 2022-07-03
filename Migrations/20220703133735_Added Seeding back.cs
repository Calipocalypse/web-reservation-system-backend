using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class AddedSeedingback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PoolTables",
                keyColumn: "Id",
                keyValue: new Guid("88820976-6bc6-40e2-b3af-583c56459ea1"));

            migrationBuilder.DeleteData(
                table: "PoolTables",
                keyColumn: "Id",
                keyValue: new Guid("8d971194-dfa1-4288-97e8-21af2736a5da"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc2dbbd7-f769-46b9-bb73-3bb31a03e673"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cebe3f55-e086-413b-a031-2ffdc3708530"));
        }
    }
}
