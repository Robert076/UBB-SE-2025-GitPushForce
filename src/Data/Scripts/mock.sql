USE GitPushForce
GO

INSERT INTO Users (Cnp, FirstName, LastName, Email, PhoneNumber, HashedPassword, NumberOfOffenses, RiskScore, ROI, CreditScore, Birthday, ZodiacSign, ZodiacAttribute, NumberOfBillSharesPaid, Income, Balance)
VALUES 
('5040203070016', 'John', 'Doe', 'john.doe@email.com', '0712345678', 'hash123', 0, 20, 0.1, 650, '1985-07-15', 'Cancer', 'Courage', 0, 2100, 1000.00),
('5010228345678', 'Michael', 'Brown', 'michael.b@email.com', '0756789012', 'hash567', 3, 65, 2.4, 180, '1979-01-30', 'Aquarius', 'Generosity', 3, 4200, 2000.00),
('1801231123459', 'David', 'Miller', 'david.m@email.com', '0778901234', 'hash789', 0, 25, 5.50, 340, '1986-12-03', 'Sagittarius', 'Balance', 3, 8600, 3000.00),
('5050301123458', 'Thomas', 'Wilson', 'thomas.w@email.com', '0711223344', 'hash111', 1, 28, 4.90, 700, '1983-08-22', 'Leo', 'Originality', 0, 5000, 2000.00),
('5011231156789', 'Mason', 'Carter', 'mason.carter@email.com', '0755432109', 'hash098', 3, 55, 3.1, 200, '1978-12-31', 'Capricorn', 'Discipline', 4, 4800, 4000.00),
('7050415109876', 'Isabella', 'Mitchell', 'isabella.mitchell@email.com', '0723344556', 'hash765', 0, 20, 1.1, 680, '1995-04-15', 'Aries', 'Passion', 2, 2900, 5000.00),
('1080718229876', 'Daniel', 'Harris', 'daniel.harris@email.com', '0756677889', 'hash098', 0, 12, 0.9, 630, '1996-07-18', 'Cancer', 'Loyalty', 0, 8500, 2340.00),
('3100930128765', 'Michael', 'Nelson', 'michael.nelson@email.com', '0778899001', 'hash210', 2, 44, 3.4, 450, '1985-09-30', 'Libra', 'Harmony', 1, 7200, 5460.00),
('5121119223456', 'Benjamin', 'Martinez', 'benjamin.martinez@email.com', '0791011123', 'hash432', 1, 39, 2.1, 690, '1984-11-19', 'Scorpio', 'Resilience', 2, 4500, 4350.00);


INSERT INTO ChatReports(ReportedUserCnp, ReportedMessage) 
VALUES 
--John Doe (3 reports)
('5040203070016', 'im going to the gym'),
('5040203070016', 'I am not paying you back bro get lost'),
('5040203070016', 'where u at bro we need to talk'),
--Michael Brown
('5010228345678', 'youre a dic'),
('5010228345678', 'fuck'),
-- Isabella Mitchell (2 report)
('7050415109876', 'I hate you!'),
('7050415109876', 'Have a nice day!'),
-- Michael Nelson (3 report)
('3100930128765', 'You are so dumb!'),
('3100930128765', 'This is the worst thing ever!'),
('3100930128765', 'Let’s grab lunch sometime!'),
-- David Miller (3 report)
('1801231123459', 'Good morning, how are you?'),
('1801231123459', 'I will hurt you!'),
('1801231123459', 'Have a wonderful weekend!'),
-- Mason Carter (5 report)
('5011231156789', 'Let’s meet up later!'),
('5011231156789', 'You are a complete failure!'),
('5011231156789', 'I hope you get what you deserve!'),
('5011231156789', 'I can’t stand you!'),
('5011231156789', 'I hope you get what you deserve!'),
-- Thomas Wilson (3 report)
('5050301123458', 'See you at work tomorrow!'),
('5050301123458', 'Take care and stay safe!'),
('5050301123458', 'You’re the worst person ever!'),
-- Daniel Harris (1 report)
('1080718229876', 'Hope you have an amazing day!'),
('1080718229876', 'I wish something bad happens to you!'),
-- Benjamin Martinez (3 reports)
('5121119223456', 'Looking forward to our meeting!'),
('5121119223456', 'Why are you so useless?'),
('5121119223456', 'You make everything worse!');


