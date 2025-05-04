using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface IUserService
    {
        public User GetUserByCnp(string cnp);
        public List<User> GetUsers();
    }
}
