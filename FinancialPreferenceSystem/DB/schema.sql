-- 建立資料庫
CREATE DATABASE FinanceLikeDB;
GO

USE FinanceLikeDB;
GO

-- 使用者資料表
CREATE TABLE [dbo].[User](
    [UserID] NVARCHAR(20) PRIMARY KEY,
    [UserName] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Account] NVARCHAR(20) NOT NULL
);

-- 產品資料表
CREATE TABLE [dbo].[Product](
    [Nom] INT IDENTITY(1,1) PRIMARY KEY,
    [ProductName] NVARCHAR(100) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [FeeRate] DECIMAL(5,2) NOT NULL
);

-- 喜好清單
CREATE TABLE [dbo].[LikeList](
    [SN] INT IDENTITY(1,1) PRIMARY KEY,
    [UserID] NVARCHAR(20) NOT NULL,
    [Nom] INT NOT NULL,
    [OrderQty] INT NOT NULL,
    [Account] NVARCHAR(20) NOT NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    [TotalFee] DECIMAL(18,2) NOT NULL,
    FOREIGN KEY(UserID) REFERENCES [User](UserID),
    FOREIGN KEY(Nom) REFERENCES [Product](Nom)
);
