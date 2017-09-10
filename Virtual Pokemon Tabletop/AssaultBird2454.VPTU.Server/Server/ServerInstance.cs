using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Server
{
    public class ServerInstance
    {
        /// <summary>
        /// Server class for handeling server communications
        /// </summary>
        public Networking.Server.TCP.TCP_Server Server { get; private set; }

        /// <summary>
        /// Server_CommandHandeler contains functions for creating and calling command callbacks
        /// </summary>
        public Networking.Server.Command_Handeler.Server_CommandHandeler Server_CommandHandeler { get; private set; }

        /// <summary>
        /// Server_Logger contains functions for server logging
        /// </summary>
        public object Server_Logger { get; set; }

        /// <summary>
        /// Save Manager class, contains Save Data
        /// </summary>
        public SaveManager.SaveManager SaveManager { get; private set; }

        public ServerInstance(string SaveData, Class.Logging.Logger _Logger = null)
        {
            #region Logs
            if (_Logger == null)
            {
                Server_Logger = new Class.Logging.Logger();
            }
            else
            {
                Server_Logger = _Logger;
            }
            #endregion
            #region SaveManager
            ((Class.Logging.I_Logger)Server_Logger).Log("Initilizing Save Manager", Class.Logging.LoggerLevel.Debug);
            SaveManager = new VPTU.SaveManager.SaveManager(SaveData);// Create Manager Object

            ((Class.Logging.I_Logger)Server_Logger).Log("Loading Save Data", Class.Logging.LoggerLevel.Debug);
            SaveManager.Load_SaveData();// Load Data
            #endregion
            #region Command Handeler
            ((Class.Logging.I_Logger)Server_Logger).Log("Initilizing Command Handeler", Class.Logging.LoggerLevel.Debug);
            Server_CommandHandeler = new Networking.Server.Command_Handeler.Server_CommandHandeler();
            Server_CommandHandeler.CommandRegistered += Server_CommandHandeler_CommandRegistered;
            Server_CommandHandeler.CommandUnRegistered += Server_CommandHandeler_CommandUnRegistered;
            #endregion
            #region Networking
            ((Class.Logging.I_Logger)Server_Logger).Log("Initilizing Base Network", Class.Logging.LoggerLevel.Debug);
            Server = new Networking.Server.TCP.TCP_Server(IPAddress.Any, Server_CommandHandeler, 25444);
            Server.TCP_AcceptClients_Changed += Server_TCP_AcceptClients_Changed;
            Server.TCP_ClientState_Changed += Server_TCP_ClientState_Changed;
            Server.TCP_Data_Error_Event += Server_TCP_Data_Error_Event;
            Server.TCP_Data_Event += Server_TCP_Data_Event;
            Server.TCP_ServerState_Changed += Server_TCP_ServerState_Changed;
            #endregion
            #region Save Data
            ((Class.Logging.I_Logger)Server_Logger).Log("Initilizing Save Manager", Class.Logging.LoggerLevel.Debug);
            SaveManager = new VPTU.SaveManager.SaveManager(SaveData);

            ((Class.Logging.I_Logger)Server_Logger).Log("Loading Campaign", Class.Logging.LoggerLevel.Debug);
            SaveManager.Load_SaveData();
            #endregion

            #region Plugins

            #endregion

            #region Start Server
            ((Class.Logging.I_Logger)Server_Logger).Log("Starting Base Network", Class.Logging.LoggerLevel.Info);
            try
            {
                Server.Start();
                ((Class.Logging.I_Logger)Server_Logger).Log("Base Network Started", Class.Logging.LoggerLevel.Info);
            }
            catch
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Base Network Failed to Start", Class.Logging.LoggerLevel.Fatil);
                StopInstance();
                return;
            }
            #endregion
        }

        public void StopInstance()
        {

        }

        private void Server_CommandHandeler_CommandUnRegistered(string Command, string Callback = "")
        {
            ((Class.Logging.I_Logger)Server_Logger).Log("Command Unregistered -> Command: " + Command, Class.Logging.LoggerLevel.Debug);
        }
        private void Server_CommandHandeler_CommandRegistered(string Command, string Callback = "")
        {
            ((Class.Logging.I_Logger)Server_Logger).Log("Command Registered -> Command: " + Command, Class.Logging.LoggerLevel.Debug);
        }

        private void Server_TCP_ServerState_Changed(Networking.Data.Server_Status Server_State)
        {
            ((Class.Logging.I_Logger)Server_Logger).Log("Base Network is " + Server_State.ToString(), Class.Logging.LoggerLevel.Debug);
        }
        private void Server_TCP_Data_Event(string Data, Networking.Server.TCP.TCP_ClientNode Client, Networking.Server.TCP.DataDirection Direction)
        {
            if (Direction == Networking.Server.TCP.DataDirection.Recieve)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Data Recieved from " + Client.ID, Class.Logging.LoggerLevel.Debug);
            }
            else if (Direction == Networking.Server.TCP.DataDirection.Send)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Data Sent to " + Client.ID, Class.Logging.LoggerLevel.Debug);
            }
        }
        private void Server_TCP_Data_Error_Event(Exception ex, Networking.Server.TCP.DataDirection Direction)
        {
            ((Class.Logging.I_Logger)Server_Logger).Log("Failed to " + Direction.ToString() + " Data (Base Network)", Class.Logging.LoggerLevel.Error);
        }
        private void Server_TCP_ClientState_Changed(Networking.Server.TCP.TCP_ClientNode Client, Networking.Data.Client_ConnectionStatus Client_State)
        {
            if (Client_State == Networking.Data.Client_ConnectionStatus.Connected)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Connected (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.Connecting)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Connecting (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.Disconnected)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Disconnected (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.Dropped)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Dropped Connection (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.Encrypted)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Encrypted Network Transmittions (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.Rejected)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Client Rejected (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
            else if (Client_State == Networking.Data.Client_ConnectionStatus.ServerFull)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Server Full, Client Rejected (Base Network) Address: " + Client.ID, Class.Logging.LoggerLevel.Info);
            }
        }
        private void Server_TCP_AcceptClients_Changed(bool Accepting_Connections)
        {
            if (Accepting_Connections)
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Base Network is Accepting New Connections", Class.Logging.LoggerLevel.Info);
            }
            else
            {
                ((Class.Logging.I_Logger)Server_Logger).Log("Base Network is Refusing New Connections", Class.Logging.LoggerLevel.Info);
            }
        }
    }
}
