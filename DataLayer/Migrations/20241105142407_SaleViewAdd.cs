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
            migrationBuilder.DropTable(name: "sales_view");
            migrationBuilder.Sql($"{CreateTotalGoodsUnitsCountV1}{CreateTotalPriceV1}{CreateSalesViewV1}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sales_view",
                columns: table => new
                {
                    goods_units_count = table.Column<int>(type: "int", nullable: false),
                    is_paid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    paid_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reservation_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    returning_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    sale_date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    sale_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            throw new Exception("more2qqqqqqqqqqqqqqqqqqqqqqqqqqqq");
        }
    }
}
