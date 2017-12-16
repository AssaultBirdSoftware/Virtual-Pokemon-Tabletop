using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Connection
{
    public enum ConnectionStatus { OK = 0, Rejected = 1, ServerFull = 2, Old_Client = 3, Old_Server = 4 }

    public class Connect : Networking.Data.NetworkCommand
    {
        public string Command { get { return "ConnectionState"; } }

        public ConnectionStatus Connection_State { get; set; }
        public string Version { get; set; }
        public string Commit { get; set; }
    }
}
