using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    public class TCP_Client
    {
        #region Events

        #endregion

        #region Variables
        private TcpClient Client;
        private SslStream SSL;
        private IPAddress TCP_IPAddress;
        private int TCP_Port;

        private byte[] Tx;
        private byte[] Rx;

        public Action<string> CommandHandeler;
        #endregion

        #region Variable Handelers
        /// <summary>
        /// Get or Set the IPAddress. IPAddress wont be affected if client is connected
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return TCP_IPAddress;
            }
            set
            {
                //Event Trigger Here
                TCP_IPAddress = value;
            }
        }

        /// <summary>
        /// Get or Set the Port. Port wont be affected if client is connected
        /// </summary>
        public int Port
        {
            get
            {
                return TCP_Port;
            }
            set
            {
                //Event Trigger
                TCP_Port = value;
            }
        }
        #endregion

        public TCP_Client(IPAddress Address, Action<string> _CommandHandeler, int Port = 25444)
        {
            TCP_IPAddress = Address;// Sets the IPAddress
            TCP_Port = Port;// Sets the Port
            CommandHandeler = _CommandHandeler;// Sets the Command Callback
        }

        #region Client Methods
        /// <summary>
        /// Connects to the server, it will also disconnect any connections
        /// </summary>
        public void Connect()
        {
            Disconnect();// Disconnects (No Error if it was never connected)

            // Execute Connection State Change

            Client = new TcpClient();// Creates a new client object

            Client.BeginConnect(TCP_IPAddress, TCP_Port, Client_Connected, null);// Connects to the server and on connect will call the connect call back

            Tx = new byte[32768];
            Rx = new byte[32768];
        }

        /// <summary>
        /// Disconnects the Client from the server
        /// </summary>
        public void Disconnect()
        {
            try
            {
                Client.Close();// Closes connection
                Client = null;// Clears Client

                // Execute Connection State Change
            }
            catch
            {
                /* Failed to disconnect or was never connected to anything */
            }
        }
        #endregion

        #region Data Events
        private void Client_DataRecv(IAsyncResult ar)
        {

        }
        #endregion

        #region Connection Events
        private void Client_Connected(IAsyncResult ar)
        {
            SSL = new SslStream(Client.GetStream(), false, ValidateCert);// Encrypts connection with SSL
            Client.GetStream().BeginRead(Rx, 0, 32768, Client_DataRecv, Client);
            // Execute Connection State Change
        }

        private bool ValidateCert(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;// Allow all certs
        }
        #endregion
    }
}
