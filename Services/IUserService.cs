using src.Model;
using System.Collections.Generic;

namespace src.Services
{
    public interface IUserService
    {
        public User GetUserByCnp(string cnp);
        public List<User> GetUsers();
    }
}
