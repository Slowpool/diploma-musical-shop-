using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class v051 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "receipt_date",
                table: "sheet_music_editions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "receipt_date",
                table: "musical_instruments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "receipt_date",
                table: "audio_equipment_units",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "receipt_date",
                table: "accessories",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receipt_date",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "receipt_date",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "receipt_date",
                table: "audio_equipment_units");

            migrationBuilder.DropColumn(
                name: "receipt_date",
                table: "accessories");
        }
    }
}
