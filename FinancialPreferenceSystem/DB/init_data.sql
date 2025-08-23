USE FinanceLikeDB;
GO

-- 建立測試使用者
INSERT INTO [User] (UserID, UserName, Email, Account)
VALUES 
('A1236456789', N'王小明', 'test@email.com', '1111999666')

-- 建立測試產品
INSERT INTO [Product] (ProductName, Price, FeeRate)
VALUES
(N'股票型基金', 1000.00, 0.02),   -- 手續費 2%
(N'債券型基金', 500.00, 0.015),   -- 手續費 1.5%
(N'ETF指數型', 50.00, 0.01);      -- 手續費 1%

-- 直接模擬新增一筆
-- 小明買 2 單位股票型基金
DECLARE @price DECIMAL(18,2), @feeRate DECIMAL(5,2), @totalAmount DECIMAL(18,2), @totalFee DECIMAL(18,2);

SELECT @price = Price, @feeRate = FeeRate FROM Product WHERE ProductName = N'股票型基金';

SET @totalAmount = @price * 2;
SET @totalFee = @totalAmount * @feeRate;

INSERT INTO [LikeList] (UserID, Nom, OrderQty, Account, TotalAmount, TotalFee)
VALUES ('A1236456789', 1, 2, '1111999666', @totalAmount, @totalFee);
