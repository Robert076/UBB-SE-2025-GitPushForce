using System.Collections.Generic;
using src.Model;
using src.Repos;
using src.View.Components;
using Microsoft.UI.Xaml;

namespace src.View.Pages
{
    public sealed partial class TipHistoryWindow : Window
    {
        private User _selectedUser;
        MessagesRepository _messagesRepository;
        TipsRepository _tipsRepository;

        public TipHistoryWindow(User selectedUser, MessagesRepository messagesRepository, TipsRepository tipsRepository)
        {
            this.InitializeComponent();
            _selectedUser = selectedUser;
            _messagesRepository = messagesRepository;
            _tipsRepository = tipsRepository;

            List<Message> messages = _messagesRepository.GetMessagesForGivenUser(selectedUser.Cnp);
            List<Tip> tips = _tipsRepository.GetTipsForGivenUser(selectedUser.Cnp);

            LoadHistory(tips);
            LoadHistory(messages);
        }

        private void LoadHistory(List<Message> messages)
        {
            foreach (Message message in messages)
            {
                MessageHistoryComponent messageComponent = new MessageHistoryComponent();
                messageComponent.setMessageData(message);
                MessageHistoryContainer.Items.Add(messageComponent);
            }
        }

        private void LoadHistory(List<Tip> tips)
        {
            foreach (Tip tip in tips)
            {
                TipHistoryComponent tipComponent = new TipHistoryComponent();
                tipComponent.setTipData(tip);
                TipHistoryContainer.Items.Add(tipComponent);
            }
        }
    }
}
