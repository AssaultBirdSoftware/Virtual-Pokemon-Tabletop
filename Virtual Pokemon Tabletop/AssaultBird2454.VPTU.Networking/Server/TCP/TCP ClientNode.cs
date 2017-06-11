using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public class TCP_ClientNode
    {
        #region Networking
        internal TcpClient Client { get; set; }
        internal Socket Socket { get; set; }
        internal byte[] Tx { get; set; }
        internal byte[] Rx { get; set; }
        public string ID { get; set; }
        #endregion

        public TCP_ClientNode(TcpClient _Client, string _ID)
        {
            Client = _Client;// Sets the client
            Socket = _Client.Client;// Sets the socket
            ID = _ID;// Sets the ID

            Tx = new byte[512];
            Rx = new byte[512];
        }
    }
}
