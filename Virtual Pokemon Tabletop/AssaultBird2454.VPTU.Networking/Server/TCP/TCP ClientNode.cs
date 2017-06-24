using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public class TCP_ClientNode
    {
        #region Networking
        internal TcpClient Client { get; set; }// TCP Client
        internal Socket Socket { get; set; }// Conection Socket
        internal SslStream SSL { get; set; }// Client SSL Socket
        internal byte[] Tx { get; set; }// Transmit Buffer
        internal byte[] Rx { get; set; }// Recieve Buffer
        internal string Data { get; set; }// Processed TCP Data gets stored here...
        public string ID { get; set; }// Connection ID (Connection Endpoint)
        public TCP_Server Server { get; set; }// A Refrence to the server it belongs to
        #endregion

        public TCP_ClientNode(TcpClient _Client, string _ID, TCP_Server _Server, X509Certificate Cert)
        {
            Server = _Server;// Sets the server
            Client = _Client;// Sets the client
            Socket = _Client.Client;// Sets the socket
            ID = _ID;// Sets the ID

            Tx = new byte[32768];// 32768
            Rx = new byte[32768];// 32768

            SSL = new SslStream(Client.GetStream());// Sets the SSL Stream
            SSL.AuthenticateAsServerAsync(Cert, false, System.Security.Authentication.SslProtocols.Tls, false);
        }

        /// <summary>
        /// Sends data to this client, No Encryption or Serialization is performed at this step
        /// </summary>
        /// <param name="Data">The String being transmitted</param>
        public void Send(string Data)
        {
            Tx = Encoding.UTF8.GetBytes(Data + @"|<EOD>|");//Gets Bytes and adds |<EOD>| as a end of data marker
            Client.GetStream().BeginWrite(Tx, 0, Tx.Length, Server.OnWrite, Client);//Sends Encrypted data to client
            Tx = new byte[32768];
        }
    }
}