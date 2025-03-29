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

CREATE PROCEDURE InsertGivenTip
    @UserCNP VARCHAR(16),
    @TipID INT
AS
BEGIN
    INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date)
    VALUES (@UserCNP, @TipID, NULL, GETDATE());
END;

CREATE PROCEDURE GetLowCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'Low-credit';
END;

CREATE PROCEDURE GetMediumCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'Medium-credit';
END;

CREATE PROCEDURE GetHighCreditScoreTips
AS
BEGIN
    SELECT * 
    FROM Tips
    WHERE CreditScoreBracket = 'High-credit';
END;