INSERT INTO BillSplitReports (ReportedUserCnp, ReportingUserCnp, DateOfTransaction, BillShare)
VALUES
-- David Miller (3 report)
('1801231123459', '7050415109876', '2025-02-20', 180.00),
('1801231123459', '5050301123458', '2025-02-18', 200.00),
('1801231123459', '5050301123458', '2025-01-29', 410.00),
--John Doe (3 reports)
('5040203070016', '5011231156789', '2025-01-17', 250.00),
('5040203070016', '5011231156789', '2025-02-19', 220.00),
('5040203070016', '1801231123459', '2025-03-24', 240.00),
('5040203070016', '1801231123459', '2025-02-28', 320.00),
-- Mason Carter (3 report)
('5011231156789', '7050415109876', '2025-01-16', 150.00),
('5011231156789', '1080718229876', '2025-03-21', 160.00),
('5011231156789', '3100930128765', '2025-01-30', 190.00),
-- Thomas Wilson (3 report)
('5050301123458', '3100930128765', '2025-01-14', 400.00),
('5050301123458', '3100930128765', '2025-03-22', 350.00),
('5050301123458', '5121119223456', '2025-02-01', 260.00),
-- Benjamin Martinez (3 reports)
('5121119223456', '1080718229876', '2025-02-15', 300.00),
('5121119223456', '5040203070016', '2025-02-02', 300.00),
('5121119223456', '5011231156789', '2025-02-23', 270.00),
-- Isabella Mitchell (1 report)
('7050415109876', '5050301123458', '2025-03-25', 130.00),
-- Daniel Harris (1 report)
('1080718229876', '5011231156789', '2025-02-27', 290.00),
-- Michael Nelson (2 report)
('3100930128765', '5121119223456', '2025-01-26', 380.00),
('3100930128765', '1080718229876', '2025-03-03', 250.00);


INSERT INTO TransactionLogs (SenderCnp, ReceiverCnp, TransactionDate, Amount, TransactionType, TransactionDescription) 
VALUES
('5040203070016', '5010228345678', '2025-01-17', 1000.00, 'Investment', 'Stock market investment'),
('5040203070016', '5011231156789', '2025-02-18', 300.00, 'Loan Payment', 'Paying old debt'),
('5040203070016', '7050415109876', '2025-02-16', 75.00, 'Bill Split', 'Restaurant bill'),
('5040203070016', '1080718229876', '2025-03-14', 60.00, 'Loan Payment', 'Paying for an old favor'),
('5040203070016', '1080718229876', '2025-01-10', 50.00, 'Bill Split', 'Netflix subscription'),
('5040203070016', '3100930128765', '2025-02-25', 40.00, 'Bill Split', 'WiFi bill'),
('5040203070016', '5121119223456', '2025-02-10', 90.00, 'Bill Split', 'Electricity bill'),
('5040203070016', '5010228345678', '2025-01-01', 20.00, 'Bill Split', 'Shared taxi fare'),
('5040203070016', '5010228345678', '2025-01-25', 25.00, 'Bill Split', 'Grocery bill share'),
('5040203070016', '5010228345678', '2025-02-10', 30.00, 'Bill Split', 'Dinner bill split'),
('5040203070016', '5010228345678', '2025-02-20', 50.00, 'Bill Split', 'Concert tickets'),
('5040203070016', '5010228345678', '2025-03-05', 60.00, 'Bill Split', 'Weekend trip'),
('7050415109876', '1080718229876', '2025-02-12', 500.00, 'Investment', 'Crypto trade'),
('5011231156789', '5050301123458', '2025-03-17', 700.00, 'Investment', 'Startup funding'),
('5040203070016', '5011231156789', '2025-01-15', 120.00, 'Bill Split', 'Lunch bill'),
('5040203070016', '3100930128765', '2025-01-22', 80.00, 'Bill Split', 'Gas bill'),
('5011231156789', '3100930128765', '2025-02-02', 150.00, 'Loan Payment', 'Car loan repayment'),
('1801231123459', '3100930128765', '2025-02-03', 200.00, 'Investment', 'Stock purchase'),
('5121119223456', '1080718229876', '2025-02-10', 90.00, 'Bill Split', 'Gym membership'),
('5011231156789', '3100930128765', '2025-02-14', 110.00, 'Loan Payment', 'Personal loan repayment'),
('1801231123459', '3100930128765', '2025-03-01', 50.00, 'Bill Split', 'Movie night'),
('5010228345678', '5050301123458', '2025-03-05', 60.00, 'Bill Split', 'Shared parking fee'),
('3100930128765', '5010228345678', '2025-03-10', 100.00, 'Investment', 'Bond investment'),
('3100930128765', '5010228345678', '2025-03-15', 30.00, 'Bill Split', 'Coffee outing');



