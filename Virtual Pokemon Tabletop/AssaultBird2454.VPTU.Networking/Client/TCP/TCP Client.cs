using AssaultBird2454.VPTU.Networking.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    public class TCP_Client
    {
        #region Events
        /// <summary>
        /// An event that is fired when the connection state changes
        /// </summary>
        public event TCP_ConnectionState_Handeler ConnectionStateEvent;
        private void Fire_ConnectionStateEvent(Data.Client_ConnectionStatus state)
        {
            ConnectionStateEvent?.Invoke(state);
        }

        /// <summary>
        /// An Event that is fired when the client transmitts or recieves data from the server
        /// </summary>
        public event TCP_Data DataEvent;
        private void Fire_DataEvent(string Data, DataDirection Direction)
        {
            DataEvent?.Invoke(Data, Direction);
        }

        #endregion

        #region Variables
        private TcpClient Client;// Client Object
        private IPAddress TCP_IPAddress;// Servers IPAddress
        private StateObject StateObject;// Client State Object
        private string[] delimiter = new string[] { "|<EOD>|" };// The Delimiting string for commands
        private int TCP_Port;// The portnumber that the server is running on

        private byte[] Tx;// Buffer for transmitting Data
        #region Data Que
        private Queue<string> DataQue;// Data Que
        private Thread DataQueThread;// A Thread to run the data que
        private readonly EventWaitHandle ReadQueWait;// An event for signaling to the reader that data is in Que
        #endregion

        /// <summary>
        /// The Command Handeler that the server will use to invoke commands
        /// </summary>
        public Command_Handeler.Client_CommandHandeler CommandHandeler;
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
                if (Client != null)
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

        public TCP_Client(IPAddress Address, Command_Handeler.Client_CommandHandeler _CommandHandeler, int Port = 25444)
        {
            ReadQueWait = new EventWaitHandle(false, EventResetMode.AutoReset, "ReadQue");
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

            Fire_ConnectionStateEvent(Data.Client_ConnectionStatus.Connecting);

            Client = new TcpClient();// Creates a new client object

            Client.BeginConnect(TCP_IPAddress, TCP_Port, Client_Connected, null);// Connects to the server and on connect will call the connect call back

            Tx = new byte[32768];// Creates a new Transmittion Buffer
        }

        /// <summary>
        /// Disconnects the Client from the server
        /// </summary>
        public void Disconnect()
        {
            try
            {
                DataQueThread.Abort();// Stopps the read thread
                DataQueThread = null;// Clears the read Thread

                Client.Close();// Closes connection
                Client = null;// Clears Client

                Fire_ConnectionStateEvent(Data.Client_ConnectionStatus.Disconnected);
            }
            catch
            {
                /* Failed to disconnect or was never connected to anything */
            }
        }
        #endregion

        #region Data Events
        /// <summary>
        /// A Method to start the reading of network data again
        /// </summary>
        private void StartListening()
        {
            Client.GetStream().BeginRead(StateObject.buffer, 0, 32768, Client_DataRecv, StateObject);
        }

        private void Client_DataRecv(IAsyncResult ar)
        {
            StateObject so = (StateObject)ar.AsyncState;
            //int DataLength = 0;
            Socket s = so.workSocket;
            int read;

            try { read = s.EndReceive(ar); }
            catch
            {
                Disconnect();// Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read));

                if (so.sb.ToString().Contains("|<EOD>|"))
                {
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string[] strContent;
                        strContent = so.sb.ToString().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                        so.Reset();
                        foreach (string data in strContent)
                        {
                            DataQue.Enqueue(data);
                            ReadQueWait.Set();
                        }
                    }
                }
                StartListening();
            }
            else
            {
                Disconnect();// Disconnects
                return;
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

        /// <summary>
        /// Sends data to the server
        /// </summary>
        /// <param name="Data">The data being sent to the server</param>
        public void SendData(object Data)
        {
            if (Data is Data.NetworkCommand)
            {
                if (IsConnected)
                {
                    string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(Data);// Serialises the data to be sent
                    Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");// Encodes the data
                    Fire_DataEvent(JSONData, DataDirection.Send);// Invokes the data recieved event
                    Client.GetStream().BeginWrite(Tx, 0, Tx.Length, Client_DataTran, Client);// Sneds the data to the server
                    Tx = new byte[32768];// Creates a tranmittion buffer
                }
            }
            else
            {
                throw new NotNetworkDataException();
            }
        }
        #endregion

        #region RX Message Que
        public void QueRead()
        {
            while (true)
            {
                ReadQueWait.WaitOne();// Waits for data

                while (DataQue.Count >= 1)// While there is data
                {
                    string Data = DataQue.Dequeue();// Gets the data and then removes it from the que
                    Fire_DataEvent(Data, DataDirection.Recieve);// Invokes the data recieved event
                    CommandHandeler.InvokeCommand(Data);// Handels the data
                }
            }
        }
        #endregion

        #region Connection Events
        private void Client_Connected(IAsyncResult ar)
        {
            DataQue = new Queue<string>();// Creates the que

            DataQueThread = new Thread(new ThreadStart(() =>
            {
                QueRead();
            }));
            DataQueThread.Start();// Creates and Starts the Read Thread

            StateObject = new StateObject(Client.Client, 32768);// Creates State Object for Client
            StartListening();// Starts Listening

            Fire_ConnectionStateEvent(Data.Client_ConnectionStatus.Connected);
        }
        #endregion
    }
}