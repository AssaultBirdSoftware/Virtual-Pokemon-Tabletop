using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    public class Settings
    {
        public Settings()
        {
            Updater_Streams = new List<Update.ReleaseStream>();
        }

        public bool Telemetry_Enabled { get; set; }
        public bool Telemetry_Info { get; set; }
        public bool Telemetry_Debug { get; set; }
        public bool Telemetry_Error { get; set; }
        public bool Telemetry_Fatal { get; set; }

        public bool Updater_Enabled { get; set; }
        public List<Update.ReleaseStream> Updater_Streams { get; set; }
    }
}
