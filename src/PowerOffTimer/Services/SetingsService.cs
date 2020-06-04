using System;
using System.IO;
using Newtonsoft.Json;
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
            using (StreamWriter streamWriter = File.CreateText(Path.Combine(_path, FileName)))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(streamWriter, timerState);
            }
        }

        public TimerState LoadSettings()
        {
            TimerState state = null;

            if (File.Exists(Path.Combine(_path, FileName)))
            {
                using (StreamReader streamReader = File.OpenText(Path.Combine(_path, FileName)))
                {
                    var serializser = new JsonSerializer();
                    state = (TimerState)serializser.Deserialize(streamReader, typeof(TimerState));
                }
            }

            return state;
        }
    }
}
