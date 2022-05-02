using System;
using System.IO;
using System.Text.Json;
using PowerOffTimer.Models;

namespace PowerOffTimer.Services
{
    public class SetingsService
    {
        public const string FileName = "powerofftimer.cfg";
        public readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public SetingsService(string path)
        {
            _path = path;
        }

        public SetingsService()
        {
        }

        public void SaveSettings(TimerState timerState)
        {
            using var stream = File.Create(Path.Combine(_path, FileName));
            JsonSerializer.Serialize(stream, timerState);
        }

        public TimerState LoadSettings()
        {
            TimerState state = new();

            if (File.Exists(Path.Combine(_path, FileName)))
            {
                using var streamReader = File.OpenRead(Path.Combine(_path, FileName));
                state = JsonSerializer.Deserialize<TimerState>(streamReader) ?? new();
            }

            return state;
        }
    }
}
