using System;
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
                name: "FK_accessories_sales_sale_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_audio_equipment_units_sales_sale_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "FK_musical_instruments_sales_sale_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_sheet_music_editions_sales_sale_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "IX_sheet_music_editions_sale_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "IX_musical_instruments_sale_id",
                table: "musical_instruments");

            migrationBuilder.DropIndex(
                name: "IX_audio_equipment_units_sale_id",
                table: "audio_equipment_units");

            migrationBuilder.DropIndex(
                name: "IX_accessories_sale_id",
                table: "accessories");

            migrationBuilder.DropColumn(
                name: "sale_id",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "sale_id",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "sale_id",
                table: "audio_equipment_units");

            migrationBuilder.DropColumn(
                name: "sale_id",
                table: "accessories");

            migrationBuilder.CreateTable(
                name: "AccessorySale",
                columns: table => new
                {
                    AccessoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SaleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessorySale", x => new { x.AccessoryId, x.SaleId });
                    table.ForeignKey(
                        name: "FK_sale_accessory_id",
                        column: x => x.AccessoryId,
                        principalTable: "accessories",
                        principalColumn: "goods_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_accessory_sale_id",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AudioEquipmentUnitSale",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AudioEquipmentUnitId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioEquipmentUnitSale", x => new { x.AudioEquipmentUnitId, x.SaleId });
                    table.ForeignKey(
                        name: "FK_sale_aeu_id",
                        column: x => x.AudioEquipmentUnitId,
                        principalTable: "audio_equipment_units",
                        principalColumn: "goods_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_aeu_sale_id",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusicalInstrumentSale",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MusicalInstrumentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalInstrumentSale", x => new { x.MusicalInstrumentId, x.SaleId });
                    table.ForeignKey(
                        name: "FK_sale_musical_instrument_id",
                        column: x => x.MusicalInstrumentId,
                        principalTable: "musical_instruments",
                        principalColumn: "goods_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_musical_instrument_sale_id",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SheetMusicEditionSale",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SheetMusicEditionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetMusicEditionSale", x => new { x.SaleId, x.SheetMusicEditionId });
                    table.ForeignKey(
                        name: "FK_sale_sme_id",
                        column: x => x.SheetMusicEditionId,
                        principalTable: "sheet_music_editions",
                        principalColumn: "goods_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_sme_sale_id",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AccessorySale_SaleId",
                table: "AccessorySale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioEquipmentUnitSale_AudioEquipmentUnitId",
                table: "AudioEquipmentUnitSale",
                column: "AudioEquipmentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioEquipmentUnitSale_SaleId",
                table: "AudioEquipmentUnitSale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicalInstrumentSale_MusicalInstrumentId",
                table: "MusicalInstrumentSale",
                column: "MusicalInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicalInstrumentSale_SaleId",
                table: "MusicalInstrumentSale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetMusicEditionSale_SheetMusicEditionId",
                table: "SheetMusicEditionSale",
                column: "SheetMusicEditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessorySale");

            migrationBuilder.DropTable(
                name: "AudioEquipmentUnitSale");

            migrationBuilder.DropTable(
                name: "MusicalInstrumentSale");

            migrationBuilder.DropTable(
                name: "SheetMusicEditionSale");

            migrationBuilder.AddColumn<Guid>(
                name: "sale_id",
                table: "sheet_music_editions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "sale_id",
                table: "musical_instruments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "sale_id",
                table: "audio_equipment_units",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "sale_id",
                table: "accessories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sheet_music_editions_sale_id",
                table: "sheet_music_editions",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_musical_instruments_sale_id",
                table: "musical_instruments",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_audio_equipment_units_sale_id",
                table: "audio_equipment_units",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_accessories_sale_id",
                table: "accessories",
                column: "sale_id");

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_sales_sale_id",
                table: "accessories",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_audio_equipment_units_sales_sale_id",
                table: "audio_equipment_units",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_musical_instruments_sales_sale_id",
                table: "musical_instruments",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_sheet_music_editions_sales_sale_id",
                table: "sheet_music_editions",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
