using AssaultBird2454.VPTU.Server.Instances;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public class Server
    {
        public string Server_ID { get; set; }
        public string Server_Name { get; set; }
        public int Server_Port { get; set; }
        public string SaveFile { get; set; }

        [JsonIgnore]
        public ServerInstance Server_Instance;
        [JsonIgnore]
        public ListViewItem LVI;
    }
}
