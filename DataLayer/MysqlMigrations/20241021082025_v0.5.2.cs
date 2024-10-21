using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class v052 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sheet_music_edition_id",
                table: "sheet_music_editions",
                newName: "goods_id");

            migrationBuilder.RenameColumn(
                name: "musical_instrument_id",
                table: "musical_instruments",
                newName: "goods_id");

            migrationBuilder.RenameColumn(
                name: "audio_equipment_unit_id",
                table: "audio_equipment_units",
                newName: "goods_id");

            migrationBuilder.RenameColumn(
                name: "accessory_id",
                table: "accessories",
                newName: "goods_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "goods_id",
                table: "sheet_music_editions",
                newName: "sheet_music_edition_id");

            migrationBuilder.RenameColumn(
                name: "goods_id",
                table: "musical_instruments",
                newName: "musical_instrument_id");

            migrationBuilder.RenameColumn(
                name: "goods_id",
                table: "audio_equipment_units",
                newName: "audio_equipment_unit_id");

            migrationBuilder.RenameColumn(
                name: "goods_id",
                table: "accessories",
                newName: "accessory_id");
        }
    }
}
