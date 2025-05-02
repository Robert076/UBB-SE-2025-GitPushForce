using System.Collections.ObjectModel;
using System.Threading.Tasks;
using src.Services;
using src.Model;
using src.Data;
using src.Repos;
using System;

namespace src.ViewModel
{
    public class ChatReportsViewModel
    {
        private readonly IChatReportService _chatReportService;

        public ObservableCollection<ChatReport> ChatReports { get; set; }

        public ChatReportsViewModel()
        {
            ChatReports = new ObservableCollection<ChatReport>();
        }

        public async Task LoadChatReports()
        {
            try
            {
                var reports = _chatReportService.GetChatReports();
                foreach (var report in reports)
                {
                    ChatReports.Add(report);
                }
            }
            catch (Exception exception)
            { 
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
