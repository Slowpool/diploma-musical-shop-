using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
    @"DELIMITER //
      CREATE FUNCTION total_price(sale_id: int)
      RETURNS INT
      NOT DETERMINISTIC
      BEGIN
          DECLARE INT total DEFAULT 0;
          total = total + (SELECT SUM(mi.price) FROM musical_instruments AS mi WHERE mi.sale_i sale_id);
          total = total + (SELECT SUM(ac.price) FROM accessories AS ac WHERE ac.sale_id=sale_id);
          total = total + (SELECT SUM(sme.price) FROM sheet_music_editions AS sme WHERE sme.sale_i  =sale_id);
          total = total + (SELECT SUM(aeu.price) FROM audio_equipment_units AS aeu WHERE aeu.sale_id =sale_id);
          RETURN total;
      END//
      DELIMITER ;");
            migrationBuilder.Sql(@"CREATE VIEW `sale_view` AS
                                   SELECT `sale_id`, `sate`, `status`, (total_price(sale_id))
                                   FROM `sale`;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW `sale_view`;");
            migrationBuilder.Sql(@"DROP FUNCTION `total_price`;");
        }
    }
}
