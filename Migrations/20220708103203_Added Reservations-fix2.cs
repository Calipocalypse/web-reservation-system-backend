using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class AddedReservationsfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PoolTables_PoolTableId",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "PoolTableId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NoteId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PoolTables_PoolTableId",
                table: "Reservations",
                column: "PoolTableId",
                principalTable: "PoolTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PoolTables_PoolTableId",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "PoolTableId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "NoteId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PoolTables_PoolTableId",
                table: "Reservations",
                column: "PoolTableId",
                principalTable: "PoolTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
