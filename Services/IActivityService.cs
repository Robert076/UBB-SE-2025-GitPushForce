using src.Model;
using System.Collections.Generic;

namespace src.Services
{
    public interface IActivityService
    {
        public List<ActivityLog> GetActivityForUser(string userCNP);
    }
}
