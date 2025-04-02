USE GitPushForce
GO
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
    WHERE ID = @ChatReportId;
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
GO

-- bill split report procedures
go
CREATE OR ALTER PROCEDURE IncrementNoOfBillSharesPaidForGivenUser
    @UserCNP VARCHAR(16)
AS
BEGIN
    UPDATE Users
    SET NoOfBillSharesPaid = NoOfBillSharesPaid + 1
    WHERE CNP = @UserCNP;
END;


go
CREATE OR ALTER PROCEDURE GetBillSplitReports
AS
BEGIN
    SELECT * FROM BillSplitReports;
END;

go
CREATE OR ALTER PROCEDURE DeleteBillSplitReportById
    @BillSplitReportId INT
AS
BEGIN
    DELETE FROM BillSplitReports
    WHERE Id = @BillSplitReportId;
END;

go
CREATE OR ALTER PROCEDURE CreateBillSplitReport
    @ReportedUserCNP VARCHAR(16),
    @ReporterUserCNP VARCHAR(16),
    @DateOfTransaction DATE,
    @BillShare FLOAT
AS
BEGIN
    INSERT INTO BillSplitReports (ReportedUserCNP, ReporterUserCNP, DateOfTransaction, BillShare)
    VALUES (@ReportedUserCNP, @ReporterUserCNP, @DateOfTransaction, @BillShare);
END;

go
CREATE OR ALTER PROCEDURE CheckLogsForSimilarPayments
    @ReportedUserCNP VARCHAR(16),
    @ReporterUserCNP VARCHAR(16),
    @DateOfTransaction DATE,
    @BillShare FLOAT
AS
BEGIN
    SELECT COUNT(*)
    FROM TransactionLogs
    WHERE SenderCNP = @ReportedUserCNP
      AND ReceiverCNP = @ReporterUserCNP
      AND TransactionDate > @DateOfTransaction
      AND Amount = @BillShare
      AND (TransactionDescription LIKE '%bill%' OR TransactionDescription LIKE '%share%' OR TransactionDescription LIKE '%split%')
      AND TransactionType != 'Bill Split';
END;

go
CREATE OR ALTER PROCEDURE GetCurrentBalance
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT Balance FROM Users WHERE CNP = @ReportedUserCNP;
END;

go
CREATE OR ALTER PROCEDURE SumTransactionsSinceReport
    @ReportedUserCNP VARCHAR(16),
    @DateOfTransaction DATE
AS
BEGIN
    SELECT SUM(Amount)
    FROM TransactionLogs
    WHERE SenderCNP = @ReportedUserCNP
      AND TransactionDate > @DateOfTransaction;
END;

go
CREATE OR ALTER PROCEDURE CheckHistoryOfBillShares
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT NoOfBillSharesPaid FROM Users WHERE CNP = @ReportedUserCNP;
END;

go
CREATE OR ALTER PROCEDURE CheckFrequentTransfers
    @ReportedUserCNP VARCHAR(16),
    @ReporterUserCNP VARCHAR(16)
AS
BEGIN
    SELECT COUNT(*)
    FROM TransactionLogs
    WHERE SenderCNP = @ReportedUserCNP
      AND ReceiverCNP = @ReporterUserCNP
      AND TransactionDate >= DATEADD(month, -1, GETDATE());
END;

go
CREATE OR ALTER PROCEDURE GetNumberOfOffenses
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT NoOffenses FROM Users WHERE CNP = @ReportedUserCNP;
END;

go
CREATE OR ALTER PROCEDURE GetCurrentCreditScore
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT CreditScore FROM Users WHERE CNP = @ReportedUserCNP;
END;

go
CREATE OR ALTER PROCEDURE DeleteLoanRequest
@LoanRequestID INT
AS
BEGIN
    DELETE FROM LoanRequest
    WHERE ID = @LoanRequestID;
END;
GO

CREATE OR ALTER PROCEDURE GetInvestmentsHistory
AS
BEGIN
    SELECT ID, InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate
    FROM Investments
END;
GO

go
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
END;
GO

CREATE OR ALTER PROCEDURE GetLoans
AS
BEGIN
    SELECT * FROM Loans;
END;
GO

go
CREATE OR ALTER PROCEDURE GetLoanRequests
AS
BEGIN
    SELECT * FROM LoanRequest;
END;
GO

go
CREATE PROCEDURE GetLoansByUserCNP
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