INSERT INTO Investments (InvestorCnp, Details, AmountInvested, AmountReturned, InvestmentDate)
VALUES 
-- John Doe (CNP: 5040203070016)
('5040203070016', 'Investment in Apple Inc.', 1000.00, 1200.00, '2025-04-02'),
('5040203070016', 'Investment in Microsoft Corp.', 500.00, -1, '2025-04-02'),
('5040203070016', 'Investment in Amazon.com Inc.', 800.00, 100.00, '2025-04-03'),
('5040203070016', 'Investment in Google Inc.', 1200.00, 300.00, '2025-04-05'),
('5040203070016', 'Investment in Facebook Inc.', 1500.00, 2100.00, '2025-04-07'),
('5040203070016', 'Investment in Tesla Inc.', 2000.00, 2200.00, '2025-04-08'),
('5040203070016', 'Investment in Nvidia Corp.', 2500.00, -1, '2025-04-08'),

-- Michael Brown (CNP: 5010228345678)
('5010228345678', 'Investment in Alphabet Inc.', 2000.00, 1000.00, '2025-04-01'),
('5010228345678', 'Investment in Intel Corp.', 1800.00, 2000.00, '2025-04-04'),
('5010228345678', 'Investment in Cisco Systems Inc.', 2200.00, 2400.00, '2025-04-06'),
('5010228345678', 'Investment in Oracle Corp.', 2500.00, 2800.00, '2025-04-09'),

-- David Miller (CNP: 1801231123459)
('1801231123459', 'Investment in Johnson', 3000.00, 2500.00, '2025-04-08'),
('1801231123459', 'Investment in Johnson & Johnson', 3000.00, 1500.00, '2025-04-08'),
('1801231123459', 'Investment in Procter & Gamble Co.', 2800.00, 1000.00, '2025-04-08'),
('1801231123459', 'Investment in Coca-Cola Co.', 2500.00, 1800.00, '2025-04-08'),
('1801231123459', 'Investment in PepsiCo Inc.', 2200.00, 1400.00, '2025-04-08'),
('1801231123459', 'Investment in Unilever PLC', 2000.00, -1, '2025-04-08'),

-- Isabella Mitchell (CNP: 7050415109876)
('7050415109876', 'Investment in Walmart Inc.', 5000.00, 5500.00, '2025-04-01'),
('7050415109876', 'Investment in Home Depot Inc.', 4500.00, 4800.00, '2025-04-02'),
('7050415109876', 'Investment in Target Corp.', 4000.00, 4200.00, '2025-04-04'),
('7050415109876', 'Investment in Costco Wholesale Corp.', 3500.00, 3800.00, '2025-04-06'),
('7050415109876', 'Investment in Lowes Companies Inc.', 3000.00, 3200.00, '2025-04-08'),
('7050415109876', 'Investment in TJX Companies Inc.', 2500.00, -1, '2025-04-08'),

