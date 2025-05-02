using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Model
{
    class ZodiacModel
    {
        public int Id { get; set; }
        public string Cnp { get; set; }
        public DateOnly Birthday { get; set; }
        public int CreditScore { get; set; }
        public string ZodiacSign { get; set; }
        public string ZodiacAttribute { get; set; }
    }
}
