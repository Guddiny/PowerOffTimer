using System;
using PowerOffTimer.Models;
using PowerOffTimer.Services;
using ReactiveUI;

namespace PowerOffTimer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _hours;
        private int _minutes;
        private int _seconds;

        private TimerState _timerState;

        private DateTime GenerateInitialTime(int hours, int minutes, int seconds)
        {
            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            return new DateTime(1990, 1, 1).AddSeconds(totalSeconds);
        }

        public PowerOffService PowerOffService { get; set; }

        public SetingsService SettingsService { get; set; }

        public int Hours
        {
            get => _hours;
            set => this.RaiseAndSetIfChanged(ref _hours, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => this.RaiseAndSetIfChanged(ref _minutes, value);
        }

        public int Seconds
        {
            get => _seconds;
            set => this.RaiseAndSetIfChanged(ref _seconds, value);
        }

        public MainWindowViewModel()
        {
            PowerOffService = new PowerOffService();
            SettingsService = new SetingsService();

            var settings = SettingsService.LoadSettings();

            _timerState = settings ?? new TimerState();

            Hours = _timerState.Hours;
            Minutes = _timerState.Minutes;
            Seconds = _timerState.Seconds;
        }

        public void StartTimer()
        {
            SettingsService.SaveSettings(new TimerState
            {
                Hours = Hours,
                Minutes = Minutes,
                Seconds = Seconds
            });

            PowerOffService.ScheduledTime = GenerateInitialTime(Hours, Minutes, Seconds);
            PowerOffService.StartTimer();
        }

        public void StopTimer() => PowerOffService.StopTimer();

        public void RestartTimer() => PowerOffService.RestartTimer();
    }
}
