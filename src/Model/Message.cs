namespace Src.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string MessageContent { get; set; }

        public Message(int id, string type, string message)
        {
            Id = id;
            Type = type;
            MessageContent = message;
        }

        public Message()
        {
            Id = 0;
            Type = string.Empty;
            MessageContent = string.Empty;
        }

        public string MessageText
        {
            get { return MessageContent; }
            set { MessageContent = value; }
        }
    }
}
