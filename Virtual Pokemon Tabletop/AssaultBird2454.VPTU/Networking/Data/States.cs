using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    public enum Client_ConnectionStatus { Connecting = 1, Connected = 2, Disconnected = 3, Dropped = 4, Encrypted = 5 }
    public enum Server_Status { Offline = 0, Starting = 1, Online = 2}
}
