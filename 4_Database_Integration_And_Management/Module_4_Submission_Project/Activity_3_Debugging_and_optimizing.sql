-- ============================================================
--  SmartShop Inventory System
--  Activity 1 + Activity 2 + Activity 3
--  Debugging, Optimization & Final Deliverable
-- ============================================================




-- ============================================================
--  KROK 1: TWORZENIE BAZY DANYCH
-- ============================================================

CREATE DATABASE IF NOT EXISTS SmartShop
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE SmartShop;




-- ============================================================
--  KROK 2: TABELE
-- ============================================================

-- suppliers
CREATE TABLE IF NOT EXISTS Suppliers (
    SupplierID   INT           AUTO_INCREMENT PRIMARY KEY,
    SupplierName VARCHAR(100)  NOT NULL,
    ContactEmail VARCHAR(150)  NOT NULL,
    Phone        VARCHAR(20),
    Country      VARCHAR(60)   NOT NULL,
    CreatedAt    TIMESTAMP     DEFAULT CURRENT_TIMESTAMP
);


-- stores
CREATE TABLE IF NOT EXISTS Stores (
    StoreID     INT          AUTO_INCREMENT PRIMARY KEY,
    StoreName   VARCHAR(100) NOT NULL,
    City        VARCHAR(80)  NOT NULL,
    Region      VARCHAR(80),
    OpenedDate  DATE
);


-- products
CREATE TABLE IF NOT EXISTS Products (
    ProductID   INT            AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(150)   NOT NULL,
    Category    VARCHAR(80)    NOT NULL,
    Price       DECIMAL(10,2)  NOT NULL  CHECK (Price >= 0),
    StockLevel  INT            NOT NULL  DEFAULT 0,
    SupplierID  INT,
    UpdatedAt   TIMESTAMP      DEFAULT CURRENT_TIMESTAMP
                               ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_supplier
        FOREIGN KEY (SupplierID)
        REFERENCES Suppliers(SupplierID)
        ON DELETE SET NULL
);


-- sales
CREATE TABLE IF NOT EXISTS Sales (
    SaleID      INT            AUTO_INCREMENT PRIMARY KEY,
    ProductID   INT            NOT NULL,
    StoreID     INT            NOT NULL,
    Quantity    INT            NOT NULL  CHECK (Quantity > 0),
    SaleDate    DATE           NOT NULL,
    TotalPrice  DECIMAL(12,2)  NOT NULL,
    CONSTRAINT fk_sale_product
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    CONSTRAINT fk_sale_store
        FOREIGN KEY (StoreID)   REFERENCES Stores(StoreID)
);




-- ============================================================
--  KROK 3: INDEKSY — Activity 3 (optymalizacja)
--  Dodane na kolumnach najczęściej używanych w WHERE / JOIN
-- ============================================================

-- Products: szybkie filtrowanie po kategorii i dostawcy
CREATE INDEX IF NOT EXISTS idx_products_category
    ON Products (Category);

CREATE INDEX IF NOT EXISTS idx_products_supplier
    ON Products (SupplierID);

-- Sales: szybkie JOIN-y i filtrowanie po dacie
CREATE INDEX IF NOT EXISTS idx_sales_product
    ON Sales (ProductID);

CREATE INDEX IF NOT EXISTS idx_sales_store
    ON Sales (StoreID);

CREATE INDEX IF NOT EXISTS idx_sales_date
    ON Sales (SaleDate);

-- indeks złożony: zapytania filtrujące produkt + datę jednocześnie
CREATE INDEX IF NOT EXISTS idx_sales_product_date
    ON Sales (ProductID, SaleDate);




-- ============================================================
--  KROK 4: DANE TESTOWE
-- ============================================================

INSERT INTO Suppliers (SupplierName, ContactEmail, Phone, Country) VALUES
    ('TechDistrib SA',  'contact@techdistrib.pl', '+48221234567', 'Poland'),
    ('FurniPro GmbH',   'order@furnipro.de',       '+493019876',   'Germany'),
    ('PaperWorld Ltd',  'sales@paperworld.co.uk',  '+442071234',   'UK');


INSERT INTO Stores (StoreName, City, Region, OpenedDate) VALUES
    ('SmartShop Warszawa', 'Warszawa', 'Mazowsze',   '2019-03-15'),
    ('SmartShop Kraków',   'Kraków',   'Małopolska', '2020-07-01'),
    ('SmartShop Gdańsk',   'Gdańsk',   'Pomorze',    '2021-11-20');


