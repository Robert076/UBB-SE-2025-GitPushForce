using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    class ZodiacModel
    {
        public int _Id { get; set; }
        public string _CNP { get; set; }
        public DateOnly _Birthday { get; set; }
        public int _CreditScore { get; set; }
        public string _ZodiacSign { get; set; }
        public string _ZodiacAttribute { get; set; }


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

    }

    
}
