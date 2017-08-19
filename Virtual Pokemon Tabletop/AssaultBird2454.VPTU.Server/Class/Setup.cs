using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class
{
    public static class Setup
    {
        #region Dedicated Server Start
        public static void Start_Dedicated()
        {
            Start_LoggerInit();// Configures Logger

            Main.ConsoleLogger.Log("Virtual Pokemon Tabletop Server", Logging.LoggerLevel.Info);
            Main.ConsoleLogger.Log("Version: " + Main.VersioningInfo.Version, Logging.LoggerLevel.Info);
            Main.ConsoleLogger.Log("Commit ID: " + Main.VersioningInfo.Compile_Commit.Remove(7), Logging.LoggerLevel.Info);

            Main.ConsoleLogger.Log("VPTU Server Starting!", Logging.LoggerLevel.Debug);// Logs Server Starting

            Start_Database();// Load & Connect to Database

            Start_LoadDedicatedServerSettings();// Load Server Settings

            Start_TCPInit();// Load TCP Servers

            Start_TCPSubscribe();// Subscribe to TCP Server Events

            Start_RegisterCommands();// Register Server Commands

            Start_TCPStart();// Start TCP Servers
        }

        #region Start Methods
        private static void Start_LoggerInit()
        {
            Main.ConsoleLogger = new Class.Logging.Logger();
            Main.ConsoleLogger.Setup(Main.AssemblyDirectory + "/Logs/" + DateTime.Now.ToShortDateString() + ".txt");
        }

        private static void Start_Database()
        {
            Main.ConsoleLogger.Log("Loading and Connecting to Database Server", Logging.LoggerLevel.Debug);

            try
            {
                using (System.Data.SqlClient.SqlConnection Nconn = new System.Data.SqlClient.SqlConnection(Main.SQLConnectionString))
                {
                    Nconn.Open();
                    Nconn.Close();
                    Main.ConsoleLogger.Log("Database Connection Check Passed!", Logging.LoggerLevel.Info);
                }
            }
            catch (Exception e)
            {
                Main.ConsoleLogger.Log("Database Connection Check Failed!", Logging.LoggerLevel.Fatil);
                Main.ConsoleLogger.Log(e.ToString(), Logging.LoggerLevel.Debug);
            }
        }

        private static void Start_LoadDedicatedServerSettings()
        {
            Main.ConsoleLogger.Log("Loading Server Settings From Database Server", Logging.LoggerLevel.Info);

            try
            {
                using (System.Data.SqlClient.SqlConnection Nconn = new System.Data.SqlClient.SqlConnection(Main.SQLConnectionString))
                {
                    Nconn.Open();// Open Connection

                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                    {
                        cmd.Connection = Nconn;// Sets connection
                        cmd.CommandText = "";// Sets Command

                        //Add Params Here

                        using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Get Server Config here
                            // Debug the settings retrieved
                        }

                        cmd.Dispose();// Deletes the command
                    }
                    Nconn.Close();// Closes Connection
                }
            }
            catch
            {

            }
        }
        #endregion
        #endregion
        #region Intergrated Server Start

        #endregion
        #region Management Server Start

        #endregion

        #region Generic Start Methods
        private static void Start_TCPInit()
        {

        }

        private static void Start_TCPSubscribe()
        {

        }

        private static void Start_RegisterCommands()
        {

        }

        private static void Start_TCPStart()
        {

        }
        #endregion
    }
}

// Main.ConsoleLogger.Log("", Logging.LoggerLevel.Debug);