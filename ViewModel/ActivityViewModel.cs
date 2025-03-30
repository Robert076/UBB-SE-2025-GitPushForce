using src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.VoiceCommands;

namespace src.ViewModel
{
    public class ActivityViewModel : INotifyPropertyChanged
    {
        private ActivityService _activityService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ActivityViewModel(ActivityService activityService)
        {
            _activityService = activityService ?? throw new ArgumentNullException(nameof(activityService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
