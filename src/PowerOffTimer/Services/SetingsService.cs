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

        public SetingsService(string path = null)
        {
            if (path != null)
            {
                _path = path;
            }
        }

        public void SaveSettings(TimerState timerState)
        {
            using var stream = File.Create(Path.Combine(_path, FileName));
            JsonSerializer.Serialize(stream, timerState);
        }

        public TimerState LoadSettings()
        {
            TimerState state = null;

            if (File.Exists(Path.Combine(_path, FileName)))
            {
                using var streamReader = File.OpenRead(Path.Combine(_path, FileName));
                state = JsonSerializer.Deserialize<TimerState>(streamReader);
            }

            return state;
        }
    }
}
