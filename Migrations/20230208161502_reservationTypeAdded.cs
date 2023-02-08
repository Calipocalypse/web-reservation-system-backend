using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class reservationTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationType",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationType",
                table: "Reservations");
        }
    }
}
