using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using AssaultBird2454.VPTU.Networking.Client.Command_Handeler;
using AssaultBird2454.VPTU.Networking.Data;
using AssaultBird2454.VPTU.Networking.Shared;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    public enum NetworkMode
    {
        Standard,
        SSL
    }

    public class TCP_Client
    {
        public TCP_Client(IPAddress Address, Client_CommandHandeler _CommandHandeler, int Port = 25444)
        {
            NetMode = NetworkMode.Standard;
            ReadQueWait = new EventWaitHandle(false, EventResetMode.AutoReset, "ReadQue");
            IPAddress = Address; // Sets the IPAddress
            this.Port = Port; // Sets the Port
            CommandHandeler = _CommandHandeler; // Sets the Command Callback

            try
            {
                CommandHandeler.RegisterCommand<InternalNetworkCommand>("Network Command", Client_Commands);
            }
            catch (CommandNameTakenException e)
            {
            }
        }

        private void Client_Commands(object _Data)
        {
            var Data = (InternalNetworkCommand) _Data;

            if (Data.CommandType == Commands.SSL_Enable)
            {
                if (Data.Response == ResponseCode.Not_Avaliable)
                {
                    // Not Avaliable
                }
                else if (Data.Response == ResponseCode.Avaliable)
                {
                    var sslStream = new SslStream(Client.GetStream(), true, ValidateCert);
                    StateObject.SSL = sslStream;
                    //new SslStream(Client.GetStream(), true, new RemoteCertificateValidationCallback(ValidateCert), null, EncryptionPolicy.RequireEncryption);
                    SendData(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.Ready));
                }
                else if (Data.Response == ResponseCode.Ready)
                {
                    NetMode = NetworkMode.SSL;
                    SendData(new InternalNetworkCommand(Commands.SSL_Enable, ResponseCode.OK));
                    Fire_ConnectionStateEvent(Client_ConnectionStatus.Encrypted);
                }
                else if (Data.Response == ResponseCode.Error)
                {
                    // Error
                }
            }
            else if (Data.CommandType == Commands.SSL_Dissable)
            {
            }
            else if (Data.CommandType == Commands.SSL_Active)
            {
            }
            else if (Data.CommandType == Commands.SetBufferSize)
            {
            }
        }

        private bool ValidateCert(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #region RX Message Que

        public void QueRead()
        {
            while (true)
            {
                ReadQueWait.WaitOne(); // Waits for data

                while (DataQue.Count >= 1) // While there is data
                {
                    var Data = DataQue.Dequeue(); // Gets the data and then removes it from the que
                    Fire_DataEvent(Data, DataDirection.Recieve); // Invokes the data recieved event
                    CommandHandeler.InvokeCommand(Data); // Handels the data
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     An event that is fired when the connection state changes
        /// </summary>
        public event TCP_ConnectionState_Handeler ConnectionStateEvent;

        private void Fire_ConnectionStateEvent(Client_ConnectionStatus state)
        {
            ConnectionStateEvent?.Invoke(state);
        }

        /// <summary>
        ///     An Event that is fired when the client transmitts or recieves data from the server
        /// </summary>
        public event TCP_Data DataEvent;

        private void Fire_DataEvent(string Data, DataDirection Direction)
        {
            DataEvent?.Invoke(Data, Direction);
        }

        #endregion

        #region Variables

        private TcpClient Client; // Client Object
        private StateObject StateObject; // Client State Object
        private readonly string[] delimiter = {"|<EOD>|"}; // The Delimiting string for commands

        private NetworkMode NetMode;

        #region Data Que

        private Queue<string> DataQue; // Data Que
        private Thread DataQueThread; // A Thread to run the data que
        private readonly EventWaitHandle ReadQueWait; // An event for signaling to the reader that data is in Que

        #endregion

        /// <summary>
        ///     The Command Handeler that the server will use to invoke commands
        /// </summary>
        public Client_CommandHandeler CommandHandeler;

        #endregion

        #region Variable Handelers

        /// <summary>
        ///     Get or Set the IPAddress. IPAddress wont be affected if client is connected
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        ///     Get or Set the Port. Port wont be affected if client is connected
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Checks if the client is connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (Client != null)
                    return Client.Connected;
                return false;
            }
        }

        #endregion

        #region Client Methods

        /// <summary>
        ///     Connects to the server, it will also disconnect any connections
        /// </summary>
        public void Connect()
        {
            Disconnect(); // Disconnects (No Error if it was never connected)

            Fire_ConnectionStateEvent(Client_ConnectionStatus.Connecting);

            Client = new TcpClient(); // Creates a new client object

            try
            {
                Client.Connect(IPAddress,
                    Port); // Connects to the server and on connect will call the connect call back
            }
            catch (Exception e)
            {
                Client = null;
                Fire_ConnectionStateEvent(Client_ConnectionStatus.Disconnected);
                return;
            }

            DataQue = new Queue<string>(); // Creates the que

            DataQueThread = new Thread(() => { QueRead(); });
            DataQueThread.IsBackground = true;
            DataQueThread.Start(); // Creates and Starts the Read Thread

            StateObject = new StateObject(Client.Client, 32768); // Creates State Object for Client
            StartListening(); // Starts Listening

            Fire_ConnectionStateEvent(Client_ConnectionStatus.Connected);
        }

        /// <summary>
        ///     Disconnects the Client from the server
        /// </summary>
        public void Disconnect()
        {
            try
            {
                DataQueThread.Abort(); // Stopps the read thread
                DataQueThread = null; // Clears the read Thread

                Client.Close(); // Closes connection
                Client = null; // Clears Client

                Fire_ConnectionStateEvent(Client_ConnectionStatus.Disconnected);
            }
            catch
            {
                /* Failed to disconnect or was never connected to anything */
            }
        }

        public void Enable_SSL()
        {
            SendData(new InternalNetworkCommand(Commands.SSL_Enable));
        }

        #endregion

        #region Data Events

        /// <summary>
        ///     A Method to start the reading of network data again
        /// </summary>
        private void StartListening()
        {
            if (NetMode == NetworkMode.Standard)
                Client.GetStream().BeginRead(StateObject.buffer, 0, 32768, Client_DataRecv, StateObject);
            else if (NetMode == NetworkMode.SSL)
                StateObject.SSL.BeginRead(StateObject.buffer, 0, 32768, Client_DataSslRecv, StateObject);
        }

        private void Client_DataRecv(IAsyncResult ar)
        {
            var so = (StateObject) ar.AsyncState;
            var s = so.workSocket;
            int read;

            try
            {
                read = s.EndReceive(ar);
            }
            catch
            {
                Disconnect(); // Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read));

                if (so.sb.ToString().Contains("|<EOD>|"))
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string[] strContent;
                        strContent = so.sb.ToString().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                        so.Reset();
                        foreach (var data in strContent)
                        {
                            DataQue.Enqueue(data);
                            ReadQueWait.Set();
                        }
                    }
                if (NetMode == NetworkMode.Standard)
                    StartListening();
            }
            else
            {
                Disconnect(); // Disconnects
            }
        }

        private void Client_DataSslRecv(IAsyncResult ar)
        {
            var so = (StateObject) ar.AsyncState;
            var s = so.workSocket;
            int read;

            try
            {
                read = s.EndReceive(ar);
            }
            catch
            {
                Disconnect(); // Disconnects
                return;
            }

            if (read > 0)
            {
                so.sb.Append(Encoding.ASCII.GetString(so.buffer, 0, read));

                if (so.sb.ToString().Contains("|<EOD>|"))
                    if (so.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string[] strContent;
                        strContent = so.sb.ToString().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                        so.Reset();
                        foreach (var data in strContent)
                        {
                            DataQue.Enqueue(data);
                            ReadQueWait.Set();
                        }
                    }
                if (NetMode == NetworkMode.SSL)
                    StartListening();
            }
            else
            {
                Disconnect(); // Disconnects
            }
        }

        private void Client_DataTran(IAsyncResult ar)
        {
            try
            {
                var tcpc = (TcpClient) ar.AsyncState; //Gets the client data is going to
                tcpc.GetStream().EndWrite(ar); //Ends client write stream
            }
            catch (Exception e)
            {
                //TCP_Data_Error_Event.Invoke(e, DataDirection.Send);
                /* Transmition Error */
            }
        }

        private void Client_DataSslTran(IAsyncResult ar)
        {
            try
            {
                var tcpc = (TcpClient) ar.AsyncState; //Gets the client data is going to
                tcpc.GetStream().EndWrite(ar); //Ends client write stream
            }
            catch (Exception e)
            {
                //TCP_Data_Error_Event.Invoke(e, DataDirection.Send);
                /* Transmition Error */
            }
        }

        /// <summary>
        ///     Sends data to the server
        /// </summary>
        /// <param name="Data">The data being sent to the server</param>
        public void SendData(object Data)
        {
            if (Data is Data.NetworkCommand)
            {
                if (IsConnected)
                    if (NetMode == NetworkMode.Standard)
                    {
                        var JSONData = JsonConvert.SerializeObject(Data); // Serialises the data to be sent
                        Fire_DataEvent(JSONData, DataDirection.Send); // Invokes the data recieved event
                        var Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                        Client.GetStream()
                            .BeginWrite(Tx, 0, Tx.Length, Client_DataTran, Client); // Sneds the data to the server
                    }
                    else
                    {
                        var JSONData = JsonConvert.SerializeObject(Data); // Serialises the data to be sent
                        Fire_DataEvent(JSONData, DataDirection.Send); // Invokes the data recieved event
                        var Tx = Encoding.UTF8.GetBytes(JSONData + "|<EOD>|");
                        StateObject.SSL.BeginWrite(Tx, 0, Tx.Length, Client_DataSslTran,
                            StateObject.SSL); // Sneds the data to the server
                    }
            }
            else
            {
                throw new NotNetworkDataException();
            }
        }

        #endregion
    }
}