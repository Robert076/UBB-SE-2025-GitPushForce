using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Src.Services;

namespace Src.ViewModel
{
    public class ActivityViewModel : INotifyPropertyChanged
    {
        private IActivityService activityService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ActivityViewModel(IActivityService activityService)
        {
            this.activityService = activityService ?? throw new ArgumentNullException(nameof(activityService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
