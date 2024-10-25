DELIMITER //
CREATE FUNCTION total_price(sale_id: int)
RETURNS INT
NOT DETERMINISTIC
BEGIN
    DECLARE INT total DEFAULT 0;
    total = total + (SELECT SUM(mi.price) FROM musical_instruments AS mi WHERE mi.sale_id = sale_id);
    total = total + (SELECT SUM(ac.price) FROM accessories AS ac WHERE ac.sale_id = sale_id);
    total = total + (SELECT SUM(sme.price) FROM sheet_music_editions AS sme WHERE sme.sale_id = sale_id);
    total = total + (SELECT SUM(aeu.price) FROM audio_equipment_units AS aeu WHERE aeu.sale_id = sale_id);
    RETURN total;
END//
DELIMITER ;