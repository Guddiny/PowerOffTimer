using System;
using System.Diagnostics;
using System.Timers;
using ReactiveUI;

namespace PowerOffTimer.Services
{
    public class PowerOffService : ReactiveObject
    {
        private const int TimerInterval = 1000;

        private DateTime _scheduledTime;
        private DateTime _currentTime;
        private bool _isInProgress;
        private Timer _timer;
        private double _secondsPassed;
        private double _totalSeconds;

        private DateTime GetInitialDate()
        {
            return new DateTime(1990, 1, 1, 0, 0, 0);
        }


        public double SecondsPassed
        {
            get => _secondsPassed;
            set => this.RaiseAndSetIfChanged(ref _secondsPassed, value);
        }

        public double TotalSeconds
        {
            get => _totalSeconds;
            set => this.RaiseAndSetIfChanged(ref _totalSeconds, value);
        }

        public DateTime ScheduledTime
        {
            get => _scheduledTime;
            set => this.RaiseAndSetIfChanged(ref _scheduledTime, value);
        }

        public DateTime CurrentTime
        {
            get => _currentTime;
            set => this.RaiseAndSetIfChanged(ref _currentTime, value);
        }

        public bool IsInProgress
        {
            get => _isInProgress;
            set => this.RaiseAndSetIfChanged(ref _isInProgress, value);
        }


        public void StartTimer()
        {
            _timer = new Timer(TimerInterval);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();

            IsInProgress = true;
            CurrentTime = GetInitialDate();

            TotalSeconds = (ScheduledTime - CurrentTime).TotalSeconds;
            SecondsPassed = 0;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentTime = CurrentTime.AddSeconds(1);
            SecondsPassed++;

            if (CurrentTime >= ScheduledTime)
            {
                StopTimer();
#if RELEASE
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
#endif
            }
        }

        public void StopTimer()
        {
            _timer.Stop();
            CurrentTime = GetInitialDate();
            SecondsPassed = 0;
            IsInProgress = false;
        }

        public void RestartTimer()
        {
            StopTimer();
            StartTimer();
        }
    }
}
