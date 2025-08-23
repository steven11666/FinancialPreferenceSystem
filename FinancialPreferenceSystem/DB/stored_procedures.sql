USE FinanceLikeDB;
GO

-- 新增喜好商品
CREATE PROCEDURE sp_AddLikeProduct
    @UserID NVARCHAR(20),
    @Nom INT,
    @OrderQty INT,
    @Account NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @Price DECIMAL(18,2);
        DECLARE @FeeRate DECIMAL(5,2);
        DECLARE @TotalAmount DECIMAL(18,2);
        DECLARE @TotalFee DECIMAL(18,2);

        SELECT @Price = Price, @FeeRate = FeeRate FROM Product WHERE Nom = @Nom;

        SET @TotalAmount = @Price * @OrderQty;
        SET @TotalFee = @TotalAmount * @FeeRate;

        INSERT INTO LikeList(UserID, Nom, OrderQty, Account, TotalAmount, TotalFee)
        VALUES(@UserID, @Nom, @OrderQty, @Account, @TotalAmount, @TotalFee);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 查詢喜好清單
CREATE PROCEDURE sp_GetLikeList
    @UserID NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        L.SN,
        L.UserID,
        L.Nom,
        P.ProductName,
        P.Price,
        P.FeeRate,
        L.OrderQty,
        L.Account,
        L.TotalAmount,
        L.TotalFee,
        U.Email
    FROM LikeList L
    JOIN Product P ON L.Nom = P.Nom
    JOIN [User] U ON L.UserID = U.UserID
    WHERE L.UserID = @UserID
END
GO

-- 取得單筆喜好商品
CREATE PROCEDURE sp_GetLikeById
    @SN INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        L.SN,
        L.UserID,
        L.Nom,
        P.ProductName,
        P.Price,
        P.FeeRate,
        L.OrderQty,
        L.Account,
        L.TotalAmount,
        L.TotalFee,
        U.Email
    FROM LikeList L
    JOIN Product P ON L.Nom = P.Nom
    JOIN [User] U ON L.UserID = U.UserID
    WHERE L.SN = @SN
END
GO

-- 修改喜好商品
CREATE PROCEDURE sp_UpdateLikeProduct
    @SN INT,
    @Nom INT,
    @OrderQty INT,
    @Account NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Price DECIMAL(18,2)
    DECLARE @FeeRate DECIMAL(5,2)

    SELECT @Price = Price, @FeeRate = FeeRate
    FROM Product
    WHERE Nom = @Nom

    DECLARE @TotalAmount DECIMAL(18,2) = @Price * @OrderQty
    DECLARE @TotalFee DECIMAL(18,2) = @TotalAmount * @FeeRate / 100

    UPDATE LikeList
    SET Nom = @Nom,
        OrderQty = @OrderQty,
        Account = @Account,
        TotalAmount = @TotalAmount,
        TotalFee = @TotalFee
    WHERE SN = @SN
END
GO

-- 取得使用者資料
CREATE PROCEDURE sp_GetUserById
    @UserID NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [User]
    WHERE UserID = @UserID
END
GO

-- 所有產品下拉選單
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Product
END
GO

-- 刪除
CREATE PROCEDURE sp_DeleteLike
    @SN INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM LikeList
    WHERE SN = @SN
END
GO
