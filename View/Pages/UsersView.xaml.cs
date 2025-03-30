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
using src.Data;
using src.ViewModel;
using src.Repos;
using src.Services;
using src.View.Components;
using src.Model;



namespace src.Views
{
    public sealed partial class UsersView : Page
    {

        public UsersView()
        {
            this.InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersContainer.Items.Clear();

            DatabaseConnection dbConn = new DatabaseConnection();
            UserRepository repo = new UserRepository(dbConn);
            UserService service = new UserService(repo);
            UserViewModel userViewModel = new UserViewModel(service);

            try
            {
                List<User> users = service.GetUsers();
                foreach (var user in users)
                {
                    UserInfoComponent userComponent = new UserInfoComponent();
                    userComponent.SetUserData(user);
                    UsersContainer.Items.Add(userComponent);
                }
            }
            catch (Exception)
            {
                UsersContainer.Items.Add("There are no users to display.");
            }
        }
    }
}
