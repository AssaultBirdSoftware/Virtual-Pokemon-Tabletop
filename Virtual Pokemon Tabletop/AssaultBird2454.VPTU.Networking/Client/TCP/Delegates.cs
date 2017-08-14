using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    public delegate void TCP_ConnectionState_Handeler(Data.Client_ConnectionStatus ConnectionState);

    public delegate void TCP_Data(string Data, DataDirection Direction);

    public delegate void TCP_Data_Error(Exception ex, DataDirection Direction);
}
