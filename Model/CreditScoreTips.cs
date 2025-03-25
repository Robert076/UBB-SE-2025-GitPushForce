using System;

namespace src.Model
{
	public class GivenTips
	{
		private int _id;
		private string _userCNP;
		private int _tipID;
		private int _messageID;
		private DateTime _date;

		public GivenTips(int id, string userCNP, int tipID, int messageID, int date)
		{
			_id = id;
			_userCNP = userCNP;
			_tipID = tipID;
			_messageID = messageID;
			_date = date;
		}
		public CreditScore()
		{
			_id = 0;
			_UserCNP = string.Empty;
			_TipID = 0;
			_messageID = 0;
			_Date = new DateOnly;
		}
		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public string UserCNP
		{
			get { return _UserCNP; }
			set { _UserCNP = value; }
		}

		public int TipID
		{
			get { return _TipID; }
			set { _TipID = value; }
		}

		public int MessageID
		{
			get { return _messageID; }
			set { _messageID = value; }
		}

		public int Date
		{
			get { return _Date; }
			set { _Date = value; }
		}

	}