using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Update
{
    public enum ReleaseStream { Alpha, Beta, Master }
    internal class Data
    {
        public string Version_ID { get; set; }
        public ReleaseStream Version_Type { get; set; }
        public string Commit_ID { get; set; }
        public string Version_Name { get; set; }
        public string Download_URL { get; set; }
    }
}