-- Mason Carter (CNP: 5011231156789)
('5011231156789', 'Investment in Exxon Corp.', 4000.00, 5000.00, '2025-04-06'),
('5011231156789', 'Investment in Mobil.', 3000.00, 1000.00, '2025-04-07'),
('5011231156789', 'Investment in Exxon Mobil Corp.', 2500.00, 5000.00, '2025-04-07'),
('5011231156789', 'Investment in Chevron Corp.', 4000.00, 1200.00, '2025-04-07'),
('5011231156789', 'Investment in ConocoPhillips', 3500.00, 2800.00, '2025-04-08'),
('5011231156789', 'Investment in Royal Dutch Shell PLC', 3000.00, 3200.00, '2025-04-08'),
('5011231156789', 'Investment in BP PLC', 2500.00, -1, '2025-04-08'),

-- Sarah Davis (CNP: 7050415109876)
('7050415109876', 'Investment in Visa Inc.', 5500.00, 4000.00, '2025-04-01'),
('7050415109876', 'Investment in Mastercard Inc.', 5000.00, 5200.00, '2025-04-03'),
('7050415109876', 'Investment in American Express Co.', 4500.00, 3800.00, '2025-04-05'),
('7050415109876', 'Investment in Discover Financial Services', 4000.00, 100.00, '2025-04-07'),
('7050415109876', 'Investment in Capital One Financial Corp.', 3500.00, 100.00, '2025-04-08'),

-- Daniel Harris (CNP: 1080718229876)
('1080718229876', 'Investment in Salesforce.com Inc.', 6500.00, 7000.00, '2025-04-02'),
('1080718229876', 'Investment in Oracle Corp.', 6000.00, 6200.00, '2025-04-04'),
('1080718229876', 'Investment in SAP SE', 5500.00, 5800.00, '2025-04-06'),
('1080718229876', 'Investment in IBM Corp.', 5000.00, 5200.00, '2025-04-08'),

-- Lisa Wilson (CNP: 3100930128765)
('3100930128765', 'Investment in UnitedHealth Group Inc.', 6000.00, 6500.00, '2025-04-01'),
('3100930128765', 'Investment in CVS Health Corp.', 5500.00, 5800.00, '2025-04-03'),
('3100930128765', 'Investment in Aetna Inc.', 5000.00, 5200.00, '2025-04-05'),
('3100930128765', 'Investment in Cigna Corp.', 4500.00, 4800.00, '2025-04-07'),
('3100930128765', 'Investment in Humana Inc.', 4000.00, -1, '2025-04-08'),

-- Mark Taylor (CNP: 1080718229876)
('1080718229876', 'Investment in 3M Co.', 7000.00, 7500.00, '2025-04-02'),
('1080718229876', 'Investment in General Electric Co.', 6500.00, 6800.00, '2025-04-04'),
('1080718229876', 'Investment in Siemens AG', 6000.00, 6200.00, '2025-04-06'),
('1080718229876', 'Investment in Honeywell International Inc.', 5500.00, 5800.00, '2025-04-08'),

-- Thomas Wilson (CNP: 5121119223456)
('5121119223456', 'Investment in Boeing Co.', 7500.00, 8000.00, '2025-04-01'),
('5121119223456', 'Investment in Lockheed Martin Corp.', 7000.00, 7200.00, '2025-04-03'),
('5121119223456', 'Investment in Northrop Grumman Corp.', 6500.00, 6800.00, '2025-04-05'),
('5121119223456', 'Investment in Raytheon Technologies Corp.', 6000.00, 6200.00, '2025-04-07'),
('5121119223456', 'Investment in General Dynamics Corp.', 5500.00, -1, '2025-04-08');


INSERT INTO CreditScoreHistory (UserCnp, Date, Score)
VALUES
-- John Doe (20 changes)
('5040203070016', '2025-01-01', 450),
('5040203070016', '2025-01-05', 500),
('5040203070016', '2025-01-10', 550),
('5040203070016', '2025-01-20', 650),
('5040203070016', '2025-02-08', 650),
('5040203070016', '2025-02-15', 650),
('5040203070016', '2025-02-20', 100),
('5040203070016', '2025-02-25', 150),
('5040203070016', '2025-02-28', 200),
('5040203070016', '2025-03-01', 250),
('5040203070016', '2025-03-03', 300),
('5040203070016', '2025-03-05', 350),
('5040203070016', '2025-03-10', 400),
('5040203070016', '2025-03-25', 450),
('5040203070016', '2025-03-26', 500),
('5040203070016', '2025-03-27', 550),
('5040203070016', '2025-03-28', 600),
('5040203070016', '2025-03-29', 650),
('5040203070016', '2025-03-30', 700),
('5040203070016', '2025-03-31', 120),
--Michael Brown (20 changes)
('5010228345678', '2025-01-17', 130),
('5010228345678', '2025-01-20', 180),
('5010228345678', '2025-01-22', 230),
('5010228345678', '2025-01-23', 280),
('5010228345678', '2025-01-24', 330),
('5010228345678', '2025-02-02', 380),
('5010228345678', '2025-02-14', 430),
('5010228345678', '2025-02-20', 480),
('5010228345678', '2025-02-25', 480),
('5010228345678', '2025-02-28', 580),
('5010228345678', '2025-03-02', 630),
('5010228345678', '2025-03-05', 680),
('5010228345678', '2025-03-07', 700),
('5010228345678', '2025-03-10', 120),
('5010228345678', '2025-03-15', 170),
('5010228345678', '2025-03-19', 170),
('5010228345678', '2025-03-27', 270),
('5010228345678', '2025-03-28', 320),
('5010228345678', '2025-03-30', 370),
('5010228345678', '2025-03-31', 420),
--David Miller (20 changes)
('1801231123459', '2025-01-04', 410),
('1801231123459', '2025-01-10', 480),
('1801231123459', '2025-01-16', 550),
('1801231123459', '2025-01-23', 550),
('1801231123459', '2025-02-10', 690),
('1801231123459', '2025-02-15', 700),
('1801231123459', '2025-02-20', 140),
('1801231123459', '2025-02-24', 160),
('1801231123459', '2025-02-28', 210),
('1801231123459', '2025-03-04', 210),
('1801231123459', '2025-03-07', 310),
('1801231123459', '2025-03-17', 370),
('1801231123459', '2025-03-18', 420),
('1801231123459', '2025-03-19', 460),
('1801231123459', '2025-03-20', 510),
('1801231123459', '2025-03-25', 570),
('1801231123459', '2025-03-26', 610),
('1801231123459', '2025-03-28', 660),
('1801231123459', '2025-03-29', 700),
('1801231123459', '2025-03-30', 130),
--Thomas Wilson (12 changes)
('5050301123458', '2025-01-10', 400),
('5050301123458', '2025-01-20', 480),
('5050301123458', '2025-02-12', 389),
('5050301123458', '2025-02-28', 389),
('5050301123458', '2025-03-04', 389),
('5050301123458', '2025-03-10', 403),
('5050301123458', '2025-03-15', 102),
('5050301123458', '2025-03-20', 290),
('5050301123458', '2025-03-25', 204),
('5050301123458', '2025-03-28', 394),
('5050301123458', '2025-03-29', 400),
('5050301123458', '2025-03-30', 550),
-- Mason Carter  (10 changes)
('5011231156789', '2025-01-10', 410),
('5011231156789', '2025-01-15', 600),
('5011231156789', '2025-02-20', 600),
('5011231156789', '2025-02-25', 324),
('5011231156789', '2025-03-12', 224),
('5011231156789', '2025-03-18', 133),
('5011231156789', '2025-03-20', 699),
('5011231156789', '2025-03-29', 564),
('5011231156789', '2025-03-30', 435),
('5011231156789', '2025-03-31', 500),
--Isabella Mitchell (10 changes)
('7050415109876', '2025-01-10', 390),
('7050415109876', '2025-02-15', 470),
('7050415109876', '2025-03-02', 520),
('7050415109876', '2025-03-10', 234),
('7050415109876', '2025-03-17', 234),
('7050415109876', '2025-03-22', 543),
('7050415109876', '2025-03-25', 234),
('7050415109876', '2025-03-28', 675),
('7050415109876', '2025-03-29', 345),
('7050415109876', '2025-03-30', 765),
-- Daniel Harris (10 changes)
('1080718229876', '2025-01-10', 460),
('1080718229876', '2025-02-15', 530),
('1080718229876', '2025-03-17', 580),
('1080718229876', '2025-03-22', 455),
('1080718229876', '2025-03-24', 453),
('1080718229876', '2025-03-26', 667),
('1080718229876', '2025-03-27', 232),
('1080718229876', '2025-03-28', 488),
('1080718229876', '2025-03-29', 786),
('1080718229876', '2025-03-30', 123),
-- Michael Nelson (10 changes)
('3100930128765', '2025-01-10', 420),
('3100930128765', '2025-02-15', 490),
('3100930128765', '2025-03-12', 550),
('3100930128765', '2025-03-17', 550),
('3100930128765', '2025-03-20', 345),
('3100930128765', '2025-03-22', 475),
('3100930128765', '2025-03-26', 678),
('3100930128765', '2025-03-28', 359),
('3100930128765', '2025-03-29', 562),
('3100930128765', '2025-03-30', 578),
-- Benjamin Martinez (12 changes)
('5121119223456', '2025-01-10', 450),
('5121119223456', '2025-02-15', 520),
('5121119223456', '2025-02-18', 123),
('5121119223456', '2025-03-11', 432),
('5121119223456', '2025-03-17', 564),
('5121119223456', '2025-03-19', 678),
('5121119223456', '2025-03-25', 129),
('5121119223456', '2025-03-27', 436),
('5121119223456', '2025-03-28', 217),
('5121119223456', '2025-03-29', 548),
('5121119223456', '2025-03-30', 234),
('5121119223456', '2025-03-31', 600);


