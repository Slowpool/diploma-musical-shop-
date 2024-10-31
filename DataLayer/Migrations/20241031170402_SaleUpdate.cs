using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleUpdate : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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
            migrationBuilder.Sql($"{DropSalesView}{CreateSalesViewV4}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "sales");

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_sales_sale_id",
                table: "accessories",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id");

            migrationBuilder.AddForeignKey(
                name: "FK_audio_equipment_units_sales_sale_id",
                table: "audio_equipment_units",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id");

            migrationBuilder.AddForeignKey(
                name: "FK_musical_instruments_sales_sale_id",
                table: "musical_instruments",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sheet_music_editions_sales_sale_id",
                table: "sheet_music_editions",
                column: "sale_id",
                principalTable: "sales",
                principalColumn: "sale_id");
            migrationBuilder.Sql($"{DropSalesView}{CreateSalesViewV3}");
        }
    }
}
