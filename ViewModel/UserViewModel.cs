using src.Model;
using src.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace src.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private UserService _userServices;
        public ObservableCollection<User> Users { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public UserViewModel(UserService userServices)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadUsers()
        {
            try
            {
                var users = _userServices.GetUsers();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}