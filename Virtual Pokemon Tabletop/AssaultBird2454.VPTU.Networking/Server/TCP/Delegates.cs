using System;
using AssaultBird2454.VPTU.Networking.Data;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public delegate void TCP_AcceptClients_Handeler(bool Accepting_Connections);

    public delegate void TCP_ClientState_Handeler(TcpClientNode Client, Client_ConnectionStatus Client_State);

    public delegate void TCP_ServerState_Handeler(Server_Status Server_State);

    public delegate void TCP_Data(string Data, TcpClientNode Client, DataDirection Direction);

    public delegate void TCP_Data_Error(Exception ex, DataDirection Direction);
}