using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using src.Repositories;

namespace src
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of UserRepository
            UserRepository userRepository = new UserRepository();

            // Call the GetUserName method with an Id (e.g., 1)
            string userName = await Task.Run(() => userRepository.GetUserName(1));

            // Log the result to the output window
            Debug.WriteLine($"User name for ID 1: {userName}");

            // Display the result in the TextBlock
            myTextBlock.Text = userName ?? "User not found or name is null";
        }
    }
}
