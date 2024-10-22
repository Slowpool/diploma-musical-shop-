DELIMITER //
CREATE TRIGGER sale_accessory_check
BEFORE INSERT
ON accessories
FOR EACH ROW
BEGIN
    IF NEW.sale_id IS NOT NULL THEN
        IF (SELECT sale_id)

END//
DELIMITER ;