INSERT INTO Products (ProductName, Category, Price, StockLevel, SupplierID) VALUES
    ('Laptop Pro 15',     'Electronics', 4299.00, 120, 1),
    ('Wireless Mouse',    'Electronics',   89.99, 340, 1),
    ('USB-C Hub',         'Electronics',  149.99,  15, 1),
    ('Office Chair',      'Furniture',    799.00,   8, 2),
    ('Standing Desk',     'Furniture',   1299.00,  42, 2),
    ('Notebook A4',       'Stationery',   12.99,  980, 3),
    ('Ballpoint Pen Set', 'Stationery',   24.99,    5, 3);


INSERT INTO Sales (ProductID, StoreID, Quantity, SaleDate, TotalPrice) VALUES
    (1, 1,  2, '2024-11-01',  8598.00),
    (2, 1,  5, '2024-11-03',   449.95),
    (4, 2,  1, '2024-11-05',   799.00),
    (3, 3,  3, '2024-11-07',   449.97),
    (6, 2, 20, '2024-11-10',   259.80),
    (5, 1,  1, '2024-11-12',  1299.00),
    (1, 3,  1, '2024-11-15',  4299.00),
    (7, 1, 10, '2024-11-18',   249.90),
    (2, 2,  8, '2024-11-20',   719.92),
    (3, 1,  2, '2024-11-22',   299.98);




-- ============================================================
--  ACTIVITY 1 — podstawowe zapytania
-- ============================================================

-- selecting product details
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


-- filtering by stock level
SELECT
    ProductName,
    Category,
    Price,
    StockLevel
FROM
    Products
WHERE
    StockLevel < 20;


-- sorting by price ascending
SELECT
    ProductName,
    Category,
    Price,
    StockLevel
FROM
    Products
ORDER BY
    Price ASC;




-- ============================================================
--  ACTIVITY 2 — zapytania złożone
-- ============================================================

-- JOIN 1: ProductName, SaleDate, StoreLocation, UnitsSold
SELECT
    p.ProductName,
    s.SaleDate,
    st.City        AS StoreLocation,
    s.Quantity     AS UnitsSold
FROM
    Sales s
    JOIN Products p  ON s.ProductID = p.ProductID
    JOIN Stores   st ON s.StoreID   = st.StoreID
ORDER BY
    s.SaleDate ASC;


-- JOIN 2: sprzedaż z danymi dostawcy
SELECT
    p.ProductName,
    p.Category,
    sup.SupplierName,
    s.SaleDate,
    s.Quantity     AS UnitsSold,
    s.TotalPrice
FROM
    Sales s
    JOIN Products  p   ON s.ProductID  = p.ProductID
    JOIN Suppliers sup ON p.SupplierID = sup.SupplierID
ORDER BY
    s.TotalPrice DESC;


-- AGREGACJA 1: całkowita sprzedaż dla każdego produktu
SELECT
    p.ProductName,
    p.Category,
    SUM(s.Quantity)    AS TotalUnitsSold,
    SUM(s.TotalPrice)  AS TotalRevenue
FROM
    Sales s
    JOIN Products p ON s.ProductID = p.ProductID
GROUP BY
    p.ProductID,
    p.ProductName,
    p.Category
ORDER BY
    TotalRevenue DESC;


-- AGREGACJA 2: wyniki dostawców
SELECT
    sup.SupplierName,
    sup.Country,
    COUNT(DISTINCT p.ProductID)  AS ProductsSupplied,
    SUM(s.Quantity)              AS TotalUnitsSold,
    SUM(s.TotalPrice)            AS TotalRevenue,
    AVG(s.TotalPrice)            AS AvgSaleValue
FROM
    Suppliers sup
    JOIN Products p ON sup.SupplierID = p.SupplierID
    JOIN Sales    s ON p.ProductID    = s.ProductID
GROUP BY
    sup.SupplierID,
    sup.SupplierName,
    sup.Country
ORDER BY
    TotalRevenue DESC;


-- AGREGACJA 3: MAX i AVG per kategoria
SELECT
    p.Category,
    COUNT(DISTINCT p.ProductID)  AS NumberOfProducts,
    AVG(p.Price)                 AS AvgPrice,
    MAX(p.Price)                 AS MaxPrice,
    SUM(s.Quantity)              AS TotalUnitsSold,
    SUM(s.TotalPrice)            AS TotalRevenue
FROM
    Products p
    JOIN Sales s ON p.ProductID = s.ProductID
GROUP BY
    p.Category
ORDER BY
    TotalRevenue DESC;


-- PODZAPYTANIE 1: produkty ze sprzedażą powyżej średniej
SELECT
    p.ProductName,
    p.Category,
    p.Price
FROM
    Products p
WHERE
    p.ProductID IN (
        SELECT  s.ProductID
        FROM    Sales s
        GROUP BY s.ProductID
        HAVING  SUM(s.Quantity) > (
            SELECT AVG(total_qty)
            FROM (
                SELECT SUM(Quantity) AS total_qty
                FROM   Sales
                GROUP BY ProductID
            ) AS sub
        )
    )
ORDER BY
    p.Price DESC;


-- PODZAPYTANIE 2: dostawca z najwyższym wolumenem
SELECT
    sup.SupplierName,
    sup.Country,
    SUM(s.Quantity) AS TotalUnitsSold
FROM
    Suppliers sup
    JOIN Products p ON sup.SupplierID = p.SupplierID
    JOIN Sales    s ON p.ProductID    = s.ProductID
GROUP BY
    sup.SupplierID,
    sup.SupplierName,
    sup.Country
HAVING
    SUM(s.Quantity) = (
        SELECT MAX(supplier_total)
        FROM (
            SELECT SUM(s2.Quantity) AS supplier_total
            FROM   Suppliers sup2
            JOIN   Products   p2  ON sup2.SupplierID = p2.SupplierID
            JOIN   Sales      s2  ON p2.ProductID    = s2.ProductID
            GROUP BY sup2.SupplierID
        ) AS sub
    );


-- PODZAPYTANIE 3: stan magazynu vs średnia kategorii
SELECT
    p.ProductName,
    p.Category,
    p.StockLevel,
    ROUND(cat_avg.AvgStock, 0) AS CategoryAvgStock,
    CASE
        WHEN p.StockLevel < cat_avg.AvgStock THEN 'Poniżej średniej'
        ELSE 'OK'
    END AS StockStatus
FROM
    Products p
    JOIN (
        SELECT   Category, AVG(StockLevel) AS AvgStock
        FROM     Products
        GROUP BY Category
    ) AS cat_avg ON p.Category = cat_avg.Category
ORDER BY
    p.Category,
    p.StockLevel ASC;




-- ============================================================
--  ACTIVITY 3 — debugging i optymalizacja
-- ============================================================


-- ------------------------------------------------------------
--  DEBUG 1: błędny JOIN — brakujący alias powodował
--  "Column 'ProductID' in on clause is ambiguous"
--
--  PRZED (błąd):
--    JOIN Products ON ProductID = ProductID
--
--  PO (poprawka): jawne aliasy tabel po obu stronach ON
-- ------------------------------------------------------------
SELECT
    p.ProductName,
    s.SaleDate,
    st.City       AS StoreLocation,
    s.Quantity    AS UnitsSold,
    s.TotalPrice
FROM
    Sales s
    JOIN Products p  ON s.ProductID = p.ProductID   -- poprawiony ON
    JOIN Stores   st ON s.StoreID   = st.StoreID     -- poprawiony ON
ORDER BY
    s.SaleDate ASC;


-- ------------------------------------------------------------
--  DEBUG 2: błędny WHERE w zagnieżdżonym zapytaniu —
--  filtr był poza podzapytaniem zamiast wewnątrz
--
--  PRZED (błąd):
--    SELECT AVG(StockLevel) FROM Products
--    WHERE Category = p.Category   -- p niewidoczne w subquery
--
--  PO (poprawka): podzapytanie skorelowane z GROUP BY
-- ------------------------------------------------------------
SELECT
    p.ProductName,
    p.Category,
    p.StockLevel,
    ROUND(cat_avg.AvgStock, 0) AS CategoryAvgStock,
    CASE
        WHEN p.StockLevel < cat_avg.AvgStock THEN 'Wymaga uzupełnienia'
        ELSE 'Poziom OK'
    END AS StockStatus
FROM
    Products p
    JOIN (
        SELECT   Category,
                 AVG(StockLevel) AS AvgStock   -- poprawiona agregacja
        FROM     Products
        GROUP BY Category
    ) AS cat_avg ON p.Category = cat_avg.Category  -- poprawiony JOIN
ORDER BY
    p.Category,
    p.StockLevel ASC;


-- ------------------------------------------------------------
--  DEBUG 3: brakujące GROUP BY przy COUNT / SUM
--  powodowało "Expression not in GROUP BY"
--
--  PRZED (błąd):
--    SELECT SupplierName, COUNT(ProductID), SUM(Quantity)
--    FROM Suppliers JOIN ...
--    (brak GROUP BY)
--
--  PO (poprawka): pełna lista niemierzonych kolumn w GROUP BY
-- ------------------------------------------------------------
SELECT
    sup.SupplierName,
    sup.Country,
    COUNT(DISTINCT p.ProductID) AS ProductsSupplied,
    SUM(s.Quantity)             AS TotalUnitsSold,
    SUM(s.TotalPrice)           AS TotalRevenue
FROM
    Suppliers sup
    JOIN Products p ON sup.SupplierID = p.SupplierID
    JOIN Sales    s ON p.ProductID    = s.ProductID
