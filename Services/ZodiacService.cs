using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Repos;

namespace src.Services
{
    public class ZodiacService
    {
        UserRepository _userRepository;

        public ZodiacService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

    }
}
