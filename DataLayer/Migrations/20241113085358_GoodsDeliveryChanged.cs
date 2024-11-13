using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class GoodsDeliveryChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accessories_goods_delivery_delivery_goods_delivery_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_goods_delivery_delivery_goods_delivery",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_goods_delivery_delivery_goods_delivery_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_goods_delivery_delivery_goods_delivery_",
                table: "sheet_music_editions");

            migrationBuilder.RenameColumn(
                name: "delivery_goods_delivery_id",
                table: "sheet_music_editions",
                newName: "delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_sheet_music_editions_delivery_goods_delivery_id",
                table: "sheet_music_editions",
                newName: "ix_sheet_music_editions_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_goods_delivery_id",
                table: "musical_instruments",
                newName: "delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_musical_instruments_delivery_goods_delivery_id",
                table: "musical_instruments",
                newName: "ix_musical_instruments_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_goods_delivery_id",
                table: "audio_equipment_units",
                newName: "delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_audio_equipment_units_delivery_goods_delivery_id",
                table: "audio_equipment_units",
                newName: "ix_audio_equipment_units_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_goods_delivery_id",
                table: "accessories",
                newName: "delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_accessories_delivery_goods_delivery_id",
                table: "accessories",
                newName: "ix_accessories_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accessories_goods_delivery_delivery_id",
                table: "accessories",
                column: "delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_units_goods_delivery_delivery_id",
                table: "audio_equipment_units",
                column: "delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instruments_goods_delivery_delivery_id",
                table: "musical_instruments",
                column: "delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_editions_goods_delivery_delivery_id",
                table: "sheet_music_editions",
                column: "delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accessories_goods_delivery_delivery_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_goods_delivery_delivery_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_goods_delivery_delivery_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_goods_delivery_delivery_id",
                table: "sheet_music_editions");

            migrationBuilder.RenameColumn(
                name: "delivery_id",
                table: "sheet_music_editions",
                newName: "delivery_goods_delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_sheet_music_editions_delivery_id",
                table: "sheet_music_editions",
                newName: "ix_sheet_music_editions_delivery_goods_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_id",
                table: "musical_instruments",
                newName: "delivery_goods_delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_musical_instruments_delivery_id",
                table: "musical_instruments",
                newName: "ix_musical_instruments_delivery_goods_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_id",
                table: "audio_equipment_units",
                newName: "delivery_goods_delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_audio_equipment_units_delivery_id",
                table: "audio_equipment_units",
                newName: "ix_audio_equipment_units_delivery_goods_delivery_id");

            migrationBuilder.RenameColumn(
                name: "delivery_id",
                table: "accessories",
                newName: "delivery_goods_delivery_id");

            migrationBuilder.RenameIndex(
                name: "ix_accessories_delivery_id",
                table: "accessories",
                newName: "ix_accessories_delivery_goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accessories_goods_delivery_delivery_goods_delivery_id",
                table: "accessories",
                column: "delivery_goods_delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_units_goods_delivery_delivery_goods_delivery",
                table: "audio_equipment_units",
                column: "delivery_goods_delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instruments_goods_delivery_delivery_goods_delivery_id",
                table: "musical_instruments",
                column: "delivery_goods_delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_editions_goods_delivery_delivery_goods_delivery_",
                table: "sheet_music_editions",
                column: "delivery_goods_delivery_id",
                principalTable: "goods_delivery",
                principalColumn: "goods_delivery_id");
        }
    }
}
