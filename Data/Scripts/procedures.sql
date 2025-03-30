CREATE OR ALTER PROCEDURE GetUserByCNP
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM Users WHERE CNP = @UserCNP;
END;

CREATE OR ALTER PROCEDURE GetChatReports
AS
BEGIN
    SELECT * FROM ChatReports;
END;

CREATE OR ALTER PROCEDURE DeleteChatReportByGivenId
    @ChatReportId INT
AS
BEGIN
    DELETE FROM ChatReports
    WHERE Id = @ChatReportId;
END;

CREATE OR ALTER PROCEDURE LowerUserThatIsGivenByCNPHisCreditScoreWithGivenIntegerAmount
    @CNP VARCHAR(16),
    @Amount INT
AS
BEGIN
    UPDATE Users
    SET CreditScore = CreditScore - @Amount
    WHERE CNP = @CNP;
END;

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
CREATE OR ALTER PROCEDURE GetHistoryForUser	
	@UserCNP VARCHAR(16)
AS
BEGIN
	SELECT * FROM CreditScoreHistory WHERE userCNP = @UserCNP;
END;
go

CREATE OR ALTER PROCEDURE GetActivitiesForUser @UserCNP VARCHAR(16)
AS
BEGIN
	SELECT * FROM ActivityLog WHERE UserCNP = @UserCNP
END
Go

CREATE OR ALTER PROCEDURE GetUsers
AS
	SELECT * FROM Users
go

go
CREATE OR ALTER TRIGGER updateHistory ON dbo.Users
FOR UPDATE
AS
BEGIN
	DECLARE @count INT
	DECLARE @userCNP VARCHAR(16)
	DECLARE @score INT
	SELECT @userCNP = i.CNP, @score=i.CreditScore FROM INSERTED i

	SELECT @count = count(*)
	FROM CreditScoreHistory c
	WHERE c.Date = CAST(GETDATE() AS DATE) AND c.UserCNP = @userCNP
	print(@count)
	IF @count = 0
		INSERT INTO CreditScoreHistory VALUES (@userCNP, CAST(GETDATE() AS DATE), @score)
	ELSE
		UPDATE CreditScoreHistory SET Score=@score WHERE UserCNP=@userCNP AND Date=CAST(GETDATE() AS DATE)
END
GO
