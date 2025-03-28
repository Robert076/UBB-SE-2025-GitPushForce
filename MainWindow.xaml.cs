using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using src.Views; // Import Views namespace

namespace src
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(ChatReportView));
        }

        private void ChatReportsButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(ChatReportView)); 
        }

       
    }
}
