IF DB_ID('TestDb') IS NULL
BEGIN
    CREATE DATABASE TestDb;
END
GO

USE TestDb;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100)
);
GO

INSERT INTO Users (Name, Email)
VALUES ('Alice', 'alice@example.com'), ('Bob', 'bob@example.com');
GO
