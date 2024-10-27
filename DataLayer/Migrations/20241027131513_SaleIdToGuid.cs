using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE `accessories` DROP FOREIGN KEY `FK_accessories_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `musical_instruments` DROP FOREIGN KEY `FK_musical_instruments_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `sheet_music_editions` DROP FOREIGN KEY `FK_sheet_music_editions_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `audio_equipment_units` DROP FOREIGN KEY `FK_audio_equipment_units_sales_sale_id`");
            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "sheet_music_editions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "musical_instruments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "audio_equipment_units",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "accessories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "sales",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            migrationBuilder.Sql(@"ALTER TABLE `accessories` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `musical_instruments` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `sheet_music_editions` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `audio_equipment_units` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"SET foreign_key_checks = 0;"); // doesn't work
            migrationBuilder.Sql(@"ALTER TABLE `accessories` DROP FOREIGN KEY `FK_accessories_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `musical_instruments` DROP FOREIGN KEY `FK_musical_instruments_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `sheet_music_editions` DROP FOREIGN KEY `FK_sheet_music_editions_sales_sale_id`");
            migrationBuilder.Sql(@"ALTER TABLE `audio_equipment_units` DROP FOREIGN KEY `FK_audio_equipment_units_sales_sale_id`");
            migrationBuilder.AlterColumn<int>(
                name: "sale_id",
                table: "sheet_music_editions",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "sale_id",
                table: "musical_instruments",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "sale_id",
                table: "audio_equipment_units",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "sale_id",
                table: "accessories",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
            migrationBuilder.AlterColumn<int>(
                name: "sale_id",
                table: "sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
            migrationBuilder.Sql(@"ALTER TABLE `accessories` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `musical_instruments` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `sheet_music_editions` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            migrationBuilder.Sql(@"ALTER TABLE `audio_equipment_units` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);");
            //migrationBuilder.Sql(@"SET foreign_key_checks = 1;"); // doesn't work
        }
    }
}
