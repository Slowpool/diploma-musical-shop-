using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SalePaidByRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaidBy",
                table: "sales",
                newName: "paid_by");
            migrationBuilder.Sql($"{DropSalesView}{DropTotalPriceFunction}{CreateTotalPriceV2}{CreateTotalGoodsUnitsCountV1}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paid_by",
                table: "sales",
                newName: "PaidBy");
            migrationBuilder.Sql($"{DropSalesView}{DropTotalGoodsUnitsCountFunction}{DropTotalPriceFunction}{CreateTotalPriceV1}{CreateSalesViewV1}");
        }
    }
}
