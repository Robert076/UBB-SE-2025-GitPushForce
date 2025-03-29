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

        public ZodiacModel GetZodiacInfoByCNP(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be null or whitespace.", nameof(cnp));
            }

            ZodiacModel? zodiacModel = _zodiacRepository.GetZodiacModelByCNP(cnp);
            if (zodiacModel == null)
            {
                throw new Exception("Zodiac information not found for the provided CNP.");
            }
            return zodiacModel;
        }


    }
}
