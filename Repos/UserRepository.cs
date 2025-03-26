using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Data;
using src.Model;

namespace src.Repos
{
    public class UserRepository
    {
        private readonly DatabaseConnection dbConn;

        public UserRepository(DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        public int CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if(string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("First and last names cannot be empty");
            }
        }
    }
}
