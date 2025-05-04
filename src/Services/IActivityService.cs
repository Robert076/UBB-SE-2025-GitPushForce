using System.Collections.Generic;
using Src.Model;

namespace Src.Services
{
    public interface IActivityService
    {
        public List<ActivityLog> GetActivityForUser(string userCNP);
    }
}
