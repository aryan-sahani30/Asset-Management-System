CREATE DATABASE AssetManagementSystem;
GO

USE AssetManagementSystem;
GO

CREATE TABLE Asset_category (
    category_id INT PRIMARY KEY,
    category_name NVARCHAR(50)
);

CREATE TABLE Asset_Subcategory (
    subcategory_id INT PRIMARY KEY,
    subcategory_name NVARCHAR(50),
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES Asset_category(category_id)
);

CREATE TABLE Employees (
    employee_id INT PRIMARY KEY,
    employee_name NVARCHAR(100)
);

CREATE TABLE ASSET (
    asset_id INT PRIMARY KEY,
    asset_name NVARCHAR(100),
    purchase_date DATE,
    purchase_cost DECIMAL(10, 2),
    asset_category_id INT,
    asset_subcategory_id INT,
    salvage_value DECIMAL(10, 2),
    useful_life INT,
    depreciation_value DECIMAL(10, 2),
    FOREIGN KEY (asset_category_id) REFERENCES Asset_category(category_id),
    FOREIGN KEY (asset_subcategory_id) REFERENCES Asset_Subcategory(subcategory_id)
);

CREATE TABLE Issued_Assets (
    issue_id INT PRIMARY KEY,
    asset_id INT,
    employee_id INT,
    issue_date DATE,
    FOREIGN KEY (asset_id) REFERENCES ASSET(asset_id),
    FOREIGN KEY (employee_id) REFERENCES Employees(employee_id)
);

CREATE TABLE Asset_Maintenance (
    maintenance_id INT PRIMARY KEY,
    asset_id INT,
    renewal_date DATE,
    FOREIGN KEY (asset_id) REFERENCES ASSET(asset_id)
);

INSERT INTO Asset_category (category_id, category_name)
VALUES
    (1, 'Electronics'),
    (2, 'Office Equipment'),
    (3, 'Audio Visual'),
    (4, 'Furniture'),
    (5, 'Meeting Room');

INSERT INTO Asset_Subcategory (subcategory_id, subcategory_name, category_id)
VALUES
    (3, 'Computers', 1),
    (5, 'Printers', 2),
    (8, 'Projectors', 3),
    (10, 'Chairs', 4),
    (13, 'Tables', 5);

INSERT INTO Employees (employee_id, employee_name)
VALUES
    (101, 'John Doe'),
    (102, 'Jane Smith'),
    (103, 'Michael Johnson');

INSERT INTO ASSET (asset_id, asset_name, purchase_date, purchase_cost, asset_category_id, asset_subcategory_id, salvage_value, useful_life, depreciation_value)
VALUES
    (1, 'Laptop', '2023-08-04', 1200.00, 1, 3, 90078, 1, 0),
    (2, 'Printer', '2023-08-04', 400.00, 2, 5, 8000, 6, 0),
    (3, 'Projector', '2023-08-03', 800.00, 3, 8, 6760, 2, 0),
    (4, 'Desk Chair', '2023-08-02', 150.00, 4, 10, 7675, 4, 0),
    (5, 'Conference Table', '2023-08-01', 900.00, 5, 13, 7876, 9, 0);

INSERT INTO Issued_Assets (issue_id, asset_id, employee_id, issue_date)
VALUES
    (1, 1, 101, '2023-07-01'),
    (2, 2, 102, '2023-06-15'),
    (3, 3, 103, '2023-07-10');

INSERT INTO Asset_Maintenance (maintenance_id, asset_id, renewal_date)
SELECT 6, 1, '2023-08-10'
WHERE EXISTS (
    SELECT 1
    FROM ASSET
    WHERE asset_id = 1
);

SET SQL_SAFE_UPDATES 0;
GO

UPDATE ASSET
SET depreciation_value = (purchase_cost - salvage_value) / useful_life;

SELECT *
FROM Issued_Assets;

SELECT *
FROM ASSET;

SELECT *
FROM Employees;

INSERT INTO ASSET (asset_id, asset_name, purchase_date, purchase_cost, asset_category_id, asset_subcategory_id, salvage_value, useful_life, depreciation_value)
VALUES
(77, 'GOOGLE CLOUD', '2023-04-16', 1990.00, 4, 10, 675, 9, 0);

SELECT * FROM ASSET;
