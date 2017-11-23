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
            ClientInstance.Client.ConnectionStateEvent += Client_ConnectionStateEvent;// Connection State Command
            Setup_Commands();// Configures the commands
        }

        private static void Setup_Commands()
        {
            #region Pokedex
            ClientInstance.Client_CommandHandeler.GetCommand("Pokedex_Pokemon_Get").Command_Executed += MainWindow.Pokedex_Pokemon_Get_Executed;
            #endregion
        }

        private static void Client_ConnectionStateEvent(Networking.Data.Client_ConnectionStatus ConnectionState)
        {
            if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connected)
            {
                MainWindow.Status_Set_Color((Color)Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Connecting)
            {
                MainWindow.Status_Set_Color((Color)Colors.Yellow);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Disconnected)
            {
                MainWindow.Status_Set_Color((Color)Colors.Red);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Dropped)
            {
                MainWindow.Status_Set_Color((Color)Colors.Red);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Encrypted)
            {
                MainWindow.Status_Set_Color((Color)Colors.Green);
                MainWindow.Status_Set_Address(ClientInstance.Server_Address.ToString());
                MainWindow.Status_Set_Port(ClientInstance.Server_Port);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.Rejected)
            {
                MainWindow.Status_Set_Color((Color)Colors.DarkRed);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
            else if (ConnectionState == Networking.Data.Client_ConnectionStatus.ServerFull)
            {
                MainWindow.Status_Set_Color((Color)Colors.Orange);
                MainWindow.Status_Set_Address("");
                MainWindow.Status_Set_Port(0);
                //MainWindow.Status_Set_Ping(ClientInstance.Client);
                //MainWindow.Status_Set_PlayerName("");
                //MainWindow.Status_Set_CampaignName("");
            }
        }
    }
}
