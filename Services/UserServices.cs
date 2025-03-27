using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public class UserServices
    {
        private readonly UserRepository _userRepository;
        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User GetUserByCNP(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty");
            }
            return _userRepository.GetUserByCNP(cnp);
        }
    }
}