using System.Collections.Generic;
using Src.Model;

namespace Src.Repos
{
    public interface IActivityRepository
    {
        public void AddActivity(string userCNP, string activityName, int amount, string details);

        public List<ActivityLog> GetActivityForUser(string userCNP);
    }
}
