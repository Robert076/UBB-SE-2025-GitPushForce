namespace src.Model
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string UserCnp { get; set; }
        public string ActivityName { get; set; }
        public int LastModifiedAmount { get; set; }
        public string ActivityDetails { get; set; }

        public ActivityLog(int id, string userCNP, string name, int amount, string details)
        {
            Id = id;
            UserCnp = userCNP;
            ActivityName = name;
            LastModifiedAmount = amount;
            ActivityDetails = details;
        }

        public ActivityLog()
        {
            Id = 0;
            UserCnp = string.Empty;
            ActivityName = string.Empty;
            LastModifiedAmount = 0;
            ActivityDetails = string.Empty;
        }
    }
}
