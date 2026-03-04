-- Step 1: If database exists, delete it
IF DB_ID('StoreDb') IS NOT NULL
BEGIN
    ALTER DATABASE StoreDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE StoreDb;
END
GO

-- Step 2: Create database
CREATE DATABASE StoreDb;
GO

USE StoreDb;
GO

-- Step 3: Create customers table
CREATE TABLE customers
(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL
);

-- Step 4: Create orders table
CREATE TABLE orders
(
    order_id INT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    order_status INT NOT NULL,

    CONSTRAINT FK_orders_customer
        FOREIGN KEY (customer_id)
        REFERENCES customers(customer_id)
);

-- Step 5: Insert customers
INSERT INTO customers VALUES
(1, 'John', 'Smith'),
(2, 'Emma', 'Watson'),
(3, 'David', 'Miller');

-- Step 6: Insert orders
INSERT INTO orders VALUES
(101, 1, '2026-03-01', 1),
(102, 1, '2026-03-05', 4),
(103, 2, '2026-03-10', 2),
(104, 3, '2026-03-15', 1),
(105, 2, '2026-03-20', 4);

-- Step 7: Final Required Query
SELECT 
    c.first_name,
    c.last_name,
    o.order_id,
    o.order_date,
    o.order_status
FROM customers c
INNER JOIN orders o
    ON c.customer_id = o.customer_id
WHERE o.order_status IN (1,4)
ORDER BY o.order_date DESC;