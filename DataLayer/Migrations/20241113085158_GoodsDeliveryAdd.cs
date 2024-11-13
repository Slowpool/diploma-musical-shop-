using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class GoodsDeliveryAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "delivery_goods_delivery_id",
                table: "sheet_music_editions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "delivery_goods_delivery_id",
                table: "musical_instruments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "delivery_goods_delivery_id",
                table: "audio_equipment_units",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "delivery_goods_delivery_id",
                table: "accessories",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "goods_delivery",
                columns: table => new
                {
                    goods_delivery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    expected_delivery_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    actual_delivery_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_goods_delivery", x => x.goods_delivery_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_sheet_music_editions_delivery_goods_delivery_id",
                table: "sheet_music_editions",
                column: "delivery_goods_delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_musical_instruments_delivery_goods_delivery_id",
                table: "musical_instruments",
                column: "delivery_goods_delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_audio_equipment_units_delivery_goods_delivery_id",
                table: "audio_equipment_units",
                column: "delivery_goods_delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_accessories_delivery_goods_delivery_id",
                table: "accessories",
                column: "delivery_goods_delivery_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "goods_delivery");

            migrationBuilder.DropIndex(
                name: "ix_sheet_music_editions_delivery_goods_delivery_id",
                table: "sheet_music_editions");

            migrationBuilder.DropIndex(
                name: "ix_musical_instruments_delivery_goods_delivery_id",
                table: "musical_instruments");

            migrationBuilder.DropIndex(
                name: "ix_audio_equipment_units_delivery_goods_delivery_id",
                table: "audio_equipment_units");

            migrationBuilder.DropIndex(
                name: "ix_accessories_delivery_goods_delivery_id",
                table: "accessories");

            migrationBuilder.DropColumn(
                name: "delivery_goods_delivery_id",
                table: "sheet_music_editions");

            migrationBuilder.DropColumn(
                name: "delivery_goods_delivery_id",
                table: "musical_instruments");

            migrationBuilder.DropColumn(
                name: "delivery_goods_delivery_id",
                table: "audio_equipment_units");

            migrationBuilder.DropColumn(
                name: "delivery_goods_delivery_id",
                table: "accessories");
        }
    }
}
