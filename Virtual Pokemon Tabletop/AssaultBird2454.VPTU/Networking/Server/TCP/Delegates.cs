using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public delegate void TCP_AcceptClients_Handeler(bool Accepting_Connections);

    public delegate void TCP_ClientState_Handeler(TCP_ClientNode Client, Data.Client_ConnectionStatus Client_State);

    public delegate void TCP_ServerState_Handeler(Data.Server_Status Server_State);
    
    public delegate void TCP_Data(string Data, TCP_ClientNode Client, DataDirection Direction);

    public delegate void TCP_Data_Error(Exception ex, DataDirection Direction);
}
