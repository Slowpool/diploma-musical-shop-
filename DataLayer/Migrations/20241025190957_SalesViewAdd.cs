using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class SalesViewAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
    @"CREATE FUNCTION total_price(sale_id INT)
      RETURNS INT
      NOT DETERMINISTIC
      READS SQL DATA
      BEGIN
          DECLARE total INT;
          SET total = (SELECT SUM(mi.price) FROM musical_instruments AS mi WHERE mi.sale_id = sale_id);
          SET total = total + (SELECT SUM(ac.price) FROM accessories AS ac WHERE ac.sale_id = sale_id);
          SET total = total + (SELECT SUM(sme.price) FROM sheet_music_editions AS sme WHERE sme.sale_id = sale_id);
          SET total = total + (SELECT SUM(aeu.price) FROM audio_equipment_units AS aeu WHERE aeu.sale_id = sale_id);
          RETURN total;
      END;");
            migrationBuilder.Sql(@"CREATE VIEW sales_view AS
                                   SELECT `sale_id`, `date`, `status`, total_price(sale_id) AS `total`, paid_by
                                   FROM `sales`;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW `sales_view`;");
            migrationBuilder.Sql(@"DROP FUNCTION `total_price`;");
        }
    }
}
