using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    public enum Client_ConnectionStatus { Connected = 1, Disconnected = 2, Dropped = 3, Rejected = 4, ServerFull = 5 }
    public enum Server_Status { Offline = 0, Starting = 1, Online = 2, Stopping = 3}
}
