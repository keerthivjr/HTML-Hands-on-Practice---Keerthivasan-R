USE StoreDb;
GO

SELECT 
    p.product_name,
    s.store_name,
    st.quantity AS available_stock,
    ISNULL(SUM(oi.quantity), 0) AS total_quantity_sold
FROM stocks st

-- INNER JOIN products
INNER JOIN products p
    ON st.product_id = p.product_id

-- INNER JOIN stores
INNER JOIN stores s
    ON st.store_id = s.store_id

-- LEFT JOIN orders (only completed orders optional if needed)
LEFT JOIN orders o
    ON st.store_id = o.store_id
    AND o.order_status = 4

-- LEFT JOIN order_items
LEFT JOIN order_items oi
    ON o.order_id = oi.order_id
    AND st.product_id = oi.product_id

GROUP BY 
    p.product_name,
    s.store_name,
    st.quantity

ORDER BY 
    p.product_name ASC;