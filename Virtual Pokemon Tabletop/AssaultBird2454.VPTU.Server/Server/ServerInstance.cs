using System;
using System.Collections.Generic;
using System.Linq;
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
        public Class.Logging.Logger Server_Logger { get; set; }

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
            Server_Logger.Log("Initilizing Save Manager", Class.Logging.LoggerLevel.Debug);
            SaveManager = new VPTU.SaveManager.SaveManager(SaveData);// Create Manager Object

            Server_Logger.Log("Loading Save Data", Class.Logging.LoggerLevel.Debug);
            SaveManager.Load_SaveData();// Load Data
            #endregion
            #region Command Handeler
            Server_Logger.Log("Initilizing Command Handeler", Class.Logging.LoggerLevel.Debug);
            Server_CommandHandeler = new Networking.Server.Command_Handeler.Server_CommandHandeler();
            Server_CommandHandeler.CommandRegistered += Server_CommandHandeler_CommandRegistered;
            Server_CommandHandeler.CommandUnRegistered += Server_CommandHandeler_CommandUnRegistered;
            #endregion
            #region Networking

            #endregion

            #region Plugins

            #endregion
        }

        private void Server_CommandHandeler_CommandUnRegistered(string Command, string Callback = "")
        {
            throw new NotImplementedException();
        }

        private void Server_CommandHandeler_CommandRegistered(string Command, string Callback = "")
        {
            throw new NotImplementedException();
        }
    }
}
