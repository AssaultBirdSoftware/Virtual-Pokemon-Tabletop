using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class
{
    public class Server
    {
        /// <summary>
        /// Defines what logging class to use
        /// </summary>
        public Logging.Logger Logger { get; set; }
        /// <summary>
        /// Defines the SaveManager for use with this server instance
        /// </summary>
        public SaveManager.SaveManager SaveManager { get; set; }
        /// <summary>
        /// Handels networking
        /// </summary>
        public Networking.Server.TCP.TCP_Server TCP_Server { get; set; }
        /// <summary>
        /// Defines an object that can handel command handeling
        /// </summary>
        public Networking.Server.Command_Handeler.Server_CommandHandeler TCP_CommandHandeler { get; set; }

        /// <summary>
        /// Creates a new VPTU Server Instance
        /// </summary>
        public Server(Logging.Logger _Logger, SaveManager.SaveManager _SaveManager)
        {
            Logger = _Logger;

            // Base Server Init
            Logger.Log("Initializing Server", Logging.LoggerLevel.Debug);
            Logger.Log("Creating Command Handeler", Logging.LoggerLevel.Debug);
            TCP_CommandHandeler = new Networking.Server.Command_Handeler.Server_CommandHandeler();
            Logger.Log("Registering Commands", Logging.LoggerLevel.Debug);
            // Register commands

            // Opens the save file
            Logger.Log("Opening save file", Logging.LoggerLevel.Debug);
            SaveManager.Load_SaveData();

            // Creates the networking interfaces
            Logger.Log("Initializing Networking interfaces", Logging.LoggerLevel.Debug);
            TCP_Server = new Networking.Server.TCP.TCP_Server(IPAddress.Any, TCP_CommandHandeler, 25444);
        }
    }
}
