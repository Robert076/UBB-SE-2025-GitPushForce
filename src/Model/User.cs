using System;

namespace Src.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Cnp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public int NumberOfOffenses { get; set; }
        public int RiskScore { get; set; }
        public decimal ROI { get; set; }
        public int CreditScore { get; set; }
        public DateOnly Birthday { get; set; }
        public string ZodiacSign { get; set; }
        public string ZodiacAttribute { get; set; }
        public int NumberOfBillSharesPaid { get; set; }
        public int Income { get; set; }
        public decimal Balance { get; set; }

        public User(
            int id,
            string cnp,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string hashedPassword,
            int numberOfOffenses,
            int riskScore,
            decimal roi,
            int creditScore,
            DateOnly birthday,
            string zodiacSign,
            string zodiacAttribute,
            int numberOfBillSharesPaid,
            int income,
            decimal balance)
        {
            Id = id;
            Cnp = cnp;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            HashedPassword = hashedPassword;
            NumberOfOffenses = numberOfOffenses;
            RiskScore = riskScore;
            ROI = roi;
            CreditScore = creditScore;
            Birthday = birthday;
            ZodiacSign = zodiacSign;
            ZodiacAttribute = zodiacAttribute;
            NumberOfBillSharesPaid = numberOfBillSharesPaid;
            Income = income;
            Balance = balance;
        }

        public User()
        {
            Id = 0;
            Cnp = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            HashedPassword = string.Empty;
            NumberOfOffenses = 0;
            RiskScore = 0;
            ROI = 0;
            CreditScore = 0;
            Birthday = new DateOnly();
            ZodiacSign = string.Empty;
            ZodiacAttribute = string.Empty;
            NumberOfBillSharesPaid = 0;
            Income = 0;
            Balance = 0;
        }
    }
}
