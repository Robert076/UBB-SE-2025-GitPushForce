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

insert into Tips(CreditScoreBracket, TipText)
values('Low-Credit', 'Pay your bills on time,Late payments can decrease your score'),
('Low-Credit','Credit cards aren�t free money, if you cannot afford something, dont put it on Credit hoping you will figure it out after'),
('Low-Credit','Avoid doing your past mistakes '),
('Low-Credit','if you can�t pay off a card, stop using it. Swiping more won�t fix your situation'),
('Low-Credit','Start small if you have no credit history.'),
('Medium-Credit','Set up automatic payments. One missed bill can set you back years '),
('Medium-Credit','Stop applying for new credit so often. Every application lowers your score a bit'),
('Medium-Credit','Don�t let unpaid debts sit for too long. Old debts still impact your credit, so pay them off.'),
('Medium-Credit','Pay off debts with the highest interest first. It saves you money and helps with your score'),
('Medium-Credit','Don�t apply for loans you don�t need.Every application slightly lowers your score'),
('High-Credit','Pay your full balance every month.'),
('High-Credit','Set up autopay for all bills. Even one missed payment can drop your score fast'),
('High-Credit','Avoid unnecessary hard inquiries. Only apply for new credit when you truly need it'),
('High-Credit','Use credit smartly. A mix of credit types(loans, credit cards) can boost your score'),
('High-Credit','Keep your credit card utilization low.Even if your limit is high, don�t use more than 10-20%')


insert into Messages(Type,Message)
values('Congrats-message','Your credit score just went up! Keep making smart moves, and you will unlock better opportunities.'),
('Congrats-message','You�re roving you know how to handle money :).Lenders trust you more, and so should you.'),
('Congrats-message','Great progress! A higher credit score means better deals, lower interest rates, and more financial freedom.'),
('Roast-message','Your credit score is lower than your phone battery at 2%.Charge it up before it�s too late!'),
('Roast-message','Even monopoly money has better credit than you right now. Get it together!'),
('Roast-message','Your credit score is so low that even a charity wouldn�t loan you a dollar!')

INSERT INTO GivenTips (UserCNP, TipID, MessageID, Date)
VALUES
('5050706001777', 1, 4, GETDATE()),
('5678901234567', 2, 5, GETDATE()),
('6789012345678', 3, 6, GETDATE()),
('9012345678901', 4, 4, GETDATE()),
('4567890123456', 5, 1, GETDATE()),
('7890123456789', 6, 2, GETDATE()),
('0123456789012', 7, 3, GETDATE()), 
('5040203070016', 8, 1, GETDATE()), 
('6050711030316', 9, 2, GETDATE()), 
('8901234567890', 10, 3, GETDATE()),
('1122334455667', 11, 1, GETDATE());

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

INSERT INTO Investments (InvestorCNP, Details, AmountInvested, AmountReturned, InvestmentDate)
VALUES 
-- John Doe (3 transactions)
('5040203070016', 'Investment in Apple Inc.', 1000.00, 1200.00, '2024-11-15'),
('5040203070016', 'Investment in Microsoft Corp.', 500.00, -1, '2024-12-01'),
('5040203070016', 'Investment in Amazon.com Inc.', 800.00, 900.00, '2025-01-05'),

-- Jane Smith (4 transactions)
('6050711030316', 'Investment in Alphabet Inc.', 2000.00, 2500.00, '2024-11-20'),
('6050711030316', 'Investment in Tesla Inc.', 1500.00, 1800.00, '2024-12-10'),
('6050711030316', 'Investment in Nvidia Corp.', 1200.00, -1, '2025-01-10'),
('6050711030316', 'Investment in Facebook Inc.', 1000.00, 1100.00, '2025-01-15'),

-- Robert Johnson (2 transactions)
('5050706001777', 'Investment in Intel Corp.', 3000.00, 3500.00, '2024-11-25'),
('5050706001777', 'Investment in Cisco Systems Inc.', 2500.00, 2800.00, '2024-12-15'),

-- Emily Williams (5 transactions)
('4567890123456', 'Investment in Johnson & Johnson', 4000.00, 4500.00, '2024-11-30'),
('4567890123456', 'Investment in Procter & Gamble Co.', 3500.00, 3800.00, '2024-12-20'),
('4567890123456', 'Investment in Coca-Cola Co.', 3000.00, 3200.00, '2025-01-20'),
('4567890123456', 'Investment in PepsiCo Inc.', 2500.00, 2800.00, '2025-01-25'),
('4567890123456', 'Investment in Unilever PLC', 2000.00, -1, '2025-02-01'),

-- Michael Brown (3 transactions)
('5678901234567', 'Investment in Exxon Mobil Corp.', 4500.00, 5000.00, '2024-12-01'),
('5678901234567', 'Investment in Chevron Corp.', 4000.00, 4200.00, '2024-12-25'),
('5678901234567', 'Investment in ConocoPhillips', 3500.00, -1, '2025-01-25'),

-- Sarah Davis (4 transactions)
('6789012345678', 'Investment in Walmart Inc.', 5000.00, 5500.00, '2024-12-05'),
('6789012345678', 'Investment in Home Depot Inc.', 4500.00, 4800.00, '2024-12-30'),
('6789012345678', 'Investment in Target Corp.', 4000.00, 4200.00, '2025-01-30'),
('6789012345678', 'Investment in Costco Wholesale Corp.', 3500.00, 3800.00, '2025-02-05'),

-- David Miller (2 transactions)
('7890123456789', 'Investment in Visa Inc.', 5500.00, 6000.00, '2024-12-10'),
('7890123456789', 'Investment in Mastercard Inc.', 5000.00, 5200.00, '2024-12-31'),

-- Lisa Wilson (5 transactions)
('8901234567890', 'Investment in UnitedHealth Group Inc.', 6000.00, 6500.00, '2024-12-15'),
('8901234567890', 'Investment in CVS Health Corp.', 5500.00, 5800.00, '2024-12-28'),
('8901234567890', 'Investment in Aetna Inc.', 5000.00, 5200.00, '2025-02-01'),
('8901234567890', 'Investment in Cigna Corp.', 4500.00, 4800.00, '2025-02-10'),
('8901234567890', 'Investment in Humana Inc.', 4000.00, -1, '2025-02-15'),

-- Mark Taylor (3 transactions)
('9012345678901', 'Investment in Salesforce.com Inc.', 6500.00, 7000.00, '2024-12-20'),
('9012345678901', 'Investment in Oracle Corp.', 6000.00, 6200.00, '2024-12-29'),
('9012345678901', 'Investment in SAP SE', 5500.00, -1, '2025-02-20'),

-- Jennifer Anderson (4 transactions)
('0123456789012', 'Investment in 3M Co.', 7000.00, 7500.00, '2024-12-25'),
('0123456789012', 'Investment in General Electric Co.', 6500.00, 6800.00, '2024-12-31'),
('0123456789012', 'Investment in Siemens AG', 6000.00, 6200.00, '2025-02-01'),
('0123456789012', 'Investment in Honeywell International Inc.', 5500.00, 5800.00, '2025-02-25'),

-- Thomas Wilson (2 transactions)
('1122334455667', 'Investment in Boeing Co.', 7500.00, 8000.00, '2024-12-28'),
('1122334455667', 'Investment in Lockheed Martin Corp.', 7000.00, 7200.00, '2024-12-30')
