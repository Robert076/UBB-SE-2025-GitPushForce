using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;

namespace src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User GetUserByCnp(string cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
            {
                throw new ArgumentException("CNP cannot be empty");
            }
            return _userRepository.GetUserByCnp(cnp);
        }


        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}