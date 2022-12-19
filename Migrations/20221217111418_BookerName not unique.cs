using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class BookerNamenotunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_BookerName",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "BookerName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookerName",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookerName",
                table: "Reservations",
                column: "BookerName",
                unique: true);
        }
    }
}
