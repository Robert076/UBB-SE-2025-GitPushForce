using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src.Model;


namespace src.View.Pages
{
    public sealed partial class AnalysisWindow : Window
    {
        User user;
        public AnalysisWindow(User selectedUser)
        {
            this.InitializeComponent();
            user = selectedUser;
            LoadUserData();
        }

        public void LoadUserData()
        {
            FirstNameTextBlock.Text = $"{user.FirstName}";
            LastNameTextBlock.Text = $"{user.LastName}";
            CNPTextBlock.Text = $"{user.CNP}";
            EmailTextBlock.Text = $"{user.Email}";
            PhoneNumberTextBlock.Text = $"{user.PhoneNumber}";
        }
    }
}
