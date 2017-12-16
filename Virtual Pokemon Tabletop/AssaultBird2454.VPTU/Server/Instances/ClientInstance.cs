using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances
{
    public class ClientInstance
    {
        #region Variables and Objects
        /// <summary>
        /// Client class for handeling Client communications
        /// </summary>
        public Networking.Client.TCP.TCP_Client Client { get; private set; }

        /// <summary>
        /// Client_CommandHandeler contains functions for creating and calling command callbacks
        /// </summary>
        public Networking.Client.Command_Handeler.Client_CommandHandeler Client_CommandHandeler { get; private set; }
        public Client.Base_Commands Base_Client_Commands { get; private set; }

        private object Logger;
        /// <summary>
        /// Client_Logger contains functions for Client logging
        /// </summary>
        public object Client_Logger
        {
            get
            {
                return Logger;
            }
            set
            {
                if (value is Class.Logging.I_Logger)
                {
                    Logger = value;
                }
                else
                {
                    throw new Class.Logging.Invalid_Logger_Class_Exception();
                }
            }
        }

        public IPAddress Server_Address { get; set; }
        public int Server_Port { get; set; }
        #endregion

        public ClientInstance(Class.Logging.I_Logger _Logger, IPAddress _Server_Address, int Server_Port = 25444)
        {
            #region Logs
            Client_Logger = _Logger;
            ((Class.Logging.I_Logger)Client_Logger).Setup(Class.Logging.Logger_Type.Client);
            #endregion
            #region Command Handeler
            ((Class.Logging.I_Logger)Client_Logger).Log("Initilizing Command Handeler", Class.Logging.LoggerLevel.Debug);
            Client_CommandHandeler = new Networking.Client.Command_Handeler.Client_CommandHandeler();
            Client_CommandHandeler.CommandRegistered += Client_CommandHandeler_CommandRegistered;
            Client_CommandHandeler.CommandUnRegistered += Client_CommandHandeler_CommandUnRegistered;

            Base_Client_Commands = new Client.Base_Commands(this);

            ((Class.Logging.I_Logger)Client_Logger).Log("Registering Base Client Commands", Class.Logging.LoggerLevel.Debug);
            Base_Client_Commands.Register_Commands(Client_CommandHandeler);

            #endregion
            #region Networking
            ((Class.Logging.I_Logger)Client_Logger).Log("Initilizing Base Network", Class.Logging.LoggerLevel.Debug);
            Server_Address = _Server_Address;
            Client = new Networking.Client.TCP.TCP_Client(Server_Address, Client_CommandHandeler, Server_Port);
            Client.ConnectionStateEvent += Client_ConnectionStateEvent;
            Client.DataEvent += Client_DataEvent;
            #endregion

            #region Plugins

            #endregion

            ((Class.Logging.I_Logger)Client_Logger).Log("Client Ready!", Class.Logging.LoggerLevel.Info);
        }

        public void StartClientInstance()
        {
            #region Start Client
            ((Class.Logging.I_Logger)Client_Logger).Log("Connecting to Server", Class.Logging.LoggerLevel.Info);
            try
            {
                Client.Connect();
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Connected to Server", Class.Logging.LoggerLevel.Info);
            }
            catch (Exception e)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Failed to Connect to Server", Class.Logging.LoggerLevel.Fatil);
                ((Class.Logging.I_Logger)Client_Logger).Log(e.ToString(), Class.Logging.LoggerLevel.Debug);
                StopClientInstance();
                return;
            }
            #endregion
        }
        public void StopClientInstance()
        {
            #region Stop Client
            ((Class.Logging.I_Logger)Client_Logger).Log("Disconnecting from Server", Class.Logging.LoggerLevel.Info);
            try
            {
                Client.Disconnect();
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Disconnected from Server", Class.Logging.LoggerLevel.Info);
            }
            catch (Exception e)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Failed to Disconnect from Server", Class.Logging.LoggerLevel.Fatil);
                ((Class.Logging.I_Logger)Client_Logger).Log(e.ToString(), Class.Logging.LoggerLevel.Debug);
                return;
            }
            #endregion
        }
        public void AuthenticateClient(SaveManager.Identity.Identity_Data ID)
        {
            Client.SendData(new CommandData.Auth.Login()
            {
                Client_Key = ID.Key
            });
        }

        private void Client_CommandHandeler_CommandUnRegistered(string Command)
        {
            ((Class.Logging.I_Logger)Client_Logger).Log("Command Unregistered -> Command: " + Command, Class.Logging.LoggerLevel.Debug);
        }
        private void Client_CommandHandeler_CommandRegistered(string Command)
        {
            ((Class.Logging.I_Logger)Client_Logger).Log("Command Registered -> Command: " + Command, Class.Logging.LoggerLevel.Debug);
        }

        private void Client_DataEvent(string Data, Networking.Client.TCP.DataDirection Direction)
        {
            if (Direction == Networking.Client.TCP.DataDirection.Recieve)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Data Recieved from Server", Class.Logging.LoggerLevel.Debug);
            }
            else if (Direction == Networking.Client.TCP.DataDirection.Send)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Data Sent to Server", Class.Logging.LoggerLevel.Debug);
            }
        }
        private void Client_ConnectionStateEvent(Networking.Data.Client_ConnectionStatus ConnectionState)
        {
            if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connected)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Connected (Base Network)", Class.Logging.LoggerLevel.Info);
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connecting)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Connecting (Base Network)", Class.Logging.LoggerLevel.Info);
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Disconnected)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Disconnected (Base Network)", Class.Logging.LoggerLevel.Info);
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Dropped)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Dropped Connection (Base Network)", Class.Logging.LoggerLevel.Info);
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Encrypted)
            {
                ((Class.Logging.I_Logger)Client_Logger).Log("Client Encrypted Network Transmittions (Base Network)", Class.Logging.LoggerLevel.Info);
            }
        }
    }
}
// ((Class.Logging.I_Logger)Client_Logger).Log("", Class.Logging.LoggerLevel.Info);