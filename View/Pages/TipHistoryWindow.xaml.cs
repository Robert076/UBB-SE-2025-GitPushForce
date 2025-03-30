using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using src.Model;
using src.Repos;
using src.View.Components;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.UI.Xaml;
using src.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using src.Services;

namespace src.View.Pages
{
    public sealed partial class TipHistoryWindow : Window
    {
        private User selectedUser;

        public TipHistoryWindow(User selectedUser)
        {
            this.InitializeComponent();
            this.selectedUser = selectedUser;
            DatabaseConnection dbConn = new DatabaseConnection();

            SqlParameter[] messageParameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCNP", selectedUser.CNP)
            };

            DataTable messagesRows = dbConn.ExecuteReader("GetMessagesForGivenUser", messageParameters, CommandType.StoredProcedure);
            List<Message> messages = new List<Message>();

            foreach (DataRow row in messagesRows.Rows)
            {
                messages.Add(new Message
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Type = row["Type"].ToString(),
                    MessageText = row["Message"].ToString()
                });
            }
            SqlParameter[] tipsParameters = new SqlParameter[]
            {
                 new SqlParameter("@UserCNP", selectedUser.CNP)
            };
            DataTable tipsRows = dbConn.ExecuteReader("GetTipsForGivenUser", tipsParameters, CommandType.StoredProcedure);
            List<Tip> tips = new List<Tip>();
            foreach (DataRow row in tipsRows.Rows)
            {
                tips.Add(new Tip
                {
                    Id = Convert.ToInt32(row["ID"]),
                    CreditScoreBracket = row["CreditScoreBracket"].ToString(),
                    TipText = row["TipText"].ToString()
                });
            }
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
