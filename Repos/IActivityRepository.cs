using src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repos
{
    public interface IActivityRepository
    {
        public void AddActivity(string userCNP, string activityName, int amount, string details);

        public List<ActivityLog> GetActivityForUser(string userCNP);

    }
}
