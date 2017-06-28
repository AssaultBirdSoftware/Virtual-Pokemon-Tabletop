using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using AssaultBird2454.VPTU.Networking.Shared;
using System.Threading;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    public enum NetworkMode { Standard, SSL }

    public class TCP_ClientNode
    {
        #region Networking
        public TCP_Server Server { get; set; }// A Refrence to the server it belongs to

        internal TcpClient Client { get; set; }// TCP Client
        public string ID { get; set; }// Connection ID (Connection Endpoint)
        internal StateObject StateObject { get; set; }
        private NetworkMode NetMode { get; set; }

        private Queue<string> DataQue { get; set; }// A Data Que
        private Thread QueReadThread;// A Thread to read data in the que
        private readonly EventWaitHandle ReadQueWait;// An event to signal when data is avaliable

        private string[] delimiter = new string[] { "|<EOD>|" };// End Of Data Signal
        #endregion

        public TCP_ClientNode(TcpClient _Client, string _ID, TCP_Server _Server)
        {
            Server = _Server;// Sets the server
            Client = _Client;// Sets the client
            StateObject = new StateObject(Client.Client, 32768);// Creates a new state object
            ID = _ID;// Sets the ID
            ReadQueWait = new EventWaitHandle(false, EventResetMode.AutoReset, ID + " ReadQue");// Creates the handel event
            NetMode = NetworkMode.Standard;

            StartListening();// Starts Listening

            DataQue = new Queue<string>();// Creates Que

            QueReadThread = new Thread(new ThreadStart(() =>
            {
                QueRead();
            }));
            QueReadThread.Start();// Creates and Starts the thread to read the que
        }

        #region RX Message Que
        public void QueRead()
        {
            while (true)
            {
                ReadQueWait.WaitOne();// Waits until it has been signaled that there is data avaliable

                while (DataQue.Count >= 1)// While there is data avaliable
                {
                    Server.CommandHandeler.InvokeCommand(DataQue.Dequeue(), this);// Process the data
                }
            }
        }
        #endregion

        #region RX
        /// <summary>
        /// Tells the server to start listening to incoming data
        /// </summary>
        private void StartListening()
        {
            if (NetMode == NetworkMode.Standard)
            {
                StateObject.Reset();// resets the State Object
                StateObject.workSocket.BeginReceive(StateObject.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Client_ReadData), StateObject);
            }
            else if (NetMode == NetworkMode.SSL)
            {
                StateObject.SSL_Reset();// resets the State Object
                StateObject.SSL.BeginRead(StateObject.buffer, 0, StateObject.BUFFER_SIZE, new AsyncCallback(Client_ReadSslData), StateObject);
            }
        }
        private void StopListening()
        {
            throw new NotImplementedException("Stopping an async listener is not permitted at this time");
        }
        private void Client_ReadData(IAsyncResult ar)
        {
            StateObject so = (StateObject)ar.AsyncState;// gets the state object
            Socket s = so.workSocket;// Gets the stream used
            int read;

            try { read = s.EndReceive(ar); }
            catch
            {
                Server.Disconnect_Client(this);// Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read));// Appends Data

                if (so.sb.ToString().Contains("|<EOD>|"))
                {
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string[] strContent = so.sb.ToString().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);// Splits the string
                        foreach (string data in strContent)// While Data is abaliable
                        {
                            DataQue.Enqueue(data);// Ques the data
                            ReadQueWait.Set();// Signals new data is avaliable
                        }
                    }
                }

                if (NetMode == NetworkMode.Standard)
                {
                    StartListening();// Starts Listening Again
                }
            }
            else
            {
                Server.Disconnect_Client(this);// Disconnects
                return;
            }
        }
        private void Client_ReadSslData(IAsyncResult ar)
        {
            StateObject so = (StateObject)ar.AsyncState;// gets the state object
            SslStream s = so.SSL;// Gets the stream used
            int read;

            try { read = s.EndRead(ar); }
            catch
            {
                Server.Disconnect_Client(this);// Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read));// Appends Data

                if (so.sb.ToString().Contains("|<EOD>|"))
                {
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string[] strContent = so.sb.ToString().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);// Splits the string
                        foreach (string data in strContent)// While Data is abaliable
                        {
                            DataQue.Enqueue(data);// Ques the data
                            ReadQueWait.Set();// Signals new data is avaliable
                        }
                    }
                }

                if (NetMode == NetworkMode.SSL)
                {
                    StartListening();// Starts Listening Again
                }
            }
            else
            {
                Server.Disconnect_Client(this);// Disconnects
                return;
            }
        }
        #endregion
        #region TX
        /// <summary>
        /// Sends data to this client, No Encryption or Serialization is performed at this step
        /// </summary>
        /// <param name="Data">The String being transmitted</param>
        public void Send(object Data)
        {
            if (Data is Data.NetworkCommand)
            {
                if (NetMode == NetworkMode.Standard)
                {
                    string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
                    byte[] Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                    Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client);// Sends the data to the client
                }
                else if (NetMode == NetworkMode.SSL)
                {
                    string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
                    byte[] Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                    StateObject.SSL.BeginWrite(Tx, 0, Tx.Length, OnSslWrite, StateObject.SSL);// Sends the data to the client
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
                TcpClient tcpc = (TcpClient)ar.AsyncState;//Gets the client data is going to
                tcpc.GetStream().EndWrite(ar);//Ends client write stream
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
                SslStream tcpc = (SslStream)ar.AsyncState;//Gets the client data is going to
                tcpc.EndWrite(ar);//Ends client write stream
            }
            catch (Exception e)
            {
                //Fire_TCP_Data_Error_Event(e, DataDirection.Send);
                /* Transmition Error */
            }
        }
        #endregion

        public void Disconnect()
        {
            try
            {
                QueReadThread.Abort();// Stopps the thread
                StateObject.workSocket.Close();// Closes the socket
                Client.Close();// Closes the client

                QueReadThread = null;// Clears the Thread
                StateObject = null;
            }
            catch { }// Closes Client
        }

        internal void EnableSSL(Data.ResponseCode code)
        {
            if (NetMode == NetworkMode.SSL)
            {
                Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.Error));// SSL Already Enabled
                return;// End
            }

            if (code == Data.ResponseCode.None)// SSL Setup Started by Client
            {
                if (Server.SSL_Cert != null)// Checks if the cert is set
                {
                    Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.Avaliable));// Cert Avaliable, Signal SSL Avaliablity
                }
                else
                {
                    Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.Not_Avaliable));// Cert Not Avaliable, Signal SSL Unavaliability
                }
            }
            else if (code == Data.ResponseCode.Ready)// Client is ready to accept SSL
            {
                StateObject.SSL = new SslStream(StateObject.workSocket., true);// Creates a new SSL Stream
                StateObject.SSL.AuthenticateAsServer(Server.SSL_Cert, false, System.Security.Authentication.SslProtocols.Default, true);// Authenticate Server
            }
            else if (code == Data.ResponseCode.OK)
            {
                Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.OK));// Error
            }
            else
            {
                Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.Error));// Error
            }
        }
        private void SSL_ActiveCallback(IAsyncResult ar)
        {
            StateObject so = (StateObject)ar.AsyncState;// gets the state object

            NetMode = NetworkMode.SSL;// Sets the NetMode

            StartListening();// Starts Listening

            Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Enable, Data.ResponseCode.Ready));// Ready To Use SSL
        }

        internal void DissableSSL()
        {
            if (NetMode == NetworkMode.Standard)
            {
                Send(new Data.InternalNetworkCommand(Data.Commands.SSL_Dissable, Data.ResponseCode.Error));
                return;
            }

            NetMode = NetworkMode.Standard;
        }
    }
}