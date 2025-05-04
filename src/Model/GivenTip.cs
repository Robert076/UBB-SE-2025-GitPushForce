using System;

namespace Src.Model
{
	public class GivenTip
	{
		public int Id { get; set; }
        public string UserCnp { get; set; }
        public int TipID { get; set; }
        public int MessageID { get; set; }
        public DateOnly Date { get; set; }

        public GivenTip(int id, string userCNP, int tipID, int messageID, DateOnly date)
		{
			Id = id;
			UserCnp = userCNP;
			TipID = tipID;
			MessageID = messageID;
			Date = date;
		}
		public GivenTip()
		{
			Id = 0;
			UserCnp = string.Empty;
			TipID = 0;
			MessageID = 0;
			Date = new DateOnly();
		}
	}
}
