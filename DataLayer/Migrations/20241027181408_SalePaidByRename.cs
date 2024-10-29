using Microsoft.EntityFrameworkCore.Migrations;

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
            migrationBuilder.Sql(@"
DROP VIEW sales_view;
DROP FUNCTION total_price;
CREATE FUNCTION total_price(sale_id char(36))
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
END;
CREATE FUNCTION total_goods_units_count(sale_id char(36))
RETURNS INT
NOT DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE total INT;
    SET total = (SELECT COUNT(*) FROM musical_instruments AS mi WHERE mi.sale_id = sale_id);
    SET total = total + (SELECT COUNT(*) FROM accessories AS ac WHERE ac.sale_id = sale_id);
    SET total = total + (SELECT COUNT(*) FROM sheet_music_editions AS sme WHERE sme.sale_id = sale_id);
    SET total = total + (SELECT COUNT(*) FROM audio_equipment_units AS aeu WHERE aeu.sale_id = sale_id);
    RETURN total;
END;
CREATE VIEW sales_view AS
SELECT `sale_id`, `date`, `status`, total_price(sale_id) AS `total`, paid_by, total_goods_units_count(sale_id) as `goods_units_count`
FROM `sales`;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paid_by",
                table: "sales",
                newName: "PaidBy");
            migrationBuilder.Sql(@"
      DROP VIEW sales_view;
      DROP FUNCTION total_goods_units_count;
      DROP FUNCTION total_price;
      CREATE FUNCTION total_price(sale_id INT)
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
      END;
      CREATE VIEW sales_view AS
      SELECT `sale_id`, `date`, `status`, total_price(sale_id) AS `total`
      FROM `sales`;");
        }
    }
}
