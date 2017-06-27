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
        internal StateObject StateObject { get; set; }
        //internal Socket Socket { get; set; }// Conection Socket
        internal byte[] Tx { get; set; }// Transmit Buffer
        //internal byte[] Rx { get; set; }// Recieve Buffer
        //internal string Data { get; set; }// Processed TCP Data gets stored here...
        private Queue<string> DataQue { get; set; }
        public string ID { get; set; }// Connection ID (Connection Endpoint)
        private Thread QueReadThread;
        private readonly EventWaitHandle ReadQueWait;
        private string[] delimiter = new string[] { "|<EOD>|" };
        #endregion

        public TCP_ClientNode(TcpClient _Client, string _ID, TCP_Server _Server)
        {
            Server = _Server;// Sets the server
            Client = _Client;// Sets the client
            StateObject = new StateObject(Client.Client, 32768);
            //Socket = _Client.Client;// Sets the socket
            ID = _ID;// Sets the ID
            //Data = "";
            ReadQueWait = new EventWaitHandle(false, EventResetMode.AutoReset, ID + " ReadQue");
            Tx = new byte[32768];// 32768
            //Rx = new byte[32768];// 32768

            StartListening();

            DataQue = new Queue<string>();

            QueReadThread = new Thread(new ThreadStart(() =>
            {
                QueRead();
            }));
            QueReadThread.Start();
        }

        #region RX Message Que
        public void QueRead()
        {
            while (true)
            {
                ReadQueWait.WaitOne();

                while (DataQue.Count >= 1)
                {
                    Server.CommandHandeler.Invoke(this, DataQue.Dequeue());
                }
            }
        }
        #endregion

        #region RX
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
            StateObject so = (StateObject)ar.AsyncState;
            //int DataLength = 0;
            Socket s = so.workSocket;
            int read;

            try { read = s.EndReceive(ar); }
            catch
            {
                Server.Disconnect_Client(this);// Disconnects
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

            }
            /*
            try
            {
                DataLength = Client.GetStream().EndRead(ar);// Gets the length and ends read

                if (DataLength == 0)// If Data has nothing in it
                {
                    Server.Disconnect_Client(this);// Disconnects
                    return;
                }

                Data = Data + Encoding.UTF8.GetString(Rx, 0, DataLength).Trim();// Gets the data and trims it
                if (Data.EndsWith("|<EOD>|"))
                {
                    //Fire_TCP_Data_Event(node.Data, node, DataDirection.Recieve);// Fires the Data Recieved Event
                    Server.CommandHandeler.Invoke(this, Data.Remove(Data.Length - 7, 7));// Executes the command handeler
                    Data = "";// Data Recieved
                }

                Rx = new byte[32768];//Sets the clients recieve buffer
                StartListening();
            }
            catch
            {
                Server.Disconnect_Client(this);// Disconnects
                return;
            }
            */
        }
        #endregion
        #region TX
        /// <summary>
        /// Sends data to this client, No Encryption or Serialization is performed at this step
        /// </summary>
        /// <param name="Data">The String being transmitted</param>
        public void Send(string Data)
        {
            Tx = Encoding.UTF8.GetBytes(Data + "|<EOD>|");
            Client.GetStream().BeginWrite(Tx, 0, Tx.Length, OnWrite, Client);
            Tx = new byte[32768];
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