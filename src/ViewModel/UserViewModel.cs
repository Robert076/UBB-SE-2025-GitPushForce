using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Src.Model;
using Src.Services;

namespace Src.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private IUserService userService;
        public ObservableCollection<User> Users { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public UserViewModel(IUserService userServices)
        {
            this.userService = userServices ?? throw new ArgumentNullException(nameof(userServices));
            Users = new ObservableCollection<User>();

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadUsers()
        {
            try
            {
                var users = userService.GetUsers();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}