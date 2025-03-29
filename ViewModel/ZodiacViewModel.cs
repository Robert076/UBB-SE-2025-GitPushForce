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


    }
}
