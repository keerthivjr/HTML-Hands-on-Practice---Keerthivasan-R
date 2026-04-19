-- 1. Create the Database
CREATE DATABASE InsuranceDB;
GO

USE InsuranceDB;
GO

-- 2. Create the Claims Table
CREATE TABLE Claims (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerEmail NVARCHAR(100) NOT NULL,
    ClaimAmount DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(500) NULL,
    Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Processed', 'Failed')),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 3. Insert some dummy data to test with
INSERT INTO Claims (CustomerName, CustomerEmail, ClaimAmount, Description)
VALUES
('Vijay Shankar', 'vijay@example.com', 1500.00, 'Car accident damage'),
('Kumaran', 'kumaran@email.com', 350.50, 'Lost luggage'),
('Dinesh', 'Dinesh@company.com', 2000.00, 'Medical bill');
GO