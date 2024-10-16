using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class v05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "FK_musical_instruments_specific_type_type_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "specific_type",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "sheet_music_editions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "sheet_music_edition_id",
                table: "sheet_music_editions",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "sheet_music_editions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "release_year",
                table: "sheet_music_editions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "musical_instruments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "musical_instrument_id",
                table: "musical_instruments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "musical_instruments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "manufacturer_type",
                table: "musical_instruments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "audio_equipment_units",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "audio_equipment_unit_id",
                table: "audio_equipment_units",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "accessories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "accessory_id",
                table: "accessories",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "accessories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "accessories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_musical_instruments_specific_type_type_id",
                table: "musical_instruments",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "FK_musical_instruments_specific_type_type_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "release_year",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "manufacturer_type",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "accessories");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "accessories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "specific_type",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "sheet_music_editions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "sheet_music_edition_id",
                table: "sheet_music_editions",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "musical_instruments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "musical_instrument_id",
                table: "musical_instruments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "audio_equipment_units",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "audio_equipment_unit_id",
                table: "audio_equipment_units",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "accessories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "accessory_id",
                table: "accessories",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_musical_instruments_specific_type_type_id",
                table: "musical_instruments",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id");
        }
    }
}
