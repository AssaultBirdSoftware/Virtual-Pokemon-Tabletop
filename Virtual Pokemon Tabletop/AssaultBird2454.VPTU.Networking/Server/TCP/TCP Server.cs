using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    #region Event Delegates
    public delegate void TCP_AcceptClients_Handeler(bool Accepting_Connections);

    public delegate void TCP_ClientState_Handeler(TCP_ClientNode Client, Data.Client_ConnectionStatus Client_State);

    public delegate void TCP_ServerState_Handeler(Data.Server_Status Server_State);
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
                TCP_AcceptClients_Changed.Invoke(value);
                TCP_AcceptClients = value;
            }
        }

        /// <summary>
        /// A Setting that defines the ipaddress that the server should listen to for incoming connections
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

        public TCP_Server(IPAddress Address, int Port = 25444)
        {
            ServerAddress = Address;
            ServerPort = Port;// Sets the port
            TCP_AcceptClients = true;// Allows clients to connect
        }

        #region Server Methods
        /// <summary>
        /// Start the server using the settings that have already been set
        /// </summary>
        public void Start()
        {
            Stop();// Executes the stop server method to check that it is not running and to clear it

            TCP_ServerState_Changed.Invoke(Data.Server_Status.Starting);// Send Server State Changed Event

            ClientNodes = new List<TCP_ClientNode>();
            Listener = new TcpListener(ServerAddress, ServerPort);// Create a new server object

            Listener.BeginAcceptSocket(Client_Connected, null);// Creates an accept client callback

            Listener.Start();// Starts the server
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

                TCP_ServerState_Changed.Invoke(Data.Server_Status.Offline);// Send Server State Changed Event
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

                    tcpl.BeginAcceptTcpClient(Client_Connected, null);// Starts listening for another connection

                    lock (ClientNodes)
                    {
                        node = new TCP_ClientNode(tclient, tclient.Client.RemoteEndPoint.ToString(), this);// Creates a new client node object
                        node.Client.GetStream().BeginRead(node.Rx, 0, node.Rx.Length, Client_ReadData, node.Client);// Starts to read the data recieved
                        ClientNodes.Add(node);// Adds the client node to the list
                    }

                    TCP_ClientState_Changed.Invoke(node, Data.Client_ConnectionStatus.Connected);// Sends the client connected event
                }
                else
                {
                    tclient = Listener.EndAcceptTcpClient(ar);// Creates a client object to handel remote connection
                    tclient.Close();// Closes the connection (if full or not accepting, this will change latter)

                    tcpl.BeginAcceptTcpClient(Client_Connected, null);// Starts listening for another connection

                    TCP_ClientState_Changed.Invoke(null, Data.Client_ConnectionStatus.Rejected);// Sends the client rejected event
                }
            }
            catch
            {
                /* Error occured when connecting a client */
            }
        }

        /// <summary>
        /// Disconnects a client
        /// </summary>
        /// <param name="node">The client being disconected</param>
        public void Client_Disconnected(TCP_ClientNode node)
        {
            TCP_ClientState_Changed.Invoke(node, Data.Client_ConnectionStatus.Disconnected);// Sends Client Disconnect Event

            node.Client.Close();// Closes Connection

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
            string Data = "";// The data that was recieved
            int DataLength = 0;

            try
            {
                DataLength = node.Client.GetStream().EndRead(ar);// Gets the length and ends read

                if (DataLength == 0)// If Data has nothing in it
                {

                    return;
                }

                Data = Encoding.UTF8.GetString(node.Rx, 0, DataLength).Trim();// Gets the data and trims it

                //Use Data

                node.Rx = new byte[32768];//Sets the clients recieve buffer
                node.Client.GetStream().BeginRead(node.Rx, 0, node.Rx.Length, Client_ReadData, node.Client);//Starts to read again
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Client_SendData(string _Data, TCP_ClientNode node = null)
        {
            string Data = "";
            // Convert Data
            if (node == null)
            {
                foreach (TCP_ClientNode cn in ClientNodes)
                {
                    //cn.Send();
                }
            }
            else
            {

            }
        }

        internal void OnWrite(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpc = (TcpClient)ar.AsyncState;//Gets the client data is going to
                tcpc.GetStream().EndWrite(ar);//Ends client write stream
            }
            catch { /* Transmition Error */ }
        }
        #endregion
    }
}
