CREATE PROCEDURE GetUserByCNP
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM Users WHERE CNP = @UserCNP;
END;

GO
CREATE OR ALTER PROCEDURE UpdateUserCreditScore
    @UserCNP VARCHAR(16),
    @NewCreditScore INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET CreditScore = @NewCreditScore
    WHERE CNP = @UserCNP;
END;
GO

