CREATE VIEW sale_view AS
SELECT `sale_id`, `sate`, `status`, total_price(sale_id) AS `total`
FROM `sale`;