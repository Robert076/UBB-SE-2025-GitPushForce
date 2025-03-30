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

CREATE PROCEDURE GetInvestmentsHistory
AS
BEGIN
    SELECT ID, InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate
    FROM Investments
END

CREATE PROCEDURE AddInvestment
@InvestorCNP VARCHAR(16),
@Details VARCHAR(255),
@AmountInvested DECIMAL(6, 2),
@AmountReturned DECIMAL(6, 2),
@InvestmentDate DATE
AS
BEGIN
    INSERT INTO Investments (InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate)
    VALUES (@InvestorCNP, @Details, @AmountInvested, @AmountReturned, @InvestmentDate)
END

CREATE PROCEDURE CheckInvestmentStatus
@InvestmentId INT,
@InvestorCNP VARCHAR(16)
AS
BEGIN
    SELECT ID, InvestorCNP, AmountReturned
    FROM Investments
    WHERE ID = @InvestmentId AND InvestorCNP = @InvestorCNP
END

CREATE PROCEDURE UpdateInvestment
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
END