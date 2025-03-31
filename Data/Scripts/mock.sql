INSERT INTO Users (CNP, FirstName, LastName, Email, PhoneNumber, HashedPassword, NoOffenses, RiskScore, ROI, CreditScore, Birthday, ZodiacSign, ZodiacAttribute, NoOfBillSharesPaid, Income)
VALUES 

('5040203070016', 'John', 'Doe', 'john.doe@email.com', '0712345678', 'hash123', 0, 20, 0.1, 650, '1985-07-15', 'Cancer', 'Courage', 0, 2100),
('6050711030316', 'Jane', 'Smith', 'jane.smith@email.com', '0723456789', 'hash234', 1, 35, 3.2, 680, '1990-03-21', 'Aries', 'Patience', 1, 4200),
('5050706001777', 'Robert', 'Johnson', 'robert.j@email.com', '0734567890', 'hash345', 2, 50, 0.7, 220, '1982-11-05', 'Scorpio', 'Adaptability', 5, 2200),
('4567890123456', 'Emily', 'Williams', 'emily.w@email.com', '0745678901', 'hash456', 0, 15, 0.5, 390, '1988-05-12', 'Taurus', 'Empathy', 1, 9500),
('5678901234567', 'Michael', 'Brown', 'michael.b@email.com', '0756789012', 'hash567', 3, 65, 2.4, 180, '1979-01-30', 'Aquarius', 'Generosity', 3, 4200),
('6789012345678', 'Sarah', 'Davis', 'sarah.d@email.com', '0767890123', 'hash678', 5, 30, 5.10, 210, '1992-09-18', 'Virgo', 'Perfectionist', 2, 6800),
('7890123456789', 'David', 'Miller', 'david.m@email.com', '0778901234', 'hash789', 0, 25, 5.50, 340, '1986-12-03', 'Sagittarius', 'Balance', 3, 8600),
('8901234567890', 'Lisa', 'Wilson', 'lisa.w@email.com', '0789012345', 'hash890', 2, 45, 3.80, 450, '1984-08-27', 'Virgo', 'Passion', 0, 9500),
('9012345678901', 'Mark', 'Taylor', 'mark.t@email.com', '0790123456', 'hash901', 0, 18, 5.90, 160, '1991-04-09', 'Aries', 'Optimism', 4, 9400),
('0123456789012', 'Jennifer', 'Anderson', 'jennifer.a@email.com', '0701234567', 'hash012', 4, 70, 2.30, 560, '1977-06-22', 'Cancer', 'Ambition', 2, 7000),
('1122334455667', 'Thomas', 'Wilson', 'thomas.w@email.com', '0711223344', 'hash111', 1, 28, 4.90, 700, '1983-08-22', 'Leo', 'Originality', 0, 5000)


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
('5040203070016', 1, NULL, '2023-01-15'),
('5040203070016', 2, NULL, '2023-02-10'),
('6050711030316', 3, NULL, '2023-03-05'),
('6050711030316', 5, NULL, '2023-03-25'),
('5050706001777', 7, NULL, '2023-04-10'),
('4567890123456', NULL, 1, '2023-04-18'),
('5678901234567', NULL, 2, '2023-05-22'),
('6789012345678', NULL, 3, '2023-06-14'),
('7890123456789', NULL, 4, '2023-07-01'),
('8901234567890', NULL, 5, '2023-07-15'),
('9012345678901', NULL, 6, '2023-08-03');


INSERT INTO LoanRequest (UserCNP, Amount, ApplicationDate, RepaymentDate, State) 
VALUES 
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Solved'),
('5010228345678', 1000.00, '2024-02-15', '2027-02-15', 'Solved'),
('1801231123459', 3000.00, '2024-04-05', '2025-04-05', 'Solved'),
('5050301123458', 1000.00, '2024-08-30', '2027-08-30', 'Solved'),
('5011231156789', 1000.00, '2024-10-22', '2029-10-22', 'Solved'),
('1080718229876', 6000.00, '2024-11-05', '2026-11-05', 'Solved'),
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Solved'),
('7050415109876', 1000.00, '2024-02-15', '2027-02-15', 'Solved'),
('1080718229876', 3000.00, '2024-04-05', '2025-04-05', 'Solved'),
('5121119223456', 1000.00, '2024-08-30', '2027-08-30', 'Solved'),
('3100930128765', 1000.00, '2024-10-22', '2029-10-22', 'Unsolved'),
('5011231156789', 6000.00, '2024-11-05', '2026-11-05', 'Unsolved'),
('5121119223456', 9900.00, '2024-11-05', '2026-11-05', 'Unsolved'),
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Solved'),
('1801231123459', 1000.00, '2024-02-15', '2027-02-15', 'Unsolved'),
('7050415109876', 3000.00, '2024-04-05', '2025-04-05', 'Solved'),
('5040203070016', 1000.00, '2024-05-12', '2028-05-12', 'Unsolved'),
('5040203070016', 7000.00, '2024-06-18', '2026-06-18', 'Unsolved'),
('1080718229876', 9500.00, '2024-07-25', '2026-07-25', 'Unsolved'),
('3100930128765', 1000.00, '2024-08-30', '2027-08-30', 'Unsolved'),
('5040203070016', 4000.00, '2024-09-12', '2025-09-12', 'Unsolved'),
('5040203070016', 1000.00, '2024-10-22', '2029-10-22', 'Unsolved'),
('7050415109876', 6000.00, '2024-11-05', '2026-11-05', 'Unsolved');

INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('9012345678901', 'im going to the gym')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('6789012345678', 'youre a dic')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('9012345678901', 'I am not paying you back bro get lost')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('9012345678901', 'where u at bro we need to talk')
INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('6789012345678', 'fuck')



INSERT INTO CreditScoreHistory VALUES
('5040203070016', '2023-01-15', 450),
('5040203070016', '2023-02-10', 500),
('5040203070016', '2023-03-22', 550),
('5040203070016', '2023-04-18', 600),
('5040203070016', '2023-05-30', 650),
('5040203070016', '2023-06-15', 700),
('5040203070016', '2023-07-09', 100),
('5040203070016', '2023-08-14', 150),
('5040203070016', '2023-09-20', 200),
('5040203070016', '2023-10-25', 250),
('5040203070016', '2023-11-05', 300),
('5040203070016', '2023-12-12', 350),
('5040203070016', '2024-01-08', 400),
('5040203070016', '2024-02-14', 450),
('5040203070016', '2024-03-19', 500),
('5040203070016', '2024-04-22', 550),
('5040203070016', '2024-05-10', 600),
('5040203070016', '2024-06-15', 650),
('5040203070016', '2024-07-21', 700),
('5040203070016', '2024-08-30', 120),
('6050711030316', '2023-01-20', 130),
('6050711030316', '2023-02-15', 180),
('6050711030316', '2023-03-12', 230),
('6050711030316', '2023-04-18', 280),
('6050711030316', '2023-05-22', 330),
('6050711030316', '2023-06-29', 380),
('6050711030316', '2023-07-05', 430),
('6050711030316', '2023-08-10', 480),
('6050711030316', '2023-09-15', 530),
('6050711030316', '2023-10-20', 580),
('6050711030316', '2023-11-25', 630),
('6050711030316', '2023-12-30', 680),
('6050711030316', '2024-01-10', 700),
('6050711030316', '2024-02-18', 120),
('6050711030316', '2024-03-22', 170),
('6050711030316', '2024-04-28', 220),
('6050711030316', '2024-05-30', 270),
('6050711030316', '2024-06-12', 320),
('6050711030316', '2024-07-15', 370),
('6050711030316', '2024-08-25', 420),
('5050706001777', '2023-01-10', 410),
('5050706001777', '2023-02-15', 480),
('5050706001777', '2023-03-22', 550),
('5050706001777', '2023-04-18', 620),
('5050706001777', '2023-05-30', 690),
('5050706001777', '2023-06-15', 700),
('5050706001777', '2023-07-09', 140),
('5050706001777', '2023-08-14', 160),
('5050706001777', '2023-09-20', 210),
('5050706001777', '2023-10-25', 270),
('5050706001777', '2023-11-05', 310),
('5050706001777', '2023-12-12', 370),
('5050706001777', '2024-01-08', 420),
('5050706001777', '2024-02-14', 460),
('5050706001777', '2024-03-19', 510),
('5050706001777', '2024-04-22', 570),
('5050706001777', '2024-05-10', 610),
('5050706001777', '2024-06-15', 660),
('5050706001777', '2024-07-21', 700),
('5050706001777', '2024-08-30', 130),
('4567890123456', '2023-01-10', 400),
('4567890123456', '2023-02-15', 480),
('4567890123456', '2023-03-22', 550),
('5678901234567', '2023-01-10', 410),
('5678901234567', '2023-02-15', 500),
('5678901234567', '2023-03-22', 600),
('6789012345678', '2023-01-10', 390),
('6789012345678', '2023-02-15', 470),
('6789012345678', '2023-03-22', 520),
('7890123456789', '2023-01-10', 460),
('7890123456789', '2023-02-15', 530),
('7890123456789', '2023-03-22', 580),
('8901234567890', '2023-01-10', 420),
('8901234567890', '2023-02-15', 490),
('8901234567890', '2023-03-22', 550),
('9012345678901', '2023-01-10', 410),
('9012345678901', '2023-02-15', 480),
('9012345678901', '2023-03-22', 570),
('0123456789012', '2023-01-10', 400),
('0123456789012', '2023-02-15', 470),
('0123456789012', '2023-03-22', 560),
('1122334455667', '2023-01-10', 450),
('1122334455667', '2023-02-15', 520),
('1122334455667', '2023-03-22', 600);


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

INSERT INTO ActivityLog (Name, UserCNP, LastModifiedAmount, Details) VALUES
('Bill splitting', '5040203070016', -250, 'Paid for dinner with friends'),
('Investments', '5040203070016', 500, 'Profited from stock trading'),
('Loans', '5040203070016', -600, 'Loan repayment deducted'),
('Chart', '6050711030316', 200, 'Gained returns on mutual funds'),
('Bill splitting', '6050711030316', -100, 'Paid electricity bill'),
('Loans', '6050711030316', -400, 'EMI deducted for car loan'),
('Investments', '5050706001777', 300, 'Interest credited on fixed deposit'),
('Chart', '5050706001777', 150, 'Analyzed financial growth'),
('Bill splitting', '5050706001777', -350, 'Paid internet bill'),
('Loans', '4567890123456', -700, 'Mortgage payment processed'),
('Investments', '4567890123456', 400, 'Dividend received from stock holdings'),
('Chart', '4567890123456', 120, 'Financial insights reviewed'),
('Bill splitting', '5678901234567', -200, 'Paid for a shared taxi ride'),
('Investments', '5678901234567', 600, 'Crypto investment appreciated'),
('Loans', '5678901234567', -500, 'Credit card bill auto-debited'),
('Chart', '6789012345678', 250, 'Budget analysis completed'),
('Bill splitting', '6789012345678', -300, 'Paid for office party'),
('Loans', '6789012345678', -150, 'Personal loan installment deducted'),
('Investments', '7890123456789', 700, 'High returns from real estate'),
('Bill splitting', '7890123456789', -400, 'Shared grocery expenses'),
('Chart', '7890123456789', 350, 'Reviewed monthly financial report'),
('Loans', '8901234567890', -500, 'Auto-loan deduction from account'),
('Investments', '8901234567890', 300, 'Profit from short-term trading'),
('Chart', '8901234567890', 200, 'Analyzed expense trends'),
('Bill splitting', '9012345678901', -450, 'Paid for a group trip'),
('Investments', '9012345678901', 500, 'Stock market gain recorded'),
('Loans', '9012345678901', -700, 'Debt repayment made'),
('Chart', '0123456789012', 600, 'Quarterly financial review'),
('Bill splitting', '0123456789012', -350, 'Paid for a subscription service'),
('Loans', '0123456789012', -250, 'Education loan EMI paid'),
('Investments', '1122334455667', 400, 'Interest earned on savings'),
('Bill splitting', '1122334455667', -100, 'Paid for concert tickets'),
('Chart', '1122334455667', 300, 'Reviewed yearly financial summary');

