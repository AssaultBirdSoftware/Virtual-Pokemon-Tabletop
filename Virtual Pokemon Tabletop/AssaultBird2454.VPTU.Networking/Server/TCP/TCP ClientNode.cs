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
    public class TCP_ClientNode
    {
        #region Networking
        public TCP_Server Server { get; set; }// A Refrence to the server it belongs to

        internal TcpClient Client { get; set; }// TCP Client
        public string ID { get; set; }// Connection ID (Connection Endpoint)
        internal StateObject StateObject { get; set; }
        internal byte[] Tx { get; set; }// Transmit Buffer

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
            Tx = new byte[32768];// 32768

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
                    Server.CommandHandeler.Invoke(this, DataQue.Dequeue());// Process the data
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
            StateObject.workSocket.BeginReceive(StateObject.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Client_ReadData), StateObject);
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
                        so.Reset();// resets the State Object
                        foreach (string data in strContent)// While Data is abaliable
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
        /// Sends data to this client, No Encryption or Serialization is performed at this step
        /// </summary>
        /// <param name="Data">The String being transmitted</param>
        public void Send(string Data)
        {
            Tx = Encoding.UTF8.GetBytes(Data + "|<EOD>|");// Encodes the data
            Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client);// Sends the data to the client
            Tx = new byte[32768];// Creates a new transmittion buffer
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
        #endregion
    }
}