using System.Collections.Generic;
using Launcher.Update;

namespace Launcher
{
    public class Settings
    {
        public Settings()
        {
            Updater_Streams = new List<ReleaseStream>();
        }

        public bool Telemetry_Enabled { get; set; }
        public bool Telemetry_Info { get; set; }
        public bool Telemetry_Debug { get; set; }
        public bool Telemetry_Error { get; set; }
        public bool Telemetry_Fatal { get; set; }

        public bool Updater_Enabled { get; set; }
        public List<ReleaseStream> Updater_Streams { get; set; }
    }
}