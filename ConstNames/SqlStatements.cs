using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;
public class SqlStatements
{
    public const string CreateTotalPriceV1 =
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
          END;";
    public const string CreateSalesViewV1 =
        @"CREATE VIEW sales_view AS
          SELECT `sale_id`, `date`, `status`, total_price(sale_id) AS `total`
          FROM `sales`;";
    public const string DropSalesView = @"DROP VIEW `sales_view`;";
    public const string DropTotalPriceFunction = @"DROP FUNCTION `total_price`;";
    public const string DropGoodsForeignKeys =
        @"ALTER TABLE `accessories` DROP FOREIGN KEY `FK_accessories_sales_sale_id`;
          ALTER TABLE `musical_instruments` DROP FOREIGN KEY `FK_musical_instruments_sales_sale_id`;
          ALTER TABLE `sheet_music_editions` DROP FOREIGN KEY `FK_sheet_music_editions_sales_sale_id`;
          ALTER TABLE `audio_equipment_units` DROP FOREIGN KEY `FK_audio_equipment_units_sales_sale_id`;";
    public const string RestoreGoodsForeignKeys =
        @"ALTER TABLE `accessories` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);
          ALTER TABLE `musical_instruments` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);
          ALTER TABLE `sheet_music_editions` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);
          ALTER TABLE `audio_equipment_units` ADD FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`);";
    public const string CreateTotalPriceV2 =
        @"CREATE FUNCTION total_price(sale_id char(36))
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
          END;";
    public const string CreateSalesViewV2 =
        @"CREATE VIEW sales_view AS
          SELECT `sale_id`, `date`, `status`, total_price(sale_id) AS `total`, paid_by, total_goods_units_count(sale_id) as `goods_units_count`
          FROM `sales`;";
    public const string CreateTotalGoodsUnitsCountV1 =
        @"CREATE FUNCTION total_goods_units_count(sale_id char(36))
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
          END;";
    public const string DropTotalGoodsUnitsCountFunction = @"DROP FUNCTION `total_price`;";
    public const string CreateTotalPriceV3 =
        @"CREATE FUNCTION total_price(sale_id char(36))
          RETURNS INT
          NOT DETERMINISTIC
          READS SQL DATA
          BEGIN
              DECLARE total INT;
              SET total = IFNULL((SELECT SUM(mi.price) FROM musical_instruments AS mi WHERE mi.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT SUM(ac.price) FROM accessories AS ac WHERE ac.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT SUM(sme.price) FROM sheet_music_editions AS sme WHERE sme.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT SUM(aeu.price) FROM audio_equipment_units AS aeu WHERE aeu.sale_id = sale_id), 0);
              RETURN total;
          END;";
    public const string CreateSalesViewV3 =
        @"CREATE VIEW sales_view AS
          SELECT `sale_id`, `sale_date`, `reservation_date`, `returning_date`, `status`, total_price(sale_id) AS `total`, paid_by, total_goods_units_count(sale_id) as `goods_units_count`
          FROM `sales`;";
}
