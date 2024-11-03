using Microsoft.EntityFrameworkCore.Migrations;
using static Common.SqlStatements;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SaleViewAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{CreateTotalPriceV1}{CreateTotalGoodsUnitsCountV1}{CreateSalesViewV1}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new Exception("no, you can't");
        }
    }
}
