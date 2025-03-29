using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using src.Services;

namespace src.ViewModels
{
    public class ZodiacViewModel : INotifyPropertyChanged
    {
        private readonly ZodiacService _zodiacService;
        private string _userCNP;
        private string _joke;
        private int _creditScore;
        private string _statusMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public ZodiacViewModel(ZodiacService zodiacService)
        {
            _zodiacService = zodiacService ?? throw new ArgumentNullException(nameof(zodiacService));
            UpdateCreditScoreBasedOnJokeCommand = new RelayCommand(UpdateCreditScoreBasedOnJoke);
            UpdateCreditScoreBasedOnAttributeCommand = new RelayCommand(UpdateCreditScoreBasedOnAttribute);
        }


        public string UserCNP
        {
            get => _userCNP;
            set
            {
                _userCNP = value;
                OnPropertyChanged();
            }
        }

        public string Joke
        {
            get => _joke;
            set
            {
                _joke = value;
                OnPropertyChanged();
            }
        }

        public int CreditScore
        {
            get => _creditScore;
            set
            {
                _creditScore = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }


        public ICommand UpdateCreditScoreBasedOnJokeCommand { get; }
        public ICommand UpdateCreditScoreBasedOnAttributeCommand { get; }


        private void UpdateCreditScoreBasedOnJoke()
        {
            try
            {
                _zodiacService.CreditScoreModificationBaseOnJokeAndCoinFlip(UserCNP, Joke);

                StatusMessage = "Credit score updated based on joke and coin flip.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error updating credit score based on joke: {ex.Message}";
            }
        }

        private void UpdateCreditScoreBasedOnAttribute()
        {
            try
            {
                _zodiacService.CreditScoreModificationBadeOnAtributeAndGravity(UserCNP);
                StatusMessage = "Credit score updated based on attribute and gravity.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error updating credit score based on attribute: {ex.Message}";
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
