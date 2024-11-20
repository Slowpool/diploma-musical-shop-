using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;
#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SalesPaidByNullabilityAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "paid_by",
                table: "sales",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.Sql($"{SalesUpdateTriggerCreateV1}{SalesInsertTriggerCreateV1}{IsCorrectSalePaidByFuncCreateV1}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{SalesUpdateTriggerDrop}{SalesInsertTriggerDrop}{IsCorrectSalePaidByFuncDrop}");
            migrationBuilder.UpdateData(
                table: "sales",
                keyColumn: "paid_by",
                keyValue: null,
                column: "paid_by",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paid_by",
                table: "sales",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
