using System;

namespace src.Model
{
	public class GivenTip
	{
		private int _id;
		private string _userCNP;
		private int _tipID;
		private int _messageID;
		private DateOnly _date;

		public GivenTip(int id, string userCNP, int tipID, int messageID, DateOnly date)
		{
			_id = id;
			_userCNP = userCNP;
			_tipID = tipID;
			_messageID = messageID;
			_date = date;
		}
		public GivenTip()
		{
			_id = 0;
			_userCNP = string.Empty;
			_tipID = 0;
			_messageID = 0;
			_date = new DateOnly();
		}
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public string UserCNP
		{
			get { return _userCNP; }
			set { _userCNP = value; }
		}

		public int TipID
		{
			get { return _tipID; }
			set { _tipID = value; }
		}

		public int MessageID
		{
			get { return _messageID; }
			set { _messageID = value; }
		}

		public DateOnly Date
		{
			get { return _date; }
			set { _date = value; }
		}

	}
}