GROUP BY
    sup.SupplierID,       -- klucz główny gwarantuje unikalność
    sup.SupplierName,
    sup.Country
ORDER BY
    TotalRevenue DESC;


-- ------------------------------------------------------------
--  OPTYMALIZACJA 1: zastąpienie podzapytania IN przez JOIN
--  IN z podzapytaniem skanuje wynik subquery dla każdego wiersza
--  JOIN z pre-agregacją jest wykonywany raz → szybciej
--
--  PRZED (wolniej):
--    WHERE p.ProductID IN (SELECT ProductID FROM Sales
--                          GROUP BY ProductID HAVING SUM(...) > X)
--
--  PO (szybciej): JOIN z zagregowaną tabelą pochodną
-- ------------------------------------------------------------
SELECT
    p.ProductName,
    p.Category,
    p.Price,
    agg.TotalQty
FROM
    Products p
    JOIN (
        SELECT   ProductID,
                 SUM(Quantity) AS TotalQty
        FROM     Sales
        GROUP BY ProductID
    ) AS agg ON p.ProductID = agg.ProductID
WHERE
    agg.TotalQty > (
        SELECT AVG(total_qty)
        FROM (
            SELECT SUM(Quantity) AS total_qty
            FROM   Sales
            GROUP BY ProductID
        ) AS avg_sub
    )
ORDER BY
    p.Price DESC;


-- ------------------------------------------------------------
--  OPTYMALIZACJA 2: EXPLAIN — weryfikacja planu zapytania
--  uruchom EXPLAIN przed i po dodaniu indeksów,
--  aby porównać "rows" i "type" (ALL → ref = poprawa)
-- ------------------------------------------------------------
EXPLAIN
SELECT
    p.ProductName,
    s.SaleDate,
    st.City      AS StoreLocation,
    s.Quantity   AS UnitsSold,
    s.TotalPrice
FROM
    Sales s
    JOIN Products p  ON s.ProductID = p.ProductID
    JOIN Stores   st ON s.StoreID   = st.StoreID
WHERE
    s.SaleDate BETWEEN '2024-11-01' AND '2024-11-30'
ORDER BY
    s.SaleDate ASC;


-- ------------------------------------------------------------
--  OPTYMALIZACJA 3: filtr zakresu dat z indeksem idx_sales_date
--  zastępuje skan całej tabeli Sales
-- ------------------------------------------------------------
SELECT
    p.ProductName,
    p.Category,
    s.SaleDate,
    st.City          AS StoreLocation,
    s.Quantity       AS UnitsSold,
    s.TotalPrice
FROM
    Sales s
    JOIN Products p  ON s.ProductID = p.ProductID
    JOIN Stores   st ON s.StoreID   = st.StoreID
WHERE
    s.SaleDate BETWEEN '2024-11-01' AND '2024-11-30'  -- używa idx_sales_date
ORDER BY
    s.SaleDate ASC;


-- ------------------------------------------------------------
--  OPTYMALIZACJA 4: ROUND dla czytelności wyników DECIMAL
-- ------------------------------------------------------------
SELECT
    p.ProductName,
    p.Category,
    SUM(s.Quantity)              AS TotalUnitsSold,
    ROUND(SUM(s.TotalPrice), 2)  AS TotalRevenue,
    ROUND(AVG(s.TotalPrice), 2)  AS AvgTransactionValue,
    MAX(s.TotalPrice)            AS LargestSale
FROM
    Sales s
    JOIN Products p ON s.ProductID = p.ProductID
GROUP BY
    p.ProductID,
    p.ProductName,
    p.Category
ORDER BY
    TotalRevenue DESC;


-- ------------------------------------------------------------
--  WALIDACJA: sprawdzenie poprawności danych
--  wykrywa osierocone rekordy (FK bez pasującego rekordu)
-- ------------------------------------------------------------

-- produkty bez dostawcy
SELECT
    ProductID,
    ProductName
FROM
    Products
WHERE
    SupplierID IS NULL;

-- transakcje sprzedaży bez pasującego produktu (nie powinno być)
SELECT
    s.SaleID,
    s.ProductID
FROM
    Sales s
    LEFT JOIN Products p ON s.ProductID = p.ProductID
WHERE
    p.ProductID IS NULL;

-- podsumowanie: liczba rekordów w każdej tabeli
SELECT 'Suppliers' AS TableName, COUNT(*) AS RowCount FROM Suppliers
UNION ALL
SELECT 'Stores',   COUNT(*) FROM Stores
UNION ALL
SELECT 'Products', COUNT(*) FROM Products
UNION ALL
SELECT 'Sales',    COUNT(*) FROM Sales;