using System.Collections.Generic;
using Src.Model;
using Src.Repos;
using Src.View.Components;
using Microsoft.UI.Xaml;

namespace Src.View.Pages
{
    public sealed partial class TipHistoryWindow : Window
    {
        private User selectedUser;
        private readonly MessagesRepository messagesRepository;
        private readonly TipsRepository tipsRepository;

        public TipHistoryWindow(User selectedUser, MessagesRepository messagesRepository, TipsRepository tipsRepository)
        {
            this.InitializeComponent();
            this.selectedUser = selectedUser;
            this.messagesRepository = messagesRepository;
            this.tipsRepository = tipsRepository;

            List<Message> messages = this.messagesRepository.GetMessagesForGivenUser(selectedUser.Cnp);
            List<Tip> tips = this.tipsRepository.GetTipsForGivenUser(selectedUser.Cnp);

            LoadHistory(tips);
            LoadHistory(messages);
        }

        private void LoadHistory(List<Message> messages)
        {
            foreach (Message message in messages)
            {
                MessageHistoryComponent messageComponent = new MessageHistoryComponent();
                messageComponent.SetMessageData(message);
                MessageHistoryContainer.Items.Add(messageComponent);
            }
        }

        private void LoadHistory(List<Tip> tips)
        {
            foreach (Tip tip in tips)
            {
                TipHistoryComponent tipComponent = new TipHistoryComponent();
                tipComponent.SetTipData(tip);
                TipHistoryContainer.Items.Add(tipComponent);
            }
        }
    }
}
