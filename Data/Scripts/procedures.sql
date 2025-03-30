CREATE OR ALTER PROCEDURE GetUserByCNP
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM Users WHERE CNP = @UserCNP;
END;

go
CREATE OR ALTER PROCEDURE GetChatReports
AS
BEGIN
    SELECT * FROM ChatReports;
END;

go
CREATE OR ALTER PROCEDURE DeleteChatReportByGivenId
    @ChatReportId INT
AS
BEGIN
    DELETE FROM ChatReports
    WHERE Id = @ChatReportId;
END;

go
CREATE OR ALTER PROCEDURE LowerUserThatIsGivenByCNPHisCreditScoreWithGivenIntegerAmount
    @CNP VARCHAR(16),
    @Amount INT
AS
BEGIN
    UPDATE Users
    SET CreditScore = CreditScore - @Amount
    WHERE CNP = @CNP;
END;

go
CREATE OR ALTER PROCEDURE UpdateCreditScoreHistory
    @UserCNP VARCHAR(16),
    @NewScore INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM CreditScoreHistory WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE))
    BEGIN
        UPDATE CreditScoreHistory
        SET Score = @NewScore
        WHERE UserCNP = @UserCNP AND Date = CAST(GETDATE() AS DATE);
    END
    ELSE
    BEGIN
        INSERT INTO CreditScoreHistory (UserCNP, Date, Score)
        VALUES (@UserCNP, CAST(GETDATE() AS DATE), @NewScore);
    END
END

go
CREATE PROCEDURE IncrementOffenses
    @UserCNP VARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET NoOffenses = ISNULL(NoOffenses, 0) + 1
    WHERE CNP = @UserCNP;
END;


go
CREATE PROCEDURE IncrementNoOfOffensesBy1ForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    UPDATE Users
    SET NoOffenses = NoOffenses + 1
    WHERE CNP = @UserCNP;
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

CREATE OR ALTER PROCEDURE GetUsers
AS
	SELECT * FROM Users
go