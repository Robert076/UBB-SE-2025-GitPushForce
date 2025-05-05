using Microsoft.UI.Xaml;
using System;
using Src.Helpers;  

namespace Src.Services
{
    public class DispatcherTimerWrapper : IDispatchTimer  // Use your custom ITimer
    {
        private readonly DispatcherTimer _dispatcherTimer;

        public DispatcherTimerWrapper()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += (sender, args) =>
            {
                // Use proper EventArgs (you can pass EventArgs.Empty if needed)
                Tick?.Invoke(sender, EventArgs.Empty);
            };
        }

        public void Start() => _dispatcherTimer.Start();
        public void Stop() => _dispatcherTimer.Stop();
        public event EventHandler Tick;
    }
}
