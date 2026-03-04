USE StoreDb;
GO

/* =========================================
   STEP 1: Modify Orders Table
========================================= */

-- Drop order_items first (child table)
IF OBJECT_ID('order_items','U') IS NOT NULL DROP TABLE order_items;
GO

-- Drop and recreate orders with store_id
IF OBJECT_ID('orders','U') IS NOT NULL DROP TABLE orders;
GO

CREATE TABLE orders
(
    order_id INT PRIMARY KEY,
    customer_id INT NOT NULL,
    store_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    order_status INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);
GO


/* =========================================
   STEP 2: Recreate order_items
========================================= */

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(4,2),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
GO


/* =========================================
   STEP 3: Insert Orders Data
========================================= */

INSERT INTO orders VALUES
(101, 1, 1, '2026-03-01', 1),
(102, 1, 1, '2026-03-05', 4),
(103, 2, 2, '2026-03-10', 4),
(104, 3, 2, '2026-03-15', 1),
(105, 2, 1, '2026-03-20', 4);

INSERT INTO order_items VALUES
(1,102,1,2,800,0.10),
(2,105,2,1,600,0.05),
(3,103,1,1,800,0.00);
GO


/* =========================================
   STEP 4: LEVEL-2 PROBLEM-1 QUERY
========================================= */

SELECT 
    s.store_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM stores s
INNER JOIN orders o
    ON s.store_id = o.store_id
INNER JOIN order_items oi
    ON o.order_id = oi.order_id
WHERE o.order_status = 4
GROUP BY s.store_name
ORDER BY total_sales DESC;