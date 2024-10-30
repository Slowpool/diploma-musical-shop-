using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleViewGoodsCountRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{DropTotalPriceFunction}{CreateTotalPriceV3}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{DropTotalPriceFunction}{CreateTotalPriceV2}");
        }
    }
}
