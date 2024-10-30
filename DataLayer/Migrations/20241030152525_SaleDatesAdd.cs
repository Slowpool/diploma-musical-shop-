using System;
using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleDatesAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "sales");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reservation_date",
                table: "sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "returning_date",
                table: "sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "sale_date",
                table: "sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.Sql($"{DropSalesView}{CreateSalesViewV3}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reservation_date",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "returning_date",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "sale_date",
                table: "sales");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
            migrationBuilder.Sql($"{DropSalesView}{CreateSalesViewV2}");
        }
    }
}
