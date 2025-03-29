using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Model;
using src.Repos;


namespace src.Services
{
    public class ZodiacService
    {
        private readonly ZodiacRepository _zodiacRepository;

        public ZodiacService(ZodiacRepository zodiacRepository)
        {
            _zodiacRepository = zodiacRepository ?? throw new ArgumentNullException(nameof(zodiacRepository));
        }




    }
}
