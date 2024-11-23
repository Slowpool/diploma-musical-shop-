using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;
public class SqlStatements
{
    #region Migrations statements
    public const string DropSalesView = @"DROP VIEW `sales_view`;";
    public const string DropTotalPriceFunction = @"DROP FUNCTION `total_price`;";
    public const string DropTotalGoodsUnitsCountFunction = @"DROP FUNCTION `total_price`;";

    public const string CreateSalesViewV1 =
        @"CREATE VIEW `sales_view` AS
          SELECT `sale_id`, `sale_date`, `reservation_date`, `returning_date`, `status`, total_price(sale_id) AS `total`, `paid_by`, total_goods_units_count(sale_id) as `goods_units_count`, `is_paid`
          FROM `sales`;";
    public const string CreateTotalGoodsUnitsCountV1 =
        @"CREATE FUNCTION total_goods_units_count(sale_id char(36))
          RETURNS INT
          NOT DETERMINISTIC
          READS SQL DATA
          BEGIN
              DECLARE total INT;
              SET total = IFNULL((SELECT COUNT(*) FROM musical_instrument_sale AS mis WHERE mis.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM accessory_sale AS acs WHERE acs.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM sheet_music_edition_sale AS smes WHERE smes.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM audio_equipment_unit_sale AS aeus WHERE aeus.sale_id = sale_id), 0);
              RETURN total;
          END;";
    public const string CreateTotalPriceV1 = @"CREATE FUNCTION total_price(sale_id char(36))
          RETURNS INT
          NOT DETERMINISTIC
          READS SQL DATA
          BEGIN
              DECLARE total INT;
              SET total = IFNULL((SELECT SUM(`mi`.`price`) FROM `musical_instrument_sale` AS `mis` LEFT JOIN `musical_instruments` AS `mi` ON `mi`.`goods_id` = `mis`.`musical_instrument_id` WHERE `mis`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `accessory_sale` AS `linking_table` LEFT JOIN `accessories` AS `goods` ON `goods`.`goods_id` = `linking_table`.`accessory_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `sheet_music_edition_sale` AS `linking_table` LEFT JOIN `sheet_music_editions` AS `goods` ON `goods`.`goods_id` = `linking_table`.`sheet_music_edition_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `audio_equipment_unit_sale` AS `linking_table` LEFT JOIN `audio_equipment_units` AS `goods` ON `goods`.`goods_id` = `linking_table`.`audio_equipment_unit_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              RETURN total;
          END;";
    #endregion

    #region Backup statements
    public const string RestoreDatabaseFromBackup = "SOURCE {1}; ";

    #endregion

    #region Triggers
    public const string SalesUpdateTriggerDrop = @"DROP TRIGGER IF EXISTS `sale_paid_by_check_UPD`;";
    public const string SalesUpdateTriggerCreateV1 = @"
#CREATE TRIGGER `sale_paid_by_check_INS`
#BEFORE INSERT ON `sales`
#FOR EACH ROW
#BEGIN
#    IF(!is_correct_sale_paid_by(new.paid_by, new.is_paid)) THEN
#        SIGNAL SQLSTATE '40000';
#    END IF;
#END;";
    public const string SalesInsertTriggerDrop = @"DROP TRIGGER IF EXISTS `sale_paid_by_check_INS`;";
// TODO add another check for status and uncomment
    public const string SalesInsertTriggerCreateV1 = @"
#CREATE TRIGGER `sale_paid_by_check_UPD`
#BEFORE UPDATE ON `sales`
#FOR EACH ROW
#BEGIN
#    IF(!is_correct_sale_paid_by(new.paid_by, new.is_paid)) THEN
#        SIGNAL SQLSTATE '40000';
#    END IF;
#END;";
    public const string IsCorrectSalePaidByFuncDrop = @"DROP FUNCTION IF EXISTS `is_correct_sale_paid_by`;";
    public const string IsCorrectSalePaidByFuncCreateV1 = @"
#CREATE FUNCTION is_correct_sale_paid_by(paid_by VARCHAR(8), is_paid BOOL)
#RETURNS BOOL
#DETERMINISTIC
#CONTAINS SQL
#SQL SECURITY DEFINER
#BEGIN
#    IF(is_paid = true) THEN
#        RETURN paid_by IS NOT NULL;
#    ELSE
#        RETURN paid_by IS NULL; 
#    END IF;
#END;";

    #endregion
}
