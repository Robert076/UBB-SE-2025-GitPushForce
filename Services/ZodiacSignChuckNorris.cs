using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    class ZodiacSignChuckNorrisService
    {
        public UserZodiacSign ZodiacSignModel;

        public ZodiacSignChuckNorrisService()
        {
            this.ZodiacSignModel= new UserZodiacSign(); 
        }

    }
}
