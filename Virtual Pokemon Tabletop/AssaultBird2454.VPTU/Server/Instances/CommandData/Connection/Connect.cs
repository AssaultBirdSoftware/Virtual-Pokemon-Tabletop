using AssaultBird2454.VPTU.Networking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Connection
{
    public enum ConnectionStatus { OK = 0, Rejected = 1, ServerFull = 2, Old_Client = 3, Old_Server = 4 }

    public class Connect : NetworkCommand
    {
        public string Command { get { return "ConnectionState"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public ConnectionStatus Connection_State { get; set; }
        public string Version { get; set; }
        public string Commit { get; set; }
    }
}
