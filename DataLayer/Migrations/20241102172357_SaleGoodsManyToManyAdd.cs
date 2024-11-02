using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleGoodsManyToManyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessorySale_accessories_AccessoryGoodsId",
                table: "AccessorySale");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicalInstrumentSale_musical_instruments_MusicalInstrumentG~",
                table: "MusicalInstrumentSale");

            migrationBuilder.RenameColumn(
                name: "SheetMusicEditionGoodsId",
                table: "SheetMusicEditionSale",
                newName: "SheetMusicEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_SheetMusicEditionSale_SheetMusicEditionGoodsId",
                table: "SheetMusicEditionSale",
                newName: "IX_SheetMusicEditionSale_SheetMusicEditionId");

            migrationBuilder.RenameColumn(
                name: "MusicalInstrumentGoodsId",
                table: "MusicalInstrumentSale",
                newName: "MusicalInstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicalInstrumentSale_MusicalInstrumentGoodsId",
                table: "MusicalInstrumentSale",
                newName: "IX_MusicalInstrumentSale_MusicalInstrumentId");

            migrationBuilder.RenameColumn(
                name: "AudioEquipmentUnitGoodsId",
                table: "AudioEquipmentUnitSale",
                newName: "AudioEquipmentUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitGoodsId",
                table: "AudioEquipmentUnitSale",
                newName: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitId");

            migrationBuilder.RenameColumn(
                name: "AccessoryGoodsId",
                table: "AccessorySale",
                newName: "AccessoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessorySale_accessories_AccessoryId",
                table: "AccessorySale",
                column: "AccessoryId",
                principalTable: "accessories",
                principalColumn: "goods_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicalInstrumentSale_musical_instruments_MusicalInstrumentId",
                table: "MusicalInstrumentSale",
                column: "MusicalInstrumentId",
                principalTable: "musical_instruments",
                principalColumn: "goods_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessorySale_accessories_AccessoryId",
                table: "AccessorySale");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicalInstrumentSale_musical_instruments_MusicalInstrumentId",
                table: "MusicalInstrumentSale");

            migrationBuilder.RenameColumn(
                name: "SheetMusicEditionId",
                table: "SheetMusicEditionSale",
                newName: "SheetMusicEditionGoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_SheetMusicEditionSale_SheetMusicEditionId",
                table: "SheetMusicEditionSale",
                newName: "IX_SheetMusicEditionSale_SheetMusicEditionGoodsId");

            migrationBuilder.RenameColumn(
                name: "MusicalInstrumentId",
                table: "MusicalInstrumentSale",
                newName: "MusicalInstrumentGoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicalInstrumentSale_MusicalInstrumentId",
                table: "MusicalInstrumentSale",
                newName: "IX_MusicalInstrumentSale_MusicalInstrumentGoodsId");

            migrationBuilder.RenameColumn(
                name: "AudioEquipmentUnitId",
                table: "AudioEquipmentUnitSale",
                newName: "AudioEquipmentUnitGoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitId",
                table: "AudioEquipmentUnitSale",
                newName: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitGoodsId");

            migrationBuilder.RenameColumn(
                name: "AccessoryId",
                table: "AccessorySale",
                newName: "AccessoryGoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessorySale_accessories_AccessoryGoodsId",
                table: "AccessorySale",
                column: "AccessoryGoodsId",
                principalTable: "accessories",
                principalColumn: "goods_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicalInstrumentSale_musical_instruments_MusicalInstrumentG~",
                table: "MusicalInstrumentSale",
                column: "MusicalInstrumentGoodsId",
                principalTable: "musical_instruments",
                principalColumn: "goods_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
