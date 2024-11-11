using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SpecificTypeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accessory_specific_types_specific_types_specific_type_id",
                table: "accessory_specific_types");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_unit_specific_types_specific_types_specific_",
                table: "audio_equipment_unit_specific_types");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_specific_types_specific_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instrument_specific_types_specific_types_specific_ty",
                table: "musical_instrument_specific_types");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_specific_types_specific_type_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_edition_specific_types_specific_types_specific_t",
                table: "sheet_music_edition_specific_types");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_specific_types_specific_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropTable(
                name: "specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sheet_music_edition_specific_types",
                table: "sheet_music_edition_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_musical_instrument_specific_types",
                table: "musical_instrument_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_audio_equipment_unit_specific_types",
                table: "audio_equipment_unit_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accessory_specific_types",
                table: "accessory_specific_types");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "sheet_music_edition_specific_types",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "musical_instrument_specific_types",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "audio_equipment_unit_specific_types",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "accessory_specific_types",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sheet_music_edition_specific_types",
                table: "sheet_music_edition_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_musical_instrument_specific_types",
                table: "musical_instrument_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_audio_equipment_unit_specific_types",
                table: "audio_equipment_unit_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_accessory_specific_types",
                table: "accessory_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_units_audio_equipment_unit_specific_types_sp",
                table: "audio_equipment_units",
                column: "specific_type_id",
                principalTable: "audio_equipment_unit_specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instruments_musical_instrument_specific_types_specif",
                table: "musical_instruments",
                column: "specific_type_id",
                principalTable: "musical_instrument_specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_editions_sheet_music_edition_specific_types_spec",
                table: "sheet_music_editions",
                column: "specific_type_id",
                principalTable: "sheet_music_edition_specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_audio_equipment_unit_specific_types_sp",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_musical_instrument_specific_types_specif",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_sheet_music_edition_specific_types_spec",
                table: "sheet_music_editions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sheet_music_edition_specific_types",
                table: "sheet_music_edition_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_musical_instrument_specific_types",
                table: "musical_instrument_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_audio_equipment_unit_specific_types",
                table: "audio_equipment_unit_specific_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_accessory_specific_types",
                table: "accessory_specific_types");

            migrationBuilder.DropColumn(
                name: "name",
                table: "sheet_music_edition_specific_types");

            migrationBuilder.DropColumn(
                name: "name",
                table: "musical_instrument_specific_types");

            migrationBuilder.DropColumn(
                name: "name",
                table: "audio_equipment_unit_specific_types");

            migrationBuilder.DropColumn(
                name: "name",
                table: "accessory_specific_types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sheet_music_edition_specific_types",
                table: "sheet_music_edition_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_musical_instrument_specific_types",
                table: "musical_instrument_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_audio_equipment_unit_specific_types",
                table: "audio_equipment_unit_specific_types",
                column: "specific_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accessory_specific_types",
                table: "accessory_specific_types",
                column: "specific_type_id");

            migrationBuilder.CreateTable(
                name: "specific_types",
                columns: table => new
                {
                    specific_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specific_types", x => x.specific_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_accessory_specific_types_specific_types_specific_type_id",
                table: "accessory_specific_types",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_unit_specific_types_specific_types_specific_",
                table: "audio_equipment_unit_specific_types",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_units_specific_types_specific_type_id",
                table: "audio_equipment_units",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instrument_specific_types_specific_types_specific_ty",
                table: "musical_instrument_specific_types",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instruments_specific_types_specific_type_id",
                table: "musical_instruments",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_edition_specific_types_specific_types_specific_t",
                table: "sheet_music_edition_specific_types",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_editions_specific_types_specific_type_id",
                table: "sheet_music_editions",
                column: "specific_type_id",
                principalTable: "specific_types",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
