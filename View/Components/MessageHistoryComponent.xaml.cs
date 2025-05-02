using Microsoft.UI.Xaml.Controls;
using src.Model;

namespace src.View.Components
{
    public sealed partial class MessageHistoryComponent : Page
    {
        public Message message;

        public MessageHistoryComponent()
        {
            this.InitializeComponent();
        }

        public void setMessageData(Message givenMessage)
        {
            message = givenMessage;
            MessageTypeTextBlock.Text = $"Type: {message.Type}";
            MessageTextBlock.Text = $"{message.MessageText}";
        }
    }
}
