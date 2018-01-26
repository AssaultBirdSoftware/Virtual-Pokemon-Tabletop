using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media;
using AssaultBird2454.VPTU.Authentication_Manager.Data;
using AssaultBird2454.VPTU.Networking.Data;
using AssaultBird2454.VPTU.SaveManager.Data.SaveFile;
using AssaultBird2454.VPTU.Server.Class.Logging;
using AssaultBird2454.VPTU.Server.Instances;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Connection;
using Timer = System.Timers.Timer;

namespace AssaultBird2454.VPTU.Client
{
    public static class Program
    {
        public static ProjectInfo VersioningInfo
        {
            get
            {
                using (var str = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("AssaultBird2454.VPTU.Client.ProjectVariables.json"))
                {
                    using (var read = new StreamReader(str))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    }
                }
            }
        }

        /// <summary>
        ///     Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static MainWindow MainWindow { get; internal set; }
        public static ClientInstance ClientInstance { get; set; }
        public static ServerInstance ServerInstance { get; set; }
        public static PTUSaveData DataCache { get; set; }
        private static Timer Ping_Timer { get; set; }
        public static List<ClientIdentity> Identities { get; set; }

        public static NotifyIcon NotifyIcon { get; internal set; }

        public static object ClientLogger
        {
            get
            {
                return ClientInstance.Client_Logger;
            }
            set
            {
                if (ClientInstance != null)
                    if (value is I_Logger)
                        ClientInstance.Client_Logger = value;
            }
        }

        public static void Settings_Save()
        {
            #region Identities

            try
            {
                File.Delete(AssemblyDirectory + @"\Client_Identities.json");
                using (var sw =
                    new StreamWriter(new FileStream(AssemblyDirectory + @"\Client_Identities.json",
                        FileMode.OpenOrCreate)))
                {
                    string Client_Identities = Newtonsoft.Json.JsonConvert.SerializeObject(Identities);
                    sw.WriteLine(Client_Identities);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "There was a error saving identities to file!\nNo Identities have been saved, This means that you will need to create / add them again before you connect to the servers again...",
                    "Error Saving Identities to File");
                MessageBox.Show(ex.ToString(), "Stack Trace");
            }

            #endregion
        }

        public static void Settings_Load()
        {
            #region Identities

            try
            {
                using (var sr =
                    new StreamReader(new FileStream(AssemblyDirectory + @"\Client_Identities.json",
                        FileMode.OpenOrCreate)))
                {
                    var Client_Identities = sr.ReadToEnd();
                    Identities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClientIdentity>>(Client_Identities);
                }
            }
            catch
            {
                Identities = new List<ClientIdentity>();
            }

            if (Identities == null) Identities = new List<ClientIdentity>();

            #endregion
        }

        internal static void Setup_Client()
        {
            Ping_Timer = new Timer(); // Creates a timer
            Ping_Timer.Interval = 3000; // Sets for 3 Second intervals
            Ping_Timer.Elapsed += Ping_Timer_Elapsed; // Sets event

            ClientInstance.Client.ConnectionStateEvent += Client_ConnectionStateEvent; // Connection State Command
            Setup_Commands(); // Configures the commands
        }

        private static void Setup_Commands()
        {
            ClientInstance.Client_CommandHandeler.GetCommand("ConnectionState").Command_Executed +=
                ConnectionState_Executed;

            #region Auth

            ClientInstance.Client_CommandHandeler.GetCommand("Auth_Login").Command_Executed +=
                MainWindow.Auth_Login_Executed;

            #endregion

            #region Pokedex

            ClientInstance.Client_CommandHandeler.GetCommand("Pokedex_Pokemon_GetList").Command_Executed +=
                MainWindow.Pokedex_Pokemon_GetList_Executed;
            ClientInstance.Client_CommandHandeler.GetCommand("Pokedex_Pokemon_Get").Command_Executed +=
                MainWindow.Pokedex_Pokemon_Get_Executed;

            #endregion

            #region Resources

            ClientInstance.Client_CommandHandeler.GetCommand("Resources_Image_Get").Command_Executed +=
                MainWindow.Resources_Image_Get_Pokedex_Executed;

            #endregion

            #region Entities

            ClientInstance.Client_CommandHandeler.GetCommand("Entities_All_GetList").Command_Executed +=
                MainWindow.Entities_All_GetList_Executed;
            ClientInstance.Client_CommandHandeler.GetCommand("Entities_Pokemon_Get").Command_Executed +=
                MainWindow.Entities_Pokemon_Get_Executed;
            ClientInstance.Client_CommandHandeler.GetCommand("Resources_Image_Get").Command_Executed +=
                MainWindow.Resources_Image_Get_Entities_Executed;

            #endregion
        }

        private static void Client_ConnectionStateEvent(Client_ConnectionStatus ConnectionState)
        {
            if (ConnectionState == Client_ConnectionStatus.Connected)
            {
                MainWindow.Status_Set_Color(Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");

                Ping_Timer.Start();
            }
            else if (ConnectionState == Client_ConnectionStatus.Connecting)
            {
                MainWindow.Status_Set_Color(Colors.Yellow);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Client_ConnectionStatus.Disconnected)
            {
                MainWindow.Status_Set_Color(Colors.Red);
                MainWindow.Status_Set_Address("Not Connected");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Client_ConnectionStatus.Dropped)
            {
                MainWindow.Status_Set_Color(Colors.Red);
                MainWindow.Status_Set_Address("Not Connected");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Client_ConnectionStatus.Encrypted)
            {
                MainWindow.Status_Set_Color(Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");

                Ping_Timer.Start();
            }
        }

        private static void Ping_Timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                MainWindow.Status_Set_Ping(Convert.ToInt32(ClientInstance.Client.PingServer));
            }
            catch
            {
                Ping_Timer.Stop();
                MainWindow.Status_Set_Ping(0);
            }
        }

        private static void ConnectionState_Executed(object Data)
        {
            var Connect = (Connect)Data;

            if (Connect.Connection_State == ConnectionStatus.Rejected)
                MessageBox.Show("The server rejected your connection request...\n\nReason: Server Locked");
            else if (Connect.Connection_State == ConnectionStatus.ServerFull)
                MessageBox.Show("The server rejected your connection request...\n\nReason: Server Full");
        }
    }
}