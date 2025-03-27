using System;

namespace src.Model
{
    public class User
    {
        private int _id;
        private string _cnp;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _hashedPassword;
        private int _noOffenses;
        private int _riskScore;
        private decimal _roi;
        private int _creditScore;
        private DateOnly _birthday;
        private string _zodiacSign;
        private string _zodiacAttribute;
        private int _noOfBillSharesPaid;

        public User(
            int id,
            string cnp,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string hashedPassword,
            int noOffenses,
            int riskScore,
            decimal roi,
            int creditScore,
            DateOnly birthday,
            string zodiacSign,
            string zodiacAttribute,
            int noOfBillSharesPaid)
        {
            _id = id;
            _cnp = cnp;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phoneNumber = phoneNumber;
            _hashedPassword = hashedPassword;
            _noOffenses = noOffenses;
            _riskScore = riskScore;
            _roi = roi;
            _creditScore = creditScore;
            _birthday = birthday;
            _zodiacSign = zodiacSign;
            _zodiacAttribute = zodiacAttribute;
            _noOfBillSharesPaid = noOfBillSharesPaid;
        }

        public User()
        {
            _id = 0;
            _cnp = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _email = string.Empty;
            _phoneNumber = string.Empty;
            _hashedPassword = string.Empty;
            _noOffenses = 0;
            _riskScore = 0;
            _roi = 0;
            _creditScore = 0;
            _birthday = new DateOnly();
            _zodiacSign = string.Empty;
            _zodiacAttribute = string.Empty;
            _noOfBillSharesPaid = 0;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string CNP
        {
            get { return _cnp; }
            set { _cnp = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string HashedPassword
        {
            get { return _hashedPassword; }
            set { _hashedPassword = value; }
        }

        public int NoOffenses
        {
            get { return _noOffenses; }
            set { _noOffenses = value; }
        }

        public int RiskScore
        {
            get { return _riskScore; }
            set { _riskScore = value; }
        }

        public decimal ROI
        {
            get { return _roi; }
            set { _roi = value; }
        }

        public int CreditScore
        {
            get { return _creditScore; }
            set { _creditScore = value; }
        }

        public DateOnly Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public string ZodiacSign
        {
            get { return _zodiacSign; }
            set { _zodiacSign = value; }
        }

        public string ZodiacAttribute
        {
            get { return _zodiacAttribute; }
            set { _zodiacAttribute = value; }
        }

        public int NoOfBillSharesPaid
        {
            get { return _noOfBillSharesPaid; }
            set { _noOfBillSharesPaid = value; }
        }
    }
}
