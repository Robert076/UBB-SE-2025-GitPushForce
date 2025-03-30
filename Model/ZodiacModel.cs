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
    }
}
