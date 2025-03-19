using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class reservation_extra_info_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "reservation_extra_info_id",
                table: "sales",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    reservation_extra_info_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    secret_word = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    soft_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.reservation_extra_info_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_sales_reservation_extra_info_id",
                table: "sales",
                column: "reservation_extra_info_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_sales_reservations_reservation_extra_info_id",
                table: "sales",
                column: "reservation_extra_info_id",
                principalTable: "reservations",
                principalColumn: "reservation_extra_info_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sales_reservations_reservation_extra_info_id",
                table: "sales");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropIndex(
                name: "ix_sales_reservation_extra_info_id",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "reservation_extra_info_id",
                table: "sales");
        }
    }
}
