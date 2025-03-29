using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    public class ZodiacModel
    {
        private int _Id;
        private string _CNP;
        private DateOnly _Birthday;
        private int _CreditScore;
        private string _ZodiacSign;
        private string _ZodiacAttribute;


        public ZodiacModel(int id, string cnp, DateOnly birthday, int creditScore, string zodiacSign, string zodiacAttribute)
        {
            _Id = id;
            _CNP = cnp;
            _Birthday = birthday;
            _CreditScore = creditScore;
            _ZodiacSign = zodiacSign;
            _ZodiacAttribute = zodiacAttribute;
        }

        public ZodiacModel()
        {
            _Id = 0;
            _CNP = string.Empty;
            _Birthday = new DateOnly();
            _CreditScore = 0;
            _ZodiacSign = string.Empty;
            _ZodiacAttribute = string.Empty;
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string CNP
        {
            get { return _CNP; }
            set { _CNP = value; }
        }

        public DateOnly Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        public int CreditScore
        {
            get { return _CreditScore; }
            set { _CreditScore = value; }
        }

        public string ZodiacSign
        {
            get { return _ZodiacSign; }
            set { _ZodiacSign = value; }
        }

        public string ZodiacAttribute
        {
            get { return _ZodiacAttribute; }
            set { _ZodiacAttribute = value; }

        }



    }

}