CREATE OR ALTER PROCEDURE UpdateActivityLog
	@UserCNP VARCHAR(16),
	@ActivityName VARCHAR(16),
	@Amount INT,
	@Details VARCHAR(100)
AS
BEGIN
	DECLARE @count INT;

	SELECT @count = COUNT(*)
    FROM ActivityLog a
    WHERE a.UserCNP = @userCNP and a.Name = @ActivityName;

	IF @count = 0
    BEGIN
        INSERT INTO ActivityLog (Name, UserCNP, LastModifiedAmount, Details)
        VALUES (@ActivityName, @userCNP, @Amount, @Details);
    END
    ELSE
    BEGIN
        UPDATE ActivityLog
        SET LastModifiedAmount = @Amount,
		Details = @Details
        WHERE UserCNP = @userCNP AND Name = @ActivityName;
    END;
END
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
    SET NOCOUNT ON;

    UPDATE Users
    SET CreditScore = @NewCreditScore
    WHERE CNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE UpdateUserROI
    @UserCNP VARCHAR(16),
    @NewROI DECIMAL(6, 2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET ROI = @NewROI
    WHERE CNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE UpdateUserRiskScore
    @UserCNP VARCHAR(16),
    @NewRiskScore INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET RiskScore = @NewRiskScore
    WHERE CNP = @UserCNP;
END;
GO

CREATE OR ALTER PROCEDURE GetUsers
AS
	SELECT * FROM Users
go

CREATE OR ALTER PROCEDURE AddLoan
    @LoanRequestID INT,
    @UserCNP VARCHAR(13),
    @Amount DECIMAL(10,2),
    @ApplicationDate DATE,
    @RepaymentDate DATE,
    @InterestRate DECIMAL(5,2),
    @NoMonths INT,
    @State VARCHAR(20),
    @MonthlyPaymentAmount DECIMAL(10,2),
    @MonthlyPaymentsCompleted INT,
    @RepaidAmount DECIMAL(10,2),
    @Penalty DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Loans (LoanRequestID, UserCNP, Amount, ApplicationDate, RepaymentDate, InterestRate, 
                       NoMonths, State, MonthlyPaymentAmount, MonthlyPaymentsCompleted, RepaidAmount, Penalty)
    VALUES (@LoanRequestID, @UserCNP, @Amount, @ApplicationDate, @RepaymentDate, @InterestRate, 
            @NoMonths, @State, @MonthlyPaymentAmount, @MonthlyPaymentsCompleted, @RepaidAmount, @Penalty);
END;
GO

CREATE OR ALTER PROCEDURE GetUnsolvedLoanRequests
AS
BEGIN
    SELECT *
    FROM LoanRequest
    WHERE LoanRequest.State <> 'Solved' OR LoanRequest.State IS NULL;
END;
GO

CREATE OR ALTER PROCEDURE MarkRequestAsSolved
@LoanRequestID INT
AS
BEGIN
UPDATE LoanRequest
SET State = 'Solved'
WHERE ID = @LoanRequestID;
END;
GO

CREATE PROCEDURE UpdateLoan
    @LoanRequestID INT,
    @UserCNP NVARCHAR(50),
    @Amount FLOAT,
    @ApplicationDate DATETIME,
    @RepaymentDate DATETIME,
    @InterestRate FLOAT,
    @NoMonths INT,
    @State NVARCHAR(50),
    @MonthlyPaymentAmount FLOAT,
    @MonthlyPaymentsCompleted INT,
    @RepaidAmount FLOAT,
    @Penalty FLOAT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Loans
    SET 
        UserCNP = @UserCNP,
        Amount = @Amount,
        ApplicationDate = @ApplicationDate,
        RepaymentDate = @RepaymentDate,
        InterestRate = @InterestRate,
        NoMonths = @NoMonths,
        State = @State,
        MonthlyPaymentAmount = @MonthlyPaymentAmount,
        MonthlyPaymentsCompleted = @MonthlyPaymentsCompleted,
        RepaidAmount = @RepaidAmount,
        Penalty = @Penalty
    WHERE LoanRequestID = @LoanRequestID;
END;
GO

CREATE OR ALTER PROCEDURE DeleteLoan
    @LoanRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Loans
    WHERE LoanRequestID = @LoanRequestID;
END;
GO

CREATE OR ALTER PROCEDURE GetLoanById
    @LoanRequestID INT
    AS
    BEGIN
        SELECT *
        FROM Loans
        WHERE LoanRequestID = @LoanRequestID;
    END;
    GO
