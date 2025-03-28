using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Services;  // Ensure the correct namespace for your services
using src.Model;
using src.Data;
using src.Repos;

namespace src.View.Components
{
    public sealed partial class ChatReportComponent : Page
    {
        private readonly ChatReportService _chatReportService;

        // Constructor
        public ChatReportComponent()
        {
            this.InitializeComponent();
            _chatReportService = new ChatReportService(new ChatReportRepository(new DatabaseConnection()));
        }

        // Properties to bind data

        public int ReportId { get; set; }
        public string ReportedUserCNP { get; set; }
        public string ReportedMessage { get; set; }

        // This method will be called when the "Solve" button is clicked
        private async void OnSolveClick(object sender, RoutedEventArgs e)
        {
            // Create the ChatReport object
            var chatReport = new ChatReport
            {
                Id = ReportId,
                ReportedMessage = ReportedMessage
            };

            // Call the service to solve the chat report
            bool isSolved = await _chatReportService.SolveChatReport(chatReport);

            // Show a notification based on the result
            if (isSolved)
            {
                ShowNotification("Report has been solved! The message was offensive.");
            }
            else
            {
                ShowNotification("Report has been solved! The message was not offensive.");
            }
        }

        // Display a notification using ContentDialog
        private void ShowNotification(string message)
        {
            var notificationDialog = new ContentDialog()
            {
                Title = "Notification",
                Content = message,
                CloseButtonText = "OK"
            };
            notificationDialog.ShowAsync();
        }

        // Method to set data in the component
        public void SetData(int id, string reportedUserCnp, string reportedMessage)
        {
            ReportId = id;
            ReportedUserCNP = reportedUserCnp;
            ReportedMessage = reportedMessage;

            // Update UI elements based on the data
            ReportedUserCNPTextBlock.Text = ReportedUserCNP;
            ReportedMessageTextBlock.Text = ReportedMessage;
        }
    }
}
