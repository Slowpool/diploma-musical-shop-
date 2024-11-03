using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleViewCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SheetMusicEditionSale",
                table: "SheetMusicEditionSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicalInstrumentSale",
                table: "MusicalInstrumentSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudioEquipmentUnitSale",
                table: "AudioEquipmentUnitSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessorySale",
                table: "AccessorySale");

            migrationBuilder.RenameTable(
                name: "SheetMusicEditionSale",
                newName: "sheet_music_edition_sale");

            migrationBuilder.RenameTable(
                name: "MusicalInstrumentSale",
                newName: "musical_instrumentSale_sale");

            migrationBuilder.RenameTable(
                name: "AudioEquipmentUnitSale",
                newName: "audio_equipment_unit_sale");

            migrationBuilder.RenameTable(
                name: "AccessorySale",
                newName: "accessory_sale");

            migrationBuilder.RenameIndex(
                name: "IX_SheetMusicEditionSale_SheetMusicEditionId",
                table: "sheet_music_edition_sale",
                newName: "IX_sheet_music_edition_sale_SheetMusicEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicalInstrumentSale_MusicalInstrumentId",
                table: "musical_instrumentSale_sale",
                newName: "IX_musical_instrumentSale_sale_MusicalInstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitId",
                table: "audio_equipment_unit_sale",
                newName: "IX_audio_equipment_unit_sale_AudioEquipmentUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessorySale_AccessoryId",
                table: "accessory_sale",
                newName: "IX_accessory_sale_AccessoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sheet_music_edition_sale",
                table: "sheet_music_edition_sale",
                columns: new[] { "SaleId", "SheetMusicEditionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_musical_instrumentSale_sale",
                table: "musical_instrumentSale_sale",
                columns: new[] { "SaleId", "MusicalInstrumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_audio_equipment_unit_sale",
                table: "audio_equipment_unit_sale",
                columns: new[] { "SaleId", "AudioEquipmentUnitId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_accessory_sale",
                table: "accessory_sale",
                columns: new[] { "SaleId", "AccessoryId" });

            migrationBuilder.DropTable("sales_view");
            migrationBuilder.Sql($"{CreateTotalPriceV1}{CreateTotalGoodsUnitsCountV1}{CreateSalesViewV1}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sheet_music_edition_sale",
                table: "sheet_music_edition_sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_musical_instrumentSale_sale",
                table: "musical_instrumentSale_sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_audio_equipment_unit_sale",
                table: "audio_equipment_unit_sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accessory_sale",
                table: "accessory_sale");

            migrationBuilder.RenameTable(
                name: "sheet_music_edition_sale",
                newName: "SheetMusicEditionSale");

            migrationBuilder.RenameTable(
                name: "musical_instrumentSale_sale",
                newName: "MusicalInstrumentSale");

            migrationBuilder.RenameTable(
                name: "audio_equipment_unit_sale",
                newName: "AudioEquipmentUnitSale");

            migrationBuilder.RenameTable(
                name: "accessory_sale",
                newName: "AccessorySale");

            migrationBuilder.RenameIndex(
                name: "IX_sheet_music_edition_sale_SheetMusicEditionId",
                table: "SheetMusicEditionSale",
                newName: "IX_SheetMusicEditionSale_SheetMusicEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_musical_instrumentSale_sale_MusicalInstrumentId",
                table: "MusicalInstrumentSale",
                newName: "IX_MusicalInstrumentSale_MusicalInstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_audio_equipment_unit_sale_AudioEquipmentUnitId",
                table: "AudioEquipmentUnitSale",
                newName: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_accessory_sale_AccessoryId",
                table: "AccessorySale",
                newName: "IX_AccessorySale_AccessoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SheetMusicEditionSale",
                table: "SheetMusicEditionSale",
                columns: new[] { "SaleId", "SheetMusicEditionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicalInstrumentSale",
                table: "MusicalInstrumentSale",
                columns: new[] { "SaleId", "MusicalInstrumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudioEquipmentUnitSale",
                table: "AudioEquipmentUnitSale",
                columns: new[] { "SaleId", "AudioEquipmentUnitId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessorySale",
                table: "AccessorySale",
                columns: new[] { "SaleId", "AccessoryId" });
        }
    }
}
