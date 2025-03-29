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

INSERT INTO ChatReports(ReportedUserCNP, ReportedMessage) VALUES ('5040203070016', 'Youre a dic')

insert into Tips(CreditScoreBracket, TipText)
values('Low-Credit', 'Pay your bills on time,Late payments can decrease your score'),
('Low-Credit','Credit cards aren’t free money, if you cannot afford something, dont put it on Credit hoping you will figure it out after'),
('Low-Credit','Avoid doing your past mistakes '),
('Low-Credit','if you can’t pay off a card, stop using it. Swiping more won’t fix your situation'),
('Low-Credit','Start small if you have no credit history.'),
('Medium-Credit','Set up automatic payments. One missed bill can set you back years '),
('Medium-Credit','Stop applying for new credit so often. Every application lowers your score a bit'),
('Medium-Credit','Don’t let unpaid debts sit for too long. Old debts still impact your credit, so pay them off.'),
('Medium-Credit','Pay off debts with the highest interest first. It saves you money and helps with your score'),
('Medium-Credit','Don’t apply for loans you don’t need.Every application slightly lowers your score'),
('High-Credit','Pay your full balance every month.'),
('High-Credit','Set up autopay for all bills. Even one missed payment can drop your score fast'),
('High-Credit','Avoid unnecessary hard inquiries. Only apply for new credit when you truly need it'),
('High-Credit','Use credit smartly. A mix of credit types(loans, credit cards) can boost your score'),
('High-Credit','Keep your credit card utilization low.Even if your limit is high, don’t use more than 10-20%')


insert into Messages(Type,Message)
values('Congrats-message','Your credit score just went up! Keep making smart moves, and you will unlock better opportunities.'),
('Congrats-message','You’re roving you know how to handle money :).Lenders trust you more, and so should you.'),
('Congrats-message','Great progress! A higher credit score means better deals, lower interest rates, and more financial freedom.'),
('Roast-message','Your credit score is lower than your phone battery at 2%.Charge it up before it’s too late!'),
('Roast-message','Even monopoly money has better credit than you right now. Get it together!'),
('Roast-message','Your credit score is so low that even a charity wouldn’t loan you a dollar!')

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