﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using AssaultBird2454.VPTU.Networking.Data;
using AssaultBird2454.VPTU.Networking.Server.Command_Handeler;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public class TcpServer
    {
        public TcpServer(IPAddress Address, Server_CommandHandeler _CommandHandeler, int Port = 25444)
        {
            TCP_ServerAddress = Address;
            TCP_ServerPort = Port; // Sets the port
            TCP_AcceptClients = true; // Allows clients to connect
            CommandHandeler = _CommandHandeler; // Sets the Command Callback

            if (!CommandHandeler.HasCommandName("Network Command"))
                CommandHandeler.RegisterCommand<InternalNetworkCommand>("Network Command", Server_Commands);
        }

        #region Data Events

        /// <summary>
        /// </summary>
        public void Client_SendData(object Data, TcpClientNode node = null)
        {
            if (node == null)
                foreach (var cn in ClientNodes)
                    cn.Send(Data); // Send the data to everybody
            else
                node.Send(Data); // Send the data to a single client
        }

        #endregion

        /// <summary>
        ///     Handels Network Commands
        /// </summary>
        /// <param name="_Data">The Data that needs to be handeled</param>
        /// <param name="node">The client that sent it</param>
        internal void Server_Commands(object _Data, TcpClientNode node)
        {
            var Data = (InternalNetworkCommand) _Data;

            if (Data.CommandType == Commands.SSL_Enable)
            {
                node.EnableSSL(Data.Response);
            }
            else if (Data.CommandType == Commands.SSL_Dissable)
            {
                node.DissableSSL();
            }
            else if (Data.CommandType == Commands.SSL_Active)
            {
            }
            else if (Data.CommandType == Commands.SetBufferSize)
            {
                //Command Not Implemented
                node.Send(new InternalNetworkCommand(Commands.SetBufferSize, ResponseCode.Not_Implemented));
            }
        }

        #region Events

        /// <summary>
        ///     An event that fires when the server starts or stops accepting connection requests
        /// </summary>
        public event TCP_AcceptClients_Handeler TCP_AcceptClients_Changed;

        /// <summary>
        ///     An event that fires when a clients connection state changes
        /// </summary>
        public event TCP_ClientState_Handeler TCP_ClientState_Changed;

        /// <summary>
        ///     An event that fires when a server state changes
        /// </summary>
        public event TCP_ServerState_Handeler TCP_ServerState_Changed;

        /// <summary>
        ///     An event thst fires when data is sent or recieved
        /// </summary>
        public event TCP_Data TCP_Data_Event;

        /// <summary>
        ///     An event that is fired when a data error occures
        /// </summary>
        public event TCP_Data_Error TCP_Data_Error_Event;

        #region Trigger Event Methods

        protected void Fire_TCP_AcceptClients_Changed(bool Accepting_Connections)
        {
            TCP_AcceptClients_Changed?.Invoke(Accepting_Connections);
        }

        protected void Fire_TCP_ClientState_Changed(TcpClientNode Client, Client_ConnectionStatus Client_State)
        {
            TCP_ClientState_Changed?.Invoke(Client, Client_State);
        }

        protected void Fire_TCP_ServerState_Changed(Server_Status Server_State)
        {
            TCP_ServerState_Changed?.Invoke(Server_State);
        }

        protected void Fire_TCP_Data_Event(string Data, TcpClientNode Client, DataDirection Direction)
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
        ///     The Server Object
        /// </summary>
        private TcpListener Listener;

        /// <summary>
        ///     A List object containing all the client's connected
        /// </summary>
        private List<TcpClientNode> ClientNodes;

        private bool TCP_AcceptClients; // Server Accept Connsctions
        private IPAddress TCP_ServerAddress; // Servers Address
        private int TCP_ServerPort; // Servers Port
        private X509Certificate TCP_SSLCert; // SSL Certificate

        public Server_CommandHandeler CommandHandeler;

        #endregion

        #region Variable Handelers

        /// <summary>
        ///     A Setting that tells the server if it is allowed to accept any more clients
        /// </summary>
        public bool AcceptClients
        {
            get => TCP_AcceptClients;
            set
            {
                Fire_TCP_AcceptClients_Changed(value);
                TCP_AcceptClients = value;
            }
        }

        /// <summary>
        ///     A Setting that defines the ipaddress that the server should listen to for incoming connections, These changes will
        ///     take no effect when the server is running...
        /// </summary>
        public IPAddress ServerAddress
        {
            get => TCP_ServerAddress;
            set
            {
                if (Listener != null)
                    throw new ServerAlreadyRunningException();

                TCP_ServerAddress = value;
            }
        }

        /// <summary>
        ///     A Setting that defines what port that the server opperates on
        /// </summary>
        public int ServerPort
        {
            get => TCP_ServerPort;
            set
            {
                if (Listener != null)
                    throw new ServerAlreadyRunningException();

                TCP_ServerPort = value;
            }
        }

        /// <summary>
        ///     A Setting that defines the maxamum amount of client connections
        /// </summary>
        public int MaxConnections { get; set; }

        public X509Certificate SSL_Cert
        {
            get => TCP_SSLCert;
            set
            {
                if (Listener != null)
                    throw new ServerAlreadyRunningException();
                TCP_SSLCert = value;
            }
        }

        #endregion

        #region Server Methods

        /// <summary>
        ///     Start the server using the settings that have already been set
        /// </summary>
        public void Start()
        {
            try
            {
                Stop();
            }
            catch
            {
            } // Executes the stop server method to check that it is not running and to clear it

            Fire_TCP_ServerState_Changed(Server_Status.Starting); // Send Server State Changed Event

            ClientNodes = new List<TcpClientNode>();
            Listener = new TcpListener(ServerAddress, ServerPort); // Create a new server object
            Listener.Start(); // Starts the server
            Listener.BeginAcceptSocket(Client_Connected, Listener); // Creates an accept client callback
        }

        /// <summary>
        ///     Stopps the server
        /// </summary>
        public void Stop()
        {
            var acceptState = AcceptClients;

            try
            {
                Disconnect_AllClients();

                Listener.Stop(); // Stop the server if it is running
                Listener = null; // Delete the server object if it exists

                Fire_TCP_ServerState_Changed(Server_Status.Offline); // Send Server State Changed Event
            }
            catch (Exception e)
            {
                /* Dont Care, this is just to check that the server is stopped */
            }
            finally
            {
                AcceptClients = acceptState;
            }
        }

        #endregion

        #region Connection Events

        private void Client_Connected(IAsyncResult ar)
        {
            var tcpl = (TcpListener) ar.AsyncState; // The Listener
            TcpClient tclient = null; // The Connecting clients TCPClient Object
            TcpClientNode node = null; // The Connecting clients Node Object

            try
            {
                if (AcceptClients && ClientNodes.Count <= MaxConnections)
                {
                    tclient = Listener.EndAcceptTcpClient(ar); // Creates a client object to handel remote connection

                    tcpl.BeginAcceptTcpClient(Client_Connected, Listener); // Starts listening for another connection

                    lock (ClientNodes)
                    {
                        node = new TcpClientNode(tclient, tclient.Client.RemoteEndPoint.ToString(),
                            this); // Creates a new client node object
                        ClientNodes.Add(node); // Adds the client node to the list
                    }

                    Fire_TCP_ClientState_Changed(node,
                        Client_ConnectionStatus.Connected); // Sends the client connected event
                }
                else
                {
                    tclient = Listener.EndAcceptTcpClient(ar); // Creates a client object to handel remote connection
                    tclient.Close(); // Closes the connection (if full or not accepting, this will change latter)

                    tcpl.BeginAcceptTcpClient(Client_Connected, Listener); // Starts listening for another connection

                    Fire_TCP_ClientState_Changed(null,
                        Client_ConnectionStatus.Rejected); // Sends the client rejected event
                }
            }
            catch (Exception ex)
            {
                /* Error occured when connecting a client */
            }
        }

        /// <summary>
        ///     Disconnects a client
        /// </summary>
        /// <param name="node">The client being disconected</param>
        public void Disconnect_Client(TcpClientNode node)
        {
            Fire_TCP_ClientState_Changed(node, Client_ConnectionStatus.Disconnected); // Sends Client Disconnect Event

            node.Disconnect(); // Disconnects the client from the server

            lock (ClientNodes)
            {
                ClientNodes.Remove(node); // Removes from list
            }
        }

        /// <summary>
        ///     Disconnects all clients
        /// </summary>
        public void Disconnect_AllClients()
        {
            while (ClientNodes.Count >= 1)
                Disconnect_Client(ClientNodes[0]);
        }

        #endregion
    }
}