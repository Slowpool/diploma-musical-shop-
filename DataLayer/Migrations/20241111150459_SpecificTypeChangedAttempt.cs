using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SpecificTypeChangedAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accessories_specific_type_type_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_specific_type_type_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "ix_sheet_music_editions_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "ix_musical_instruments_type_id",
                table: "musical_instruments");

            migrationBuilder.DropIndex(
                name: "ix_audio_equipment_units_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropIndex(
                name: "ix_accessories_type_id",
                table: "accessories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_specific_type",
                table: "specific_type");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "accessories");

            migrationBuilder.RenameTable(
                name: "specific_type",
                newName: "specific_types");

            migrationBuilder.RenameIndex(
                name: "ix_specific_type_name",
                table: "specific_types",
                newName: "ix_specific_types_name");

            migrationBuilder.AddColumn<Guid>(
                name: "specific_type_id",
                table: "sheet_music_editions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "specific_type_id",
                table: "musical_instruments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "specific_type_id",
                table: "audio_equipment_units",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "specific_type_id",
                table: "accessories",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "specific_type_id",
                table: "specific_types",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_specific_types",
                table: "specific_types",
                column: "specific_type_id");

            migrationBuilder.CreateTable(
                name: "accessory_specific_types",
                columns: table => new
                {
                    specific_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessory_specific_types", x => x.specific_type_id);
                    table.ForeignKey(
                        name: "fk_accessory_specific_types_specific_types_specific_type_id",
                        column: x => x.specific_type_id,
                        principalTable: "specific_types",
                        principalColumn: "specific_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "audio_equipment_unit_specific_types",
                columns: table => new
                {
                    specific_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audio_equipment_unit_specific_types", x => x.specific_type_id);
                    table.ForeignKey(
                        name: "fk_audio_equipment_unit_specific_types_specific_types_specific_",
                        column: x => x.specific_type_id,
                        principalTable: "specific_types",
                        principalColumn: "specific_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "musical_instrument_specific_types",
                columns: table => new
                {
                    specific_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musical_instrument_specific_types", x => x.specific_type_id);
                    table.ForeignKey(
                        name: "fk_musical_instrument_specific_types_specific_types_specific_ty",
                        column: x => x.specific_type_id,
                        principalTable: "specific_types",
                        principalColumn: "specific_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sheet_music_edition_specific_types",
                columns: table => new
                {
                    specific_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheet_music_edition_specific_types", x => x.specific_type_id);
                    table.ForeignKey(
                        name: "fk_sheet_music_edition_specific_types_specific_types_specific_t",
                        column: x => x.specific_type_id,
                        principalTable: "specific_types",
                        principalColumn: "specific_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_sheet_music_editions_specific_type_id",
                table: "sheet_music_editions",
                column: "specific_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_musical_instruments_specific_type_id",
                table: "musical_instruments",
                column: "specific_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_audio_equipment_units_specific_type_id",
                table: "audio_equipment_units",
                column: "specific_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_accessories_specific_type_id",
                table: "accessories",
                column: "specific_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accessories_accessory_specific_types_specific_type_id",
                table: "accessories",
                column: "specific_type_id",
                principalTable: "accessory_specific_types",
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
                name: "fk_musical_instruments_specific_types_specific_type_id",
                table: "musical_instruments",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accessories_accessory_specific_types_specific_type_id",
                table: "accessories");

            migrationBuilder.DropForeignKey(
                name: "fk_audio_equipment_units_specific_types_specific_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropForeignKey(
                name: "fk_musical_instruments_specific_types_specific_type_id",
                table: "musical_instruments");

            migrationBuilder.DropForeignKey(
                name: "fk_sheet_music_editions_specific_types_specific_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropTable(
                name: "accessory_specific_types");

            migrationBuilder.DropTable(
                name: "audio_equipment_unit_specific_types");

            migrationBuilder.DropTable(
                name: "musical_instrument_specific_types");

            migrationBuilder.DropTable(
                name: "sheet_music_edition_specific_types");

            migrationBuilder.DropIndex(
                name: "ix_sheet_music_editions_specific_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "ix_musical_instruments_specific_type_id",
                table: "musical_instruments");

            migrationBuilder.DropIndex(
                name: "ix_audio_equipment_units_specific_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropIndex(
                name: "ix_accessories_specific_type_id",
                table: "accessories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_specific_types",
                table: "specific_types");

            migrationBuilder.DropColumn(
                name: "specific_type_id",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "specific_type_id",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "specific_type_id",
                table: "audio_equipment_units");

            migrationBuilder.DropColumn(
                name: "specific_type_id",
                table: "accessories");

            migrationBuilder.RenameTable(
                name: "specific_types",
                newName: "specific_type");

            migrationBuilder.RenameIndex(
                name: "ix_specific_types_name",
                table: "specific_type",
                newName: "ix_specific_type_name");

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "sheet_music_editions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "musical_instruments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "audio_equipment_units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "accessories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "specific_type_id",
                table: "specific_type",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "pk_specific_type",
                table: "specific_type",
                column: "specific_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_sheet_music_editions_type_id",
                table: "sheet_music_editions",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_musical_instruments_type_id",
                table: "musical_instruments",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_audio_equipment_units_type_id",
                table: "audio_equipment_units",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_accessories_type_id",
                table: "accessories",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accessories_specific_type_type_id",
                table: "accessories",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_audio_equipment_units_specific_type_type_id",
                table: "audio_equipment_units",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_musical_instruments_specific_type_type_id",
                table: "musical_instruments",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sheet_music_editions_specific_type_type_id",
                table: "sheet_music_editions",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
