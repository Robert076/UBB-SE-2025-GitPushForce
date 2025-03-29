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

INSERT INTO LoanRequest (UserCNP, Amount, ApplicationDate, RepaymentDate)
VALUES
('5040203070016', 5000.00, '2024-01-10', '2026-01-10'),
('6050711030316', 1000.00, '2024-02-15', '2027-02-15'),
('5050706001777', 8000.00, '2024-03-20', '2026-03-20'),
('4567890123456', 3000.00, '2024-04-05', '2025-04-05'),
('5678901234567', 1000.00, '2024-05-12', '2028-05-12'),
('6789012345678', 7000.00, '2024-06-18', '2026-06-18'),
('7890123456789', 9500.00, '2024-07-25', '2026-07-25'),
('8901234567890', 1000.00, '2024-08-30', '2027-08-30'),
('9012345678901', 4000.00, '2024-09-12', '2025-09-12'),
('0123456789012', 1000.00, '2024-10-22', '2029-10-22'),
('1122334455667', 6000.00, '2024-11-05', '2026-11-05');

INSERT INTO Loans (LoanRequestID, UserCNP, Amount, ApplicationDate, RepaymentDate, InterestRate, NoMonths, State, MonthlyPaymentAmount, MonthlyPaymentsCompleted, RepaidAmount, Penalty)
VALUES
(1, '5040203070016', 5000.00, '2024-01-10', '2026-01-10', 5.5, 24, 'Active', 220.50, 3, 661.50, 0.00),
(2, '6050711030316', 1000.00, '2024-02-15', '2027-02-15', 4.8, 36, 'Approved', 368.90, 0, 0.00, 0.00),
(3, '5050706001777', 8000.00, '2024-03-20', '2026-03-20', 6.2, 24, 'Active', 354.70, 6, 2128.20, 10.00),
(4, '4567890123456', 3000.00, '2024-04-05', '2025-04-05', 5.0, 12, 'Completed', 270.00, 12, 3240.00, 0.00),
(5, '5678901234567', 1000.00, '2024-05-12', '2028-05-12', 7.0, 48, 'Delayed', 372.00, 10, 3720.00, 50.00),
(6, '6789012345678', 7000.00, '2024-06-18', '2026-06-18', 4.5, 24, 'Active', 298.00, 4, 1192.00, 0.00),
(7, '7890123456789', 9500.00, '2024-07-25', '2026-07-25', 5.3, 24, 'Approved', 350.75, 0, 0.00, 0.00),
(8, '8901234567890', 1000.00, '2024-08-30', '2027-08-30', 5.9, 36, 'Active', 392.50, 2, 785.00, 0.00),
(9, '9012345678901', 4000.00, '2024-09-12', '2025-09-12', 4.2, 12, 'Pending', 345.00, 0, 0.00, 0.00),
(10, '0123456789012', 1000.00, '2024-10-22', '2029-10-22', 6.5, 60, 'Active', 350.00, 1, 350.00, 0.00),
(11, '1122334455667', 6000.00, '2024-11-05', '2026-11-05', 5.1, 24, 'Delayed', 275.00, 8, 2200.00, 25.00);

INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('5040203070016', 'youre a dic')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('6050711030316', 'I am not paying you back bro get lost')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('5678901234567', 'where u at bro we need to talk')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('0123456789012', 'im going to the gym')
