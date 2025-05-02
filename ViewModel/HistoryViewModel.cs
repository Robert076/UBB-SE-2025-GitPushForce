using src.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace src.ViewModel
{
    class HistoryViewModel : INotifyPropertyChanged
    {
        private IHistoryService _historyService;

        public event PropertyChangedEventHandler PropertyChanged;

        public HistoryViewModel(IHistoryService historyService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
