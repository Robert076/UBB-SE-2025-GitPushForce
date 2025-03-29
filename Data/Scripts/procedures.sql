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

-- bill split report procedures
CREATE OR ALTER PROCEDURE GetBillSplitReports
AS
BEGIN
    SELECT * FROM BillSplitReports;
END;

CREATE OR ALTER PROCEDURE DeleteBillSplitReportById
    @BillSplitReportId INT
AS
BEGIN
    DELETE FROM BillSplitReports
    WHERE Id = @BillSplitReportId;
END;

CREATE OR ALTER PROCEDURE CreateBillSplitReport
    @ReportedUserCNP VARCHAR(16),
    @ReporterUserCNP VARCHAR(16),
    @DateOfTransaction DATE,
    @BillShare FLOAT,
    @GravityFactor FLOAT
AS
BEGIN
    INSERT INTO BillSplitReports (ReportedUserCNP, ReporterUserCNP, DateOfTransaction, BillShare, GravityFactor)
    VALUES (@ReportedUserCNP, @ReporterUserCNP, @DateOfTransaction, @BillShare, @GravityFactor);
END;

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

CREATE OR ALTER PROCEDURE GetCurrentBalance
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT Balance FROM Users WHERE CNP = @ReportedUserCNP;
END;

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

CREATE OR ALTER PROCEDURE CheckHistoryOfBillShares
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT NoOfBillSharesPaid FROM Users WHERE CNP = @ReportedUserCNP;
END;

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

CREATE OR ALTER PROCEDURE GetNumberOfOffenses
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT NoOffenses FROM Users WHERE CNP = @ReportedUserCNP;
END;

CREATE OR ALTER PROCEDURE GetCurrentCreditScore
    @ReportedUserCNP VARCHAR(16)
AS
BEGIN
    SELECT CreditScore FROM Users WHERE CNP = @ReportedUserCNP;
END;

CREATE OR ALTER PROCEDURE UpdateCreditScore
    @UserCNP VARCHAR(16),
    @NewCreditScore INT
AS
BEGIN
    UPDATE Users
    SET CreditScore = @NewCreditScore
    WHERE CNP = @UserCNP;
END;

