using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wsr.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_NoteId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "NoteId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_NoteId",
                table: "Reservations",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Notes_NoteId",
                table: "Reservations",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
