using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Src.Services;

namespace Src.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private IHistoryService historyService;

        public event PropertyChangedEventHandler PropertyChanged;

        public HistoryViewModel(IHistoryService historyService)
        {
            this.historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