INSERT INTO ActivityLog (ActivityName, UserCnp, LastModifiedAmount, ActivityDetails) 
VALUES
-- John Doe (4 activities)
('Bill splitting', '5040203070016', -250, 'Paid for dinner with friends'),
('Investments', '5040203070016', 500, 'Profited from stock trading'),
('Loans', '5040203070016', -600, 'Loan repayment deducted'),
('Chat', '5040203070016', -200, 'Offensive messages'),
-- Benjamin Martinez (4 activities)
('Chat', '5121119223456', -200, 'Offensive messages'),
('Bill splitting', '5121119223456', 600, 'Paid electricity bill'),
('Loans', '5121119223456', -400, 'EMI deducted for car loan'),
('Investments', '5121119223456', 300, 'Interest credited on fixed deposit'),
-- Michael Nelson (4 activities)
('Chat', '3100930128765', 150, 'Offensive messages'),
('Bill splitting', '3100930128765', -350, 'Paid internet bill'),
('Loans', '3100930128765', -700, 'Mortgage payment processed'),
('Investments', '3100930128765', 400, 'Dividend received from stock holdings'),
-- Daniel Harris (4 activities)
('Chart', '1080718229876', 120, 'Offensive messages'),
('Bill splitting', '1080718229876', -200, 'Paid for a shared taxi ride'),
('Investments', '1080718229876', 600, 'Crypto investment appreciated'),
('Loans', '1080718229876', -500, 'Credit card bill auto-debited'),
--Isabella Mitchell (4 activities)
('Bill splitting', '7050415109876', -300, 'Paid for office party'),
('Loans', '7050415109876', -150, 'Personal loan installment deducted'),
('Investments', '7050415109876', 700, 'High returns from real estate'),
('Chat', '7050415109876', 200, 'Offensive messages'),
-- Mason Carter  (4 activities)
('Chat', '5011231156789', 250, 'Offensive messages'),
('Loans', '5011231156789', -500, 'Auto-loan deduction from account'),
('Investments', '5011231156789', 300, 'Profit from short-term trading'),
('Bill splitting', '5011231156789', -400, 'Shared grocery expenses'),
--Thomas Wilson (3 activities)
('Bill splitting', '5050301123458', -450, 'Paid for a group trip'),
('Investments', '5050301123458', 500, 'Stock market gain recorded'),
('Chat', '5050301123458', 300, 'Offensive messages'),
--David Miller (3 activities)
('Chat', '1801231123459', 600, 'Offensive messages'),
('Bill splitting', '1801231123459', -350, 'Paid for a subscription service'),
('Loans', '1801231123459', -250, 'Education loan EMI paid'),
--Michael Brown (4 activities) 
('Chat', '5010228345678', 350, 'Offensive messages'),
('Investments', '5010228345678', 400, 'Interest earned on savings'),
('Loans', '5010228345678', -700, 'Debt repayment made'),
('Bill splitting', '5010228345678', -100, 'Paid for concert tickets');


INSERT INTO Tips(CreditScoreBracket, TipText)
VALUES
('Low-Credit', 'Pay your bills on time,Late payments can decrease your score'),
('Low-Credit','Credit cards aren''t free money, if you cannot afford something, dont put it on Credit hoping you will figure it out after'),
('Low-Credit','Avoid doing your past mistakes '),
('Low-Credit','if you can''t pay off a card, stop using it. Swiping more won''t fix your situation'),
('Low-Credit','Start small if you have no credit history.'),
('Medium-Credit','Set up automatic payments. One missed bill can set you back years '),
('Medium-Credit','Stop applying for new credit so often. Every application lowers your score a bit'),
('Medium-Credit','Don''t let unpaid debts sit for too long. Old debts still impact your credit, so pay them off.'),
('Medium-Credit','Pay off debts with the highest interest first. It saves you money and helps with your score'),
('Medium-Credit','Don''t apply for loans you don''t need.Every application slightly lowers your score'),
('High-Credit','Pay your full balance every month.'),
('High-Credit','Set up autopay for all bills. Even one missed payment can drop your score fast'),
('High-Credit','Avoid unnecessary hard inquiries. Only apply for new credit when you truly need it'),
('High-Credit','Use credit smartly. A mix of credit types(loans, credit cards) can boost your score'),
('High-Credit','Keep your credit card utilization low.Even if your limit is high, don''t use more than 10-20%');


INSERT INTO Messages(Type, Message)
VALUES
('Congrats-message','Your credit score just went up! Keep making smart moves, and you will unlock better opportunities.'),
('Congrats-message','You''re roving you know how to handle money :).Lenders trust you more, and so should you.'),
('Congrats-message','Great progress! A higher credit score means better deals, lower interest rates, and more financial freedom.'),
('Roast-message','Your credit score is lower than your phone battery at 2%.Charge it up before it''s too late!'),
('Roast-message','Even monopoly money has better credit than you right now. Get it together!'),
('Roast-message','Your credit score is so low that even a charity wouldn''t loan you a dollar!')


INSERT INTO GivenTips (UserCnp, TipId, MessageId, Date)
VALUES
-- John Doe (2 tips)
('5040203070016', 1, NULL, '2023-01-15'),
('5040203070016', 2, NULL, '2023-02-10'),
-- David Miller (2 tips)
('1801231123459', 3, NULL, '2023-03-05'),
('1801231123459', 5, NULL, '2023-03-25'),
-- Michael Brown (3 tip)
('5010228345678', 7, NULL, '2023-04-10'),
('5010228345678', NULL, 1, '2023-04-18'),
('5010228345678', NULL, 2, '2023-05-22'),
-- Mason Carter (1 tip)
('5011231156789', NULL, 3, '2023-06-14'),
-- Daniel Harris (1 tip)
('1080718229876', NULL, 4, '2023-07-01'),
-- Isabella Mitchell (2 tips)
('7050415109876', NULL, 5, '2023-07-15'),
('7050415109876', NULL, 6, '2023-08-03');


INSERT INTO LoanRequest (UserCnp, Amount, ApplicationDate, RepaymentDate, Status) 
VALUES 
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Unsolved'),
('5010228345678', 1000.00, '2024-02-15', '2027-02-15', 'Unsolved'),
('1801231123459', 3000.00, '2024-04-05', '2025-04-05', 'Unsolved'),
('5050301123458', 1000.00, '2024-08-30', '2027-08-30', 'Unsolved'),
('5011231156789', 1000.00, '2024-10-22', '2029-10-22', 'Unsolved'),
('1080718229876', 6000.00, '2024-11-05', '2026-11-05', 'Unsolved'),
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Unsolved'),
('7050415109876', 1000.00, '2024-02-15', '2027-02-15', 'Unsolved'),
('1080718229876', 3000.00, '2024-04-05', '2025-04-05', 'Unsolved'),
('5121119223456', 1000.00, '2024-08-30', '2027-08-30', 'Unsolved'),
('3100930128765', 1000.00, '2024-10-22', '2029-10-22', 'Unsolved'),
('5011231156789', 6000.00, '2022-11-05', '2024-11-05', 'Solved'),
('5121119223456', 9900.00, '2024-11-05', '2026-11-05', 'Unsolved'),
('5040203070016', 5000.00, '2024-01-10', '2026-01-10', 'Unsolved'),
('1801231123459', 1000.00, '2024-02-15', '2027-02-15', 'Unsolved'),
('7050415109876', 3000.00, '2024-04-05', '2025-04-05', 'Unsolved'),
('5040203070016', 1000.00, '2024-05-12', '2028-05-12', 'Unsolved'),
('5040203070016', 7000.00, '2024-06-18', '2026-06-18', 'Unsolved'),
('1080718229876', 9500.00, '2024-07-25', '2026-07-25', 'Unsolved'),
('3100930128765', 1000.00, '2024-08-30', '2027-08-30', 'Unsolved'),
('5040203070016', 4000.00, '2024-09-12', '2025-09-12', 'Unsolved'),
('5040203070016', 1000.00, '2024-10-22', '2029-10-22', 'Unsolved'),
('7050415109876', 6000.00, '2024-11-05', '2026-11-05', 'Unsolved');


INSERT INTO Loans (LoanRequestId, UserCnp, Amount, ApplicationDate, RepaymentDate, InterestRate, NumberOfMonths, Status, MonthlyPaymentAmount, MonthlyPaymentsCompleted, RepaidAmount, Penalty)
VALUES
(1, '5040203070016', 5000.00, '2024-01-10', '2026-01-10', 5.5, 24, 'Active', 220.50, 3, 661.50, 0.00),
(2, '5010228345678', 1000.00, '2024-02-15', '2027-02-15', 4.8, 36, 'Approved', 368.90, 0, 0.00, 0.00),
(3, '1801231123459', 3000.00, '2024-04-05', '2025-04-05', 6.2, 24, 'Active', 354.70, 6, 2128.20, 10.00),
(4, '1080718229876', 4000.00, '2024-09-12', '2025-09-12', 5.0, 12, 'Completed', 270.00, 12, 3240.00, 0.00),
(5, '3100930128765', 9500.00, '2024-07-25', '2026-07-25', 7.0, 48, 'Delayed', 372.00, 10, 3720.00, 50.00),
(6, '5010228345678', 8000.00, '2024-03-20', '2026-03-20', 4.5, 24, 'Active', 298.00, 4, 1192.00, 0.00),
(7, '7050415109876', 9500.00, '2024-07-25', '2026-07-25', 5.3, 24, 'Approved', 350.75, 0, 0.00, 0.00),
(8, '5040203070016', 1000.00, '2024-08-30', '2027-08-30', 5.9, 36, 'Active', 392.50, 2, 785.00, 0.00),
(9, '5010228345678', 4000.00, '2024-09-12', '2025-09-12', 4.2, 12, 'Pending', 345.00, 0, 0.00, 0.00),
(10, '5121119223456', 1000.00, '2024-10-22', '2029-10-22', 6.5, 60, 'Active', 350.00, 1, 350.00, 0.00),
(11, '3100930128765', 6000.00, '2024-11-05', '2026-11-05', 5.1, 24, 'Delayed', 275.00, 8, 2200.00, 25.00);
