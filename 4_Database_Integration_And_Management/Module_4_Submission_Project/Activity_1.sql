-- creating database
    CREATE DATABASE IF NOT EXISTS SmartShop
        CHARACTER SET utf8mb4
        COLLATE utf8mb4_unicode_ci;

    USE SmartShop;




-- suppliers 
    CREATE TABLE IF NOT EXISTS Suppliers (
        SupplierID   INT           AUTO_INCREMENT PRIMARY KEY,
        SupplierName VARCHAR(100)  NOT NULL,
        ContactEmail VARCHAR(150)  NOT NULL,
        Phone        VARCHAR(20),
        Country      VARCHAR(60)   NOT NULL,
        CreatedAt    TIMESTAMP     DEFAULT CURRENT_TIMESTAMP
    );




-- products
    CREATE TABLE IF NOT EXISTS Products (
        ProductID    INT            AUTO_INCREMENT PRIMARY KEY,
        ProductName  VARCHAR(150)   NOT NULL,
        Category     VARCHAR(80)    NOT NULL,
        Price        DECIMAL(10,2)  NOT NULL  CHECK (Price >= 0),
        StockLevel   INT            NOT NULL  DEFAULT 0,
        SupplierID   INT,
        UpdatedAt    TIMESTAMP      DEFAULT CURRENT_TIMESTAMP
                                    ON UPDATE CURRENT_TIMESTAMP,
        CONSTRAINT fk_supplier
            FOREIGN KEY (SupplierID)
            REFERENCES Suppliers(SupplierID)
            ON DELETE SET NULL
    );




-- selecting product
    SELECT
        ProductName,
        Category,
        Price,
        StockLevel
    FROM
        Products;





-- filtering by Category
    SELECT
        ProductName,
        Category,
        Price,
        StockLevel
    FROM
        Products
    WHERE
        Category = 'Electronics';




--filtering by stock level
    SELECT
        ProductName,
        Category,
        Price,
        StockLevel
    FROM
        Products
    WHERE
        StockLevel < 20;




-- sorting by price
    SELECT
        ProductName,
        Category,
        Price,
        StockLevel
    FROM
        Products
    ORDER BY
        Price ASC;