﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class UserZodiacSign
    {
        private string _cnpUser;
        private DateTime _birthDate_user;
        private string _signUser;
        private decimal _amountCredit;

        public UserZodiacSign(string cnpUser, DateTime birthDate_user, decimal amountCredit)
        {
            _cnpUser = cnpUser;
            _birthDate_user = birthDate_user;
            _signUser = CalculateZodiacSign(birthDate_user);
            _amountCredit = amountCredit;
        }

        public UserZodiacSign()
        {
            _cnpUser = string.Empty;
            _birthDate_user = DateTime.Now;
            _signUser = CalculateZodiacSign(DateTime.Now);
            _amountCredit = 0;
        }

        public string CnpUser
        {
            get { return _cnpUser; }
            set { _cnpUser = value; }
        }

        public DateTime BirthDate_user
        {
            get { return _birthDate_user; }
            set { 
                  _birthDate_user = value; 
                  _signUser = CalculateZodiacSign(value);
            }
        }

        public string SignUser
        {
            get { return _signUser; }
            //set { _signUser = value; }
        }

        public decimal AmountCredit
        {
            get { return _amountCredit; }
            set { _amountCredit = value; }
        }

        private string CalculateZodiacSign(DateTime birthDate)
        {

            int day = birthDate.Day;
            int month = birthDate.Month;

            if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
                return "Berbec";

            if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
                return "Taur";

            if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
                return "Gemeni";

            if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
                return "Rac";

            if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
                return "Leu";

            if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
                return "Fecioara";

            if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
                return "Balanta";

            if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
                return "Scorpion";

            if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
                return "Sagetator";

            if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
                return "Capricorn";

            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
                return "Varsator";

            if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
                return "Pesti";


            return "Capricorn";


        }
    }
}
