using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace AssaultBird2454.VPTU.Client
{
    public static class Program
    {
        public static ProjectInfo VersioningInfo
        {
            get
            {
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.Client.ProjectVariables.json"))
                {
                    using (StreamReader read = new StreamReader(str))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    }
                }
            }
        }

        /// <summary>
        /// Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        public static MainWindow MainWindow { get; internal set; }
        public static Server.Instances.ClientInstance ClientInstance { get; set; }
        public static Server.Instances.ServerInstance ServerInstance { get; set; }
        public static SaveManager.Data.SaveFile.PTUSaveData DataCache { get; set; }
        private static Timer Ping_Timer { get; set; }

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
                {
                    if (value is VPTU.Server.Class.Logging.I_Logger)
                    {
                        ClientInstance.Client_Logger = value;
                    }
                    else
                    {

                    }
                }
            }
        }

        internal static void Setup_Client()
        {
            Ping_Timer = new Timer();// Creates a timer
            Ping_Timer.Interval = 3000;// Sets for 3 Second intervals
            Ping_Timer.Tick += Ping_Timer_Tick;// Sets event

            ClientInstance.Client.ConnectionStateEvent += Client_ConnectionStateEvent;// Connection State Command
            Setup_Commands();// Configures the commands
        }

        private static void Setup_Commands()
        {
            #region Pokedex
            ClientInstance.Client_CommandHandeler.GetCommand("Pokedex_Pokemon_GetList").Command_Executed += MainWindow.Pokedex_Pokemon_GetList_Executed;
            ClientInstance.Client_CommandHandeler.GetCommand("Pokedex_Pokemon_Get").Command_Executed += MainWindow.Pokedex_Pokemon_Get_Executed;
            #endregion
            #region Resources
            ClientInstance.Client_CommandHandeler.GetCommand("Resources_Image_Get").Command_Executed += MainWindow.Resources_Image_Get_Pokedex_Executed;
            #endregion
            #region Entity
            ClientInstance.Client_CommandHandeler.GetCommand("Entity_All_GetList").Command_Executed += MainWindow.Entity_All_GetList_Executed;
            ClientInstance.Client_CommandHandeler.GetCommand("Resources_Image_Get").Command_Executed += MainWindow.Resources_Image_Get_Entity_Executed;
            #endregion
        }

        private static void Client_ConnectionStateEvent(Networking.Data.Client_ConnectionStatus ConnectionState)
        {
            if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connected)
            {
                MainWindow.Status_Set_Color((Color)Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
                
                Ping_Timer.Start();
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connecting)
            {
                MainWindow.Status_Set_Color((Color)Colors.Yellow);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Disconnected)
            {
                MainWindow.Status_Set_Color((Color)Colors.Red);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Dropped)
            {
                MainWindow.Status_Set_Color((Color)Colors.Red);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Encrypted)
            {
                MainWindow.Status_Set_Color((Color)Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");

                Ping_Timer.Start();
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Rejected)
            {
                MainWindow.Status_Set_Color((Color)Colors.DarkRed);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.ServerFull)
            {
                MainWindow.Status_Set_Color((Color)Colors.Orange);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
        }

        private static void Ping_Timer_Tick(object sender, EventArgs e)
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
    }
}
