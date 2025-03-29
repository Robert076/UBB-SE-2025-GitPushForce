INSERT INTO Users (CNP, FirstName, LastName, Email, PhoneNumber, HashedPassword, NoOffenses, RiskScore, ROI, CreditScore, Birthday, ZodiacSign, ZodiacAttribute, NoOfBillSharesPaid)
VALUES 
('5040203070016', 'John', 'Doe', 'john.doe@email.com', '0712345678', 'hash123', 0, 20, 0.1, 650, '1985-07-15', 'Cancer', 'Courage', 0),
('6050711030316', 'Jane', 'Smith', 'jane.smith@email.com', '0723456789', 'hash234', 1, 35, 3.2, 680, '1990-03-21', 'Aries', 'Patience', 1),
('5050706001777', 'Robert', 'Johnson', 'robert.j@email.com', '0734567890', 'hash345', 2, 50, 0.7, 220, '1982-11-05', 'Scorpio', 'Adaptability', 5),
('4567890123456', 'Emily', 'Williams', 'emily.w@email.com', '0745678901', 'hash456', 0, 15, 0.5, 390, '1988-05-12', 'Taurus', 'Empathy', 1),
('5678901234567', 'Michael', 'Brown', 'michael.b@email.com', '0756789012', 'hash567', 3, 65, 2.4, 180, '1979-01-30', 'Aquarius', 'Generosity', 3),
('6789012345678', 'Sarah', 'Davis', 'sarah.d@email.com', '0767890123', 'hash678', 5, 30, 5.10, 210, '1992-09-18', 'Virgo', 'Perfectionist', 2),
('7890123456789', 'David', 'Miller', 'david.m@email.com', '0778901234', 'hash789', 0, 25, 5.50, 340, '1986-12-03', 'Sagittarius', 'Balance', 3),
('8901234567890', 'Lisa', 'Wilson', 'lisa.w@email.com', '0789012345', 'hash890', 2, 45, 3.80, 450, '1984-08-27', 'Virgo', 'Passion', 0),
('9012345678901', 'Mark', 'Taylor', 'mark.t@email.com', '0790123456', 'hash901', 0, 18, 5.90, 160, '1991-04-09', 'Aries', 'Optimism', 4),
('0123456789012', 'Jennifer', 'Anderson', 'jennifer.a@email.com', '0701234567', 'hash012', 4, 70, 2.30, 560, '1977-06-22', 'Cancer', 'Ambition', 2),
('1122334455667', 'Thomas', 'Wilson', 'thomas.w@email.com', '0711223344', 'hash111', 1, 28, 4.90, 700, '1983-08-22', 'Leo', 'Originality', 0)

	INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('5040203070016', 'youre a dic')
	INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('6050711030316', 'I am not paying you back bro get lost')
	INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('5678901234567', 'where u at bro we need to talk')
	INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('0123456789012', 'im going to the gym')