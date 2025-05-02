using src.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace src.ViewModel
{
    public class ActivityViewModel : INotifyPropertyChanged
    {
        private IActivityService _activityService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ActivityViewModel(IActivityService activityService)
        {
            _activityService = activityService ?? throw new ArgumentNullException(nameof(activityService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
