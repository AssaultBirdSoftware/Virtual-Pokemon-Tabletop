using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class
{
    public class Setup
    {
        #region Server Start
        public void Start()
        {
            Start_LoggerInit();// Configures Logger

            Main.ConsoleLogger.Log("Virtual Pokemon Tabletop Server", Logging.LoggerLevel.Info);
            Main.ConsoleLogger.Log("Version: " + Main.VersioningInfo.Version, Logging.LoggerLevel.Info);
            Main.ConsoleLogger.Log("Commit ID: " + Main.VersioningInfo.Compile_Commit.Remove(7), Logging.LoggerLevel.Info);

            Main.ConsoleLogger.Log("VPTU Server Starting!", Logging.LoggerLevel.Debug);// Logs Server Starting

            Start_Database();// Load & Connect to Database

            Start_LoadServerSettings();// Load Server Settings

            Start_TCPInit();// Load TCP Servers

            Start_TCPSubscribe();// Subscribe to TCP Server Events

            Start_RegisterCommands();// Register Server Commands

            Start_TCPStart();// Start TCP Servers
        }

        #region Start Methods
        private void Start_LoggerInit()
        {
            Main.ConsoleLogger = new Class.Logging.Logger();
            Main.ConsoleLogger.Setup(Main.AssemblyDirectory + "/Logs/" + DateTime.Now.ToShortDateString() + ".txt");
        }

        private void Start_Database()
        {
            Main.ConsoleLogger.Log("Loading and Connecting to Database Server", Logging.LoggerLevel.Debug);
        }

        private void Start_LoadServerSettings()
        {

        }

        private void Start_TCPInit()
        {

        }

        private void Start_TCPSubscribe()
        {

        }

        private void Start_RegisterCommands()
        {

        }

        private void Start_TCPStart()
        {

        }
        #endregion
        #endregion
    }
}

// Main.ConsoleLogger.Log("", Logging.LoggerLevel.Debug);