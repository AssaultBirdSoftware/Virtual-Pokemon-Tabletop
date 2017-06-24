using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public enum DataDirection { Send, Recieve }

    #region Event Delegates
    public delegate void TCP_AcceptClients_Handeler(bool Accepting_Connections);
    public delegate void TCP_ClientState_Handeler(TCP_ClientNode Client, Data.Client_ConnectionStatus Client_State);
    public delegate void TCP_ServerState_Handeler(Data.Server_Status Server_State);

    public delegate void TCP_Data(string Data, TCP_ClientNode Client, DataDirection Direction);
    public delegate void TCP_Data_Error(Exception ex, DataDirection Direction);
    #endregion

    public class TCP_Server
    {
        #region Events
        /// <summary>
        /// An event that fires when the server starts or stops accepting connection requests
        /// </summary>
        public event TCP_AcceptClients_Handeler TCP_AcceptClients_Changed;

        /// <summary>
        /// An event that fires when a clients connection state changes
        /// </summary>
        public event TCP_ClientState_Handeler TCP_ClientState_Changed;

        /// <summary>
        /// An event that fires when a server state changes
        /// </summary>
        public event TCP_ServerState_Handeler TCP_ServerState_Changed;

        /// <summary>
        /// An event thst fires when data is sent or recieved
        /// </summary>
        public event TCP_Data TCP_Data_Event;

        /// <summary>
        /// An event that is fired when a data error occures
        /// </summary>
        public event TCP_Data_Error TCP_Data_Error_Event;

        #region Trigger Event Methods
        protected void Fire_TCP_AcceptClients_Changed(bool Accepting_Connections)
        {
            TCP_AcceptClients_Changed?.Invoke(Accepting_Connections);
        }
        protected void Fire_TCP_ClientState_Changed(TCP_ClientNode Client, Data.Client_ConnectionStatus Client_State)
        {
            TCP_ClientState_Changed?.Invoke(Client, Client_State);
        }
        protected void Fire_TCP_ServerState_Changed(Data.Server_Status Server_State)
        {
            TCP_ServerState_Changed?.Invoke(Server_State);
        }
        protected void Fire_TCP_Data_Event(string Data, TCP_ClientNode Client, DataDirection Direction)
        {
            TCP_Data_Event?.Invoke(Data, Client, Direction);
        }
        protected void Fire_TCP_Data_Error_Event(Exception ex, DataDirection Direction)
        {
            TCP_Data_Error_Event?.Invoke(ex, Direction);
        }
        #endregion
        #endregion

        #region Variables
        /// <summary>
        /// The Server Object
        /// </summary>
        private TcpListener Listener;
        /// <summary>
        /// A List object containing all the client's connected
        /// </summary>
        private List<TCP_ClientNode> ClientNodes;

        private bool TCP_AcceptClients;// Server Accept Connsctions
        private IPAddress TCP_ServerAddress;// Servers Address
        private int TCP_ServerPort;// Servers Port
        private int TCP_MaxConnections;// Servers Max Client Connections

        private X509Certificate SSLCert;// SSL Certificate

        public Action<TCP_ClientNode, string> CommandHandeler;
        #endregion

        #region Variable Handelers
        /// <summary>
        /// A Setting that tells the server if it is allowed to accept any more clients
        /// </summary>
        public bool AcceptClients
        {
            get
            {
                return TCP_AcceptClients;
            }
            set
            {
                Fire_TCP_AcceptClients_Changed(value);
                TCP_AcceptClients = value;
            }
        }

        /// <summary>
        /// A Setting that defines the ipaddress that the server should listen to for incoming connections, These changes will take no effect when the server is running...
        /// </summary>
        public IPAddress ServerAddress
        {
            get
            {
                return TCP_ServerAddress;
            }
            set
            {
                TCP_ServerAddress = value;
            }
        }

        /// <summary>
        /// A Setting that defines what port that the server opperates on
        /// </summary>
        public int ServerPort
        {
            get
            {
                return TCP_ServerPort;
            }
            set
            {
                TCP_ServerPort = value;
            }
        }

        /// <summary>
        /// A Setting that defines the maxamum amount of client connections
        /// </summary>
        public int MaxConnections
        {
            get
            {
                return TCP_MaxConnections;
            }
            set
            {
                TCP_MaxConnections = value;
            }
        }
        #endregion

        public TCP_Server(IPAddress Address, Action<TCP_ClientNode, string> _CommandHandeler, int Port = 25444, X509Certificate _SSLCertificate = null)
        {
            TCP_ServerAddress = Address;
            TCP_ServerPort = Port;// Sets the port
            TCP_AcceptClients = true;// Allows clients to connect
            CommandHandeler = _CommandHandeler;// Sets the Command Callback

            if (_SSLCertificate == null)
            {
                
            }
            else
            {
                SSLCert = _SSLCertificate;// Set the cert
            }
        }

        #region Server Methods
        /// <summary>
        /// Start the server using the settings that have already been set
        /// </summary>
        public void Start()
        {
            try { Stop(); } catch { }// Executes the stop server method to check that it is not running and to clear it

            Fire_TCP_ServerState_Changed(Data.Server_Status.Starting);// Send Server State Changed Event

            ClientNodes = new List<TCP_ClientNode>();
            Listener = new TcpListener(ServerAddress, ServerPort);// Create a new server object
            Listener.Start();// Starts the server
            Listener.BeginAcceptSocket(Client_Connected, Listener);// Creates an accept client callback
        }

        /// <summary>
        /// Stopps the server
        /// </summary>
        public void Stop()
        {
            try
            {
                Listener.Stop();// Stop the server if it is running
                Listener = null;// Delete the server object if it exists

                Fire_TCP_ServerState_Changed(Data.Server_Status.Offline);// Send Server State Changed Event
            }
            catch { /* Dont Care, this is just to check that the server is stopped */ }
        }
        #endregion

        #region Connection Events
        private void Client_Connected(IAsyncResult ar)
        {
            TcpListener tcpl = (TcpListener)ar.AsyncState;// The Listener
            TcpClient tclient = null;// The Connecting clients TCPClient Object
            TCP_ClientNode node = null;// The Connecting clients Node Object

            try
            {
                if (AcceptClients && ClientNodes.Count <= MaxConnections)
                {
                    tclient = Listener.EndAcceptTcpClient(ar);// Creates a client object to handel remote connection

                    tcpl.BeginAcceptTcpClient(Client_Connected, Listener);// Starts listening for another connection

                    lock (ClientNodes)
                    {
                        node = new TCP_ClientNode(tclient, tclient.Client.RemoteEndPoint.ToString(), this, SSLCert);// Creates a new client node object
                        node.Client.GetStream().BeginRead(node.Rx, 0, node.Rx.Length, Client_ReadData, node.Client);// Starts to read the data recieved
                        ClientNodes.Add(node);// Adds the client node to the list
                    }

                    Fire_TCP_ClientState_Changed(node, Data.Client_ConnectionStatus.Connected);// Sends the client connected event
                }
                else
                {
                    tclient = Listener.EndAcceptTcpClient(ar);// Creates a client object to handel remote connection
                    tclient.Close();// Closes the connection (if full or not accepting, this will change latter)

                    tcpl.BeginAcceptTcpClient(Client_Connected, Listener);// Starts listening for another connection

                    Fire_TCP_ClientState_Changed(null, Data.Client_ConnectionStatus.Rejected);// Sends the client rejected event
                }
            }
            catch(Exception ex)
            {
                /* Error occured when connecting a client */
            }
        }

        /// <summary>
        /// Disconnects a client
        /// </summary>
        /// <param name="node">The client being disconected</param>
        public void Disconnect_Client(TCP_ClientNode node)
        {
            Fire_TCP_ClientState_Changed(node, Data.Client_ConnectionStatus.Disconnected);// Sends Client Disconnect Event

            try { node.Client.Close(); } catch { }// Closes Client
            try { node.Socket.Close(); } catch { }// Closes Connection

            lock (ClientNodes)
            {
                ClientNodes.Remove(node);// Removes from list
            }

            node = null;// Clears the object
        }
        #endregion

        #region Data Events
        private void Client_ReadData(IAsyncResult ar)
        {
            TCP_ClientNode node = ClientNodes.Find(x => x.ID == ((TcpClient)ar.AsyncState).Client.RemoteEndPoint.ToString());// Gets the node that send the data
            int DataLength = 0;

            try
            {
                DataLength = node.Client.GetStream().EndRead(ar);// Gets the length and ends read

                if (DataLength == 0)// If Data has nothing in it
                {
                    Disconnect_Client(node);// Disconnects Client
                    return;
                }

                if (node.Data == null) { node.Data = ""; }// Checks to see if it is null and if it is them set it to an empty string

                node.Data = node.Data + Encoding.UTF8.GetString(node.Rx, 0, DataLength).Trim();// Gets the data and trims it
                if (node.Data.EndsWith("|<EOD>|"))
                {
                    node.Data.Replace("|<EOD>|", "");// Removes the EOD marker
                    Fire_TCP_Data_Event(node.Data, node, DataDirection.Recieve);// Fires the Data Recieved Event
                    CommandHandeler.Invoke(node, node.Data);// Executes the command handeler
                    node.Data = "";// Data Recieved
                }

                node.Rx = new byte[32768];//Sets the clients recieve buffer
                node.Client.GetStream().BeginRead(node.Rx, 0, node.Rx.Length, Client_ReadData, node.Client);//Starts to read again
            }
            catch
            {
                // Client Dropped
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Client_SendData(string Data, TCP_ClientNode node = null)
        {
            if (node == null)
            {
                foreach (TCP_ClientNode cn in ClientNodes)
                {
                    cn.Send(Data);// Send the data to everybody
                }
            }
            else
            {
                node.Send(Data);// Send the data to a single client
            }
        }

        internal void OnWrite(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpc = (TcpClient)ar.AsyncState;//Gets the client data is going to
                tcpc.GetStream().EndWrite(ar);//Ends client write stream
            }
            catch (Exception e)
            {
                Fire_TCP_Data_Error_Event(e, DataDirection.Send);
                /* Transmition Error */
            }
        }
        #endregion
    }
}