using System;
using System.Collections.Generic;
using Src.Model;
using Src.Repos;
namespace Src.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
        }

        public List<ActivityLog> GetActivityForUser(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("user cannot be found");
            }
            return activityRepository.GetActivityForUser(userCnp);
        }
    }
}
