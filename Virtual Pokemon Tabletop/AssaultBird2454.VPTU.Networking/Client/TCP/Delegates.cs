using System;
using AssaultBird2454.VPTU.Networking.Data;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    public delegate void TCP_ConnectionState_Handeler(Client_ConnectionStatus ConnectionState);

    public delegate void TCP_Data(string Data, DataDirection Direction);

    public delegate void TCP_Data_Error(Exception ex, DataDirection Direction);
}