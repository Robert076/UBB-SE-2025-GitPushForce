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
    WHERE ChatReportId = @ChatReportId;
END;

CREATE OR ALTER PROCEDURE InsertGivenTip
    @UserCNP VARCHAR(16),
    @TipID INT
AS
BEGIN
    INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date)
    VALUES (@UserCNP, @TipID, NULL, GETDATE());
END;

CREATE OR ALTER PROCEDURE GetLowCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'Low-credit';
END;

CREATE OR ALTER PROCEDURE GetMediumCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'Medium-credit';
END;

CREATE OR ALTER PROCEDURE GetHighCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'High-credit';
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

CREATE PROCEDURE IncrementNoOfOffensesBy1ForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    UPDATE Users
    SET NoOffenses = NoOffenses + 1
    WHERE CNP = @UserCNP;
END;


CREATE OR ALTER PROCEDURE GetRandomCongratsMessage
AS
BEGIN
    SELECT * 
    FROM Messages
    WHERE Type = 'Congrats-message' 
    ORDER BY NEWID() 
    OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY; 
END;


CREATE OR ALTER PROCEDURE InsertGivenMessage
    @UserCNP VARCHAR(16),
    @MessageID INT
AS
BEGIN
    INSERT INTO GivenTips (UserCNP, MessageID, Date)
    VALUES (@UserCNP, @MessageID, GETDATE());
END;

CREATE OR ALTER PROCEDURE GetRandomRoastMessage
AS
BEGIN
    SELECT * 
    FROM Messages
    WHERE Type = 'Roast-message'
    ORDER BY NEWID()
END;
