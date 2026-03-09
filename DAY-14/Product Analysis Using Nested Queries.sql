USE EcommDb;
GO

SELECT 
    p1.product_name + ' (' + CAST(p1.model_year AS VARCHAR(4)) + ')' AS Product_Info,
    p1.model_year,
    p1.list_price,
    p1.list_price - (
        SELECT AVG(p2.list_price)
        FROM products p2
        WHERE p2.category_id = p1.category_id
    ) AS Price_Difference
FROM products p1
WHERE p1.list_price > (
        SELECT AVG(p2.list_price)
        FROM products p2
        WHERE p2.category_id = p1.category_id
);