using System;
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

        public UserZodiacSign(string cnpUser, DateTime birthDate_user, string signUser, decimal amountCredit)
        {
            _cnpUser = cnpUser;
            _birthDate_user = birthDate_user;
            _signUser = signUser;
            _amountCredit = amountCredit;
        }

        public UserZodiacSign()
        {
            _cnpUser = string.Empty;
            _birthDate_user = DateTime.Now;
            _signUser = string.Empty;
            _amountCredit = 0;
        }



    }
}
