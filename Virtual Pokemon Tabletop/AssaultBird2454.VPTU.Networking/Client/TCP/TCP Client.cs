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
        private IPAddress TCP_IPAddress;
        private int TCP_Port;

        private byte[] Tx;
        private byte[] Rx;
        private string Data = "";

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

        /// <summary>
        /// Checks if the client is connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if(Client != null)
                {
                    return Client.Connected;
                }
                else
                {
                    return false;
                }
                
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
            int DataLength = 0;

            try
            {
                DataLength = Client.GetStream().EndRead(ar);// Gets the length and ends read

                if (DataLength == 0)// If Data has nothing in it
                {
                    Disconnect();// Disconnects
                    return;
                }

                if (Data == null) { Data = ""; }// Checks to see if it is null and if it is them set it to an empty string
                
                Data = Data + Encoding.UTF8.GetString(Rx, 0, DataLength).Trim();// Gets the data and trims it
                if (Data.EndsWith("|<EOD>|"))
                {
                    //TCP_Data_Event.Invoke(Data, DataDirection.Recieve);// Fires the Data Recieved Event
                    CommandHandeler.Invoke(Data.Remove(Data.Length - 7, 7));// Executes the command handeler
                    Data = "";// Data Recieved
                }

                Rx = new byte[32768];//Sets the clients recieve buffer
                Client.GetStream().BeginRead(Rx, 0, Rx.Length, Client_DataRecv, Client);//Starts to read again
            }
            catch
            {
                // Client Dropped
            }
        }

        private void Client_DataTran(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpc = (TcpClient)ar.AsyncState;//Gets the client data is going to
                tcpc.GetStream().EndWrite(ar);//Ends client write stream
            }
            catch (Exception e)
            {
                //TCP_Data_Error_Event.Invoke(e, DataDirection.Send);
                /* Transmition Error */
            }
        }

        public void SendData(string Data)
        {
            if (IsConnected)
            {
                Tx = Encoding.UTF8.GetBytes(Data + "|<EOD>|");
                Client.GetStream().BeginWrite(Tx, 0, Tx.Length, Client_DataTran, Client);
                Tx = new byte[32768];
            }
        }
        #endregion

        #region Connection Events
        private void Client_Connected(IAsyncResult ar)
        {
            Client.GetStream().BeginRead(Rx, 0, 32768, Client_DataRecv, Client);
            // Execute Connection State Change
        }
        #endregion
    }
}
