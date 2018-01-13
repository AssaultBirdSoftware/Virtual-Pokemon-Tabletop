using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using AssaultBird2454.VPTU.Networking.Data;
using AssaultBird2454.VPTU.Networking.Shared;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public enum NetworkMode
    {
        Standard,
        SSL
    }

    public class TcpClientNode
    {
        public TcpClientNode(TcpClient _Client, string _ID, TcpServer _Server)
        {
            Server = _Server; // Sets the server
            Client = _Client; // Sets the client
            StateObject = new StateObject(Client.Client, 32768); // Creates a new state object
            ID = _ID; // Sets the ID
            ReadQueWait =
                new EventWaitHandle(false, EventResetMode.AutoReset, ID + " ReadQue"); // Creates the handel event
            NetMode = NetworkMode.Standard;

            StartListening(); // Starts Listening

            DataQue = new Queue<string>(); // Creates Que

            QueReadThread = new Thread(() => { QueRead(); });
            QueReadThread.IsBackground = true;
            QueReadThread.Start(); // Creates and Starts the thread to read the que
        }

        #region RX Message Que

        public void QueRead()
        {
            while (true)
            {
                ReadQueWait.WaitOne(); // Waits until it has been signaled that there is data avaliable

                while (DataQue.Count >= 1) // While there is data avaliable
                    Server.CommandHandeler.InvokeCommand(DataQue.Dequeue(), this); // Process the data
            }
        }

        #endregion

        public void Disconnect()
        {
            try
            {
                QueReadThread.Abort(); // Stopps the thread
                StateObject.workSocket.Close(); // Closes the socket
                Client.Close(); // Closes the client

                QueReadThread = null; // Clears the Thread
                StateObject = null;
            }
            catch
            {
            } // Closes Client
        }

        internal void EnableSSL(ResponseCode code)
        {
            if (NetMode == NetworkMode.SSL)
            {
                Send(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.Error)); // SSL Already Enabled
                return; // End
            }

            if (code == ResponseCode.None) // SSL Setup Started by Client
            {
                if (Server.SSL_Cert != null) // Checks if the cert is set
                    Send(new InternalNetworkCommand(Commands.SSL_Enable,
                        ResponseCode.Avaliable)); // Cert Avaliable, Signal SSL Avaliablity
                else
                    Send(new InternalNetworkCommand(Commands.SSL_Enable,
                        ResponseCode.Not_Avaliable)); // Cert Not Avaliable, Signal SSL Unavaliability
            }
            else if (code == ResponseCode.Ready) // Client is ready to accept SSL
            {
                StateObject.SSL = new SslStream(Client.GetStream(), true); // Creates a new SSL Stream
                StateObject.SSL.AuthenticateAsServer(Server.SSL_Cert, false, SslProtocols.Default,
                    true); // Authenticate Server
            }
            else if (code == ResponseCode.OK)
            {
                Send(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.OK)); // Error
            }
            else
            {
                Send(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.Error)); // Error
            }
        }

        private void SSL_ActiveCallback(IAsyncResult ar)
        {
            var so = (StateObject) ar.AsyncState; // gets the state object

            NetMode = NetworkMode.SSL; // Sets the NetMode

            StartListening(); // Starts Listening

            Send(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.Ready)); // Ready To Use SSL
        }

        internal void DissableSSL()
        {
            if (NetMode == NetworkMode.Standard)
            {
                Send(new InternalNetworkCommand(Commands.SSL_Dissable, ResponseCode.Error));
                return;
            }

            NetMode = NetworkMode.Standard;
        }

        #region Networking

        public TcpServer Server { get; set; } // A Refrence to the server it belongs to

        internal TcpClient Client { get; set; } // TCP Client
        public string ID { get; set; } // Connection ID (Connection Endpoint)
        internal StateObject StateObject { get; set; }
        private NetworkMode NetMode { get; set; }

        private Queue<string> DataQue { get; } // A Data Que
        private Thread QueReadThread; // A Thread to read data in the que
        private readonly EventWaitHandle ReadQueWait; // An event to signal when data is avaliable

        private readonly string[] delimiter = {"|<EOD>|"}; // End Of Data Signal

        #endregion

        #region RX

        /// <summary>
        ///     Tells the server to start listening to incoming data
        /// </summary>
        private void StartListening()
        {
            if (NetMode == NetworkMode.Standard)
            {
                StateObject.Reset(); // resets the State Object
                StateObject.workSocket.BeginReceive(StateObject.buffer, 0, StateObject.BUFFER_SIZE, 0, Client_ReadData,
                    StateObject);
            }
            else if (NetMode == NetworkMode.SSL)
            {
                StateObject.SSL_Reset(); // resets the State Object
                StateObject.SSL.BeginRead(StateObject.buffer, 0, StateObject.BUFFER_SIZE, Client_ReadSslData,
                    StateObject);
            }
        }

        private void StopListening()
        {
            throw new NotImplementedException("Stopping an async listener is not permitted at this time");
        }

        private void Client_ReadData(IAsyncResult ar)
        {
            var so = (StateObject) ar.AsyncState; // gets the state object
            var s = so.workSocket; // Gets the stream used
            int read;

            try
            {
                read = s.EndReceive(ar);
            }
            catch
            {
                Server.Disconnect_Client(this); // Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read)); // Appends Data

                if (so.sb.ToString().Contains("|<EOD>|"))
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        var strContent = so.sb.ToString()
                            .Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Splits the string
                        foreach (var data in strContent) // While Data is abaliable
                        {
                            DataQue.Enqueue(data); // Ques the data
                            ReadQueWait.Set(); // Signals new data is avaliable
                        }
                    }

                if (NetMode == NetworkMode.Standard)
                    StartListening(); // Starts Listening Again
            }
            else
            {
                Server.Disconnect_Client(this); // Disconnects
            }
        }

        private void Client_ReadSslData(IAsyncResult ar)
        {
            var so = (StateObject) ar.AsyncState; // gets the state object
            var s = so.SSL; // Gets the stream used
            int read;

            try
            {
                read = s.EndRead(ar);
            }
            catch
            {
                Server.Disconnect_Client(this); // Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read)); // Appends Data

                if (so.sb.ToString().Contains("|<EOD>|"))
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        var strContent = so.sb.ToString()
                            .Split(delimiter, StringSplitOptions.RemoveEmptyEntries); // Splits the string
                        foreach (var data in strContent) // While Data is abaliable
                        {
                            DataQue.Enqueue(data); // Ques the data
                            ReadQueWait.Set(); // Signals new data is avaliable
                        }
                    }

                if (NetMode == NetworkMode.SSL)
                    StartListening(); // Starts Listening Again
            }
            else
            {
                Server.Disconnect_Client(this); // Disconnects
            }
        }

        #endregion

        #region TX

        /// <summary>
        ///     Sends data to this client, No Encryption or Serialization is performed at this step
        /// </summary>
        /// <param name="Data">The String being transmitted</param>
        public void Send(object Data)
        {
            if (Data is Data.NetworkCommand)
            {
                if (NetMode == NetworkMode.Standard)
                {
                    var JSONData = JsonConvert.SerializeObject(Data);
                    var Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                    Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client); // Sends the data to the client
                }
                else if (NetMode == NetworkMode.SSL)
                {
                    var JSONData = JsonConvert.SerializeObject(Data);
                    var Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                    StateObject.SSL.BeginWrite(Tx, 0, Tx.Length, OnSslWrite,
                        StateObject.SSL); // Sends the data to the client
                }
            }
            else
            {
                throw new NotNetworkDataException();
            }
        }

        private void OnWrite(IAsyncResult ar)
        {
            try
            {
                var tcpc = (TcpClient) ar.AsyncState; //Gets the client data is going to
                tcpc.GetStream().EndWrite(ar); //Ends client write stream
            }
            catch (Exception e)
            {
                //Fire_TCP_Data_Error_Event(e, DataDirection.Send);
                /* Transmition Error */
            }
        }

        private void OnSslWrite(IAsyncResult ar)
        {
            try
            {
                var tcpc = (SslStream) ar.AsyncState; //Gets the client data is going to
                tcpc.EndWrite(ar); //Ends client write stream
            }
            catch (Exception e)
            {
                //Fire_TCP_Data_Error_Event(e, DataDirection.Send);
                /* Transmition Error */
            }
        }

        #endregion
    }
}