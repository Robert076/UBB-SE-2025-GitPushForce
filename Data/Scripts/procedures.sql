CREATE OR ALTER PROCEDURE GetUserByCNP
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM Users WHERE CNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetChatReports
AS
BEGIN
    SELECT * FROM ChatReports;
END;
GO

CREATE OR ALTER PROCEDURE DeleteChatReportByGivenId
    @ChatReportId INT
AS
BEGIN
    DELETE FROM ChatReports
    WHERE ChatReportId = @ChatReportId;
END;
GO

CREATE OR ALTER PROCEDURE InsertGivenTip
    @UserCNP VARCHAR(16),
    @TipID INT
AS
BEGIN
    INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date)
    VALUES (@UserCNP, @TipID, NULL, GETDATE());
END;
GO

CREATE OR ALTER PROCEDURE GetLowCreditScoreTips
AS
BEGIN
    SELECT * FROM Tips WHERE CreditScoreBracket = 'Low-credit';
END;
GO

CREATE OR ALTER PROCEDURE GetMediumCreditScoreTips
AS
BEGIN
    SELECT * FROM Tips WHERE CreditScoreBracket = 'Medium-credit';
END;
GO

CREATE OR ALTER PROCEDURE GetHighCreditScoreTips
AS
BEGIN
    SELECT * FROM Tips WHERE CreditScoreBracket = 'High-credit';
END;
GO

CREATE OR ALTER PROCEDURE LowerUserCreditScore
    @CNP VARCHAR(16),
    @Amount INT
AS
BEGIN
    UPDATE Users
    SET CreditScore = CreditScore - @Amount
    WHERE CNP = @CNP;
END;
GO

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
    END;
END;
GO

CREATE OR ALTER PROCEDURE IncrementOffenses
    @UserCNP VARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET NoOffenses = ISNULL(NoOffenses, 0) + 1
    WHERE CNP = @UserCNP;
END;

CREATE OR ALTER PROCEDURE GetInvestmentsHistory
AS
BEGIN
    SELECT ID, InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate
    FROM Investments
END

CREATE OR ALTER PROCEDURE AddInvestment
@InvestorCNP VARCHAR(16),
@Details VARCHAR(255),
@AmountInvested DECIMAL(6, 2),
@AmountReturned DECIMAL(6, 2),
@InvestmentDate DATE
AS
BEGIN
    INSERT INTO Investments (InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate)
    VALUES (@InvestorCNP, @Details, @AmountInvested, @AmountReturned, @InvestmentDate)
END;
GO

CREATE OR ALTER PROCEDURE CheckInvestmentStatus
@InvestmentId INT,
@InvestorCNP VARCHAR(16)
AS
BEGIN
    SELECT ID, InvestorCNP, AmountReturned
    FROM Investments
    WHERE ID = @InvestmentId AND InvestorCNP = @InvestorCNP
END;
GO

CREATE OR ALTER PROCEDURE UpdateInvestment
@InvestmentId INT,
@AmountReturned DECIMAL(6, 2)
AS
BEGIN
    UPDATE Investments
    SET AmountReturned = @AmountReturned
    WHERE ID = @InvestmentId AND AmountReturned = -1

    -- Check if any rows were affected (optional validation)
    IF @@ROWCOUNT = 0 
    BEGIN 
        THROW 50001, 'This transaction was already finished or does not exist.', 1;
    END 
END;
GO

CREATE OR ALTER PROCEDURE GetLoans
AS
BEGIN
    SELECT * FROM Loans;
END;
GO

CREATE OR ALTER PROCEDURE GetLoanRequests
AS
BEGIN
    SELECT * FROM LoanRequest;
END;
GO

CREATE OR ALTER PROCEDURE GetLoansByUserCNP
    @UserCNP VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LoanRequestID,
        UserCNP,
        Amount,
        ApplicationDate,
        RepaymentDate,
        InterestRate,
        NoMonths,
        MonthlyPaymentAmount
    FROM Loans
    WHERE UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetHistoryForUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM CreditScoreHistory WHERE UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetActivitiesForUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT * FROM ActivityLog WHERE UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetUsers
AS
BEGIN
    SELECT * FROM Users;
END;
GO

CREATE OR ALTER TRIGGER updateHistory ON dbo.Users
FOR UPDATE
AS
BEGIN
    DECLARE @count INT;
    DECLARE @userCNP VARCHAR(16);
    DECLARE @score INT;

    SELECT @userCNP = i.CNP, @score = i.CreditScore FROM INSERTED i;

    SELECT @count = COUNT(*)
    FROM CreditScoreHistory c
    WHERE c.Date = CAST(GETDATE() AS DATE) AND c.UserCNP = @userCNP;

    IF @count = 0
    BEGIN
        INSERT INTO CreditScoreHistory (UserCNP, Date, Score)
        VALUES (@userCNP, CAST(GETDATE() AS DATE), @score);
    END
    ELSE
    BEGIN
        UPDATE CreditScoreHistory
        SET Score = @score
        WHERE UserCNP = @userCNP AND Date = CAST(GETDATE() AS DATE);
    END;
END;
GO

CREATE OR ALTER PROCEDURE IncrementNoOfOffensesBy1ForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    UPDATE Users
    SET NoOffenses = NoOffenses + 1
    WHERE CNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetRandomCongratsMessage
AS
BEGIN
    SELECT * 
    FROM Messages
    WHERE Type = 'Congrats-message'
    ORDER BY NEWID()
    OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
END;
GO

CREATE OR ALTER PROCEDURE InsertGivenMessage
    @UserCNP VARCHAR(16),
    @MessageID INT
AS
BEGIN
    INSERT INTO GivenTips (UserCNP, MessageID, Date)
    VALUES (@UserCNP, @MessageID, GETDATE());
END;
GO

CREATE OR ALTER PROCEDURE GetRandomRoastMessage
AS
BEGIN
    SELECT * 
    FROM Messages
    WHERE Type = 'Roast-message'
    ORDER BY NEWID();
END;
GO

CREATE OR ALTER PROCEDURE GetMessagesForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT m.ID, m.Type, m.Message
    FROM GivenTips gt
    INNER JOIN Messages m ON gt.MessageID = m.ID
    WHERE gt.UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetTipsForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    SELECT T.ID, T.CreditScoreBracket, T.TipText, GT.Date
    FROM GivenTips GT
    INNER JOIN Tips T ON GT.TipID = T.ID
    WHERE GT.UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetNumberOfGivenTipsForUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS NumberOfTips
    FROM GivenTips
    WHERE UserCNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE UpdateUserCreditScore
    @UserCNP VARCHAR(16),
    @NewCreditScore INT
AS
BEGIN
    UPDATE Users
    SET CreditScore = @NewCreditScore
    WHERE CNP = @UserCNP;
END;
GO