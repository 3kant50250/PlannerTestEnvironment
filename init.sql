-- Create database
IF DB_ID('TestDb') IS NULL
BEGIN
    CREATE DATABASE TestDb;
END
GO

-- Switch to TestDb
USE TestDb;
GO

-- Create sample table
IF OBJECT_ID('Users', 'U') IS NULL
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY,
        Name NVARCHAR(100),
        Email NVARCHAR(100)
    );
END
GO

-- Seed data
INSERT INTO Users (Name, Email)
VALUES 
('Alice Smith', 'alice@example.com'),
('Bob Johnson', 'bob@example.com');
GO
