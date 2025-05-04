using Microsoft.UI.Xaml.Controls;
using Src.Model;

namespace Src.View.Components
{
    public sealed partial class MessageHistoryComponent : Page
    {
        public Message Message;

        public MessageHistoryComponent()
        {
            this.InitializeComponent();
        }

        public void SetMessageData(Message givenMessage)
        {
            Message = givenMessage;
            MessageTypeTextBlock.Text = $"Type: {Message.Type}";
            MessageTextBlock.Text = $"{Message.MessageText}";
        }
    }
}
