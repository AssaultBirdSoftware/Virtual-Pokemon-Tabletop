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
        #region Events
        /// <summary>
        /// An event thst fires when data is sent or recieved
        /// </summary>
        public event TCP_Data TCP_Data_Event;

        /// <summary>
        /// An event that is fired when a data error occures
        /// </summary>
        public event TCP_Data_Error TCP_Data_Error_Event;
        #endregion
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
            StateObject = new StateObject(Client.Client, 32768);// Creates a new state object 32768
            ID = _ID;// Sets the ID
            ReadQueWait = new EventWaitHandle(false, EventResetMode.AutoReset, ID + " ReadQue");// Creates the handel event
            NetMode = NetworkMode.Standard;

            StartListening();// Starts Listening

            DataQue = new Queue<string>();// Creates Que

            QueReadThread = new Thread(new ThreadStart(() =>
            {
                QueRead();
            }));
            QueReadThread.IsBackground = true;
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
                    string Data = DataQue.Dequeue();// Gets the Data
                    TCP_Data_Event?.Invoke(Data, this, DataDirection.Recieve);
                    Server.CommandHandeler.InvokeCommand(Data, this);// Process the data
                }
            }
        }
        #endregion

        #region RX
        /// <summary>
        /// Tells the server to start listening to incoming data
        /// </summary>
        private void StartListening(bool reset = false)
        {
            if (NetMode == NetworkMode.Standard)
            {
                StateObject.Reset();// resets the State Object
                StateObject.workSocket.BeginReceive(StateObject.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Client_ReadData), StateObject);
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
                        string data = Helper.GetUntilOrEmpty(so.sb, "|<EOD>|");
                        if (!String.IsNullOrWhiteSpace(data))
                        {
                            DataQue.Enqueue(data);// Ques the data
                            ReadQueWait.Set();// Signals new data is avaliable
                        }
                    }
                }

                StartListening();// Starts Listening Again
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
        /// Sends an object to this client
        /// </summary>
        /// <param name="Data">The object being transmitted</param>
        public void Send(object Data, string Command)
        {
            if (Data is Data.NetworkCommand)
            {
                if (NetMode == NetworkMode.Standard)
                {
                    string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
                    TCP_Data_Event?.Invoke(JSONData, this, DataDirection.Send);
                    byte[] Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                    Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client);// Sends the data to the client
                }
            }
            else
            {
                Data.NetworkCommand cmd = new Networking.Data.NetworkCommand()
                {
                    Data = Data,
                    Command = Command
                };

                string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(cmd);
                TCP_Data_Event?.Invoke(JSONData, this, DataDirection.Send);
                byte[] Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client);// Sends the data to the client
            }
        }
        /// <summary>
        /// Sends an object to this client, and invokes an Action when a response is recieved
        /// </summary>
        /// <param name="Data">The object being transmitted</param>
        /// <param name="Callback">The action to perform when a response has been recieved</param>
        public void Send(object Data, string Command, Action Callback)
        {
            //TODO: Handel creating responses

            Send(Data, Command);
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
                TCP_Data_Error_Event?.Invoke(e, DataDirection.Send);
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
    }
}