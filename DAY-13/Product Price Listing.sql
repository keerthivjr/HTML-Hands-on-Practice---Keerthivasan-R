USE StoreDb;
GO

/* =========================================
   DROP TABLES (Child → Parent Order)
========================================= */

IF OBJECT_ID('stocks','U') IS NOT NULL DROP TABLE stocks;
IF OBJECT_ID('order_items','U') IS NOT NULL DROP TABLE order_items;
IF OBJECT_ID('products','U') IS NOT NULL DROP TABLE products;
IF OBJECT_ID('brands','U') IS NOT NULL DROP TABLE brands;
IF OBJECT_ID('categories','U') IS NOT NULL DROP TABLE categories;
IF OBJECT_ID('stores','U') IS NOT NULL DROP TABLE stores;
GO

/* =========================================
   CREATE TABLES
========================================= */

-- Brands
CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(100)
);

-- Categories
CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(100)
);

-- Products
CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

-- Stores
CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

-- Order Items
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

-- Stocks
CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

GO

/* =========================================
   INSERT DATA
========================================= */

INSERT INTO brands VALUES
(1,'Nike'),
(2,'Adidas');

INSERT INTO categories VALUES
(1,'Shoes'),
(2,'Accessories');

INSERT INTO products VALUES
(1,'Air Max',1,1,2024,800),
(2,'Running Pro',2,1,2023,600),
(3,'Cap Classic',1,2,2024,200);

INSERT INTO stores VALUES
(1,'Central Store'),
(2,'City Mall Store');

INSERT INTO order_items VALUES
(1,102,1,2,800,0.10),
(2,105,2,1,600,0.05);

INSERT INTO stocks VALUES
(1,1,20),
(1,2,15),
(2,1,10),
(2,3,50);

GO

/* =========================================
   FINAL QUERY (Level-1 Problem-2)
========================================= */

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM products p
INNER JOIN brands b
    ON p.brand_id = b.brand_id
INNER JOIN categories c
    ON p.category_id = c.category_id
WHERE p.list_price > 500
ORDER BY p.list_price ASC;