using src.Model;
using src.Repos;
using System;
using System.Collections.Generic;
namespace src.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
        }

        public List<ActivityLog> GetActivityForUser(string userCnp)
        {
            if (string.IsNullOrWhiteSpace(userCnp))
            {
                throw new ArgumentException("user cannot be found");
            }
            return _activityRepository.GetActivityForUser(userCnp);
        }   

    }
}
