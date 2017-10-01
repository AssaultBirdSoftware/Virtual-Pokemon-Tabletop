using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Base Data
        public ProjectInfo VersioningInfo { get; }

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

        public MainWindow()
        {
            try
            {
                if (File.Exists(AssemblyDirectory + "\\Client.pid"))
                {
                    if (Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\Launcher.pid"))).ProcessName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show("Process Already Running!");
                        this.Close();
                        return;
                    }
                    else
                    {
                        File.Delete(AssemblyDirectory + "\\Client.pid");
                    }
                }

                File.WriteAllText(AssemblyDirectory + "\\Client.pid", Process.GetCurrentProcess().Id.ToString());
            }
            catch
            {

            }

            InitializeComponent();

            Client_Pokedex.Reload_Pressed += Pokedex_Reload_Event;
            Client_Pokedex.Pokedex_Entry_Selection_Changed_Event += Client_Pokedex_Pokedex_Entry_Selection_Changed_Event;

            //Dock.LayoutRootPanel.Children.Add();

            #region Versioning Info
            using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.Client.ProjectVariables.json"))
            {
                using (StreamReader read = new StreamReader(str))
                {
                    VersioningInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    this.Title = "Virtual Pokemon Tabletop - Client (Version: " + VersioningInfo.Version + ") (Commit: " + VersioningInfo.Compile_Commit.Remove(7) + ")";
                }
            }
            #endregion
        }

        private void Client_Pokedex_Pokedex_Entry_Selection_Changed_Event(Class.Controls.Pokedex_Entry_Type type, object Data)
        {
           if(type == Class.Controls.Pokedex_Entry_Type.Pokemon)
            {
                Pokedex_Viewer_Pokemon.Load((Pokedex.Pokemon.PokemonData)Data);
            }
        }

        private void Pokedex_Reload_Event()
        {
            try
            {
                Client_Instance.Client.SendData(new Server.Instances.CommandData.Pokedex.Get_Pokedex_Pokemon());
            }
            catch { }
        }
        #endregion
        private void Menu_Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Main_Closed(object sender, EventArgs e)
        {
            File.Delete(AssemblyDirectory + "\\Client.pid");
        }

        private void Menu_Window_SaveLayout_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Layout
        public void Dock_SaveCurrent(string File)
        {
            new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(Dock).Serialize(File);
        }
        public void Dock_LoadLayout(string File)
        {
            new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(Dock).Deserialize(File);
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Directory.Exists(AssemblyDirectory + "/Layouts"))
                Directory.CreateDirectory(AssemblyDirectory + "/Layouts");

            Dock_SaveCurrent(AssemblyDirectory + "/Layouts/Default.xml");
        }
        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(AssemblyDirectory + "/Layouts/Default.xml"))
            {
                Dock_LoadLayout(AssemblyDirectory + "/Layouts/Default.xml");
            }
        }
        private void Menu_Window_LoadLayout_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Client
        Server.Instances.ClientInstance Client_Instance { get; set; }
        public void Setup_Command_Links()
        {
            Client_Instance.Client_CommandHandeler.GetCommand("Get_Pokedex_Pokemon").Command_Executed += Client_Pokedex.Update_PokedexData;
        }
        #endregion

        #region Open / Close Campaign Session (LAN SESSION)
        /// <summary>
        /// Defines an object that acts as a server over a LAN
        /// </summary>
        public Server.Instances.ServerInstance Session_LanServer { get; set; }

        private void Menu_Menu_OpenGame_Click(object sender, RoutedEventArgs e)
        {
            Menu_Menu_OpenGame.IsEnabled = false;
            Menu_Menu_CloseGame.IsEnabled = true;
            Menu_Menu_Connect.IsEnabled = false;
            Menu_Menu_Disconnect.IsEnabled = false;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.Multiselect = false;
            openFile.Title = "Open Virtual PTU Save File";
            openFile.DefaultExt = ".ptu";

            bool? Open = openFile.ShowDialog();

            if (Open == true)
            {
                Client_Console_Pane.LogDebug = true;
                Server_Console_Pane.LogDebug = true;

                Session_LanServer = new Server.Instances.ServerInstance(openFile.FileName, Server_Console_Pane);
                Session_LanServer.StartServerInstance();

                Client_Instance = new Server.Instances.ClientInstance(Client_Console_Pane, IPAddress.Parse("127.0.0.1"));
                Setup_Command_Links();// Configures callbacks for commands from the server
                Client_Instance.StartClientInstance();
            }
            else
            {
                Menu_Menu_OpenGame.IsEnabled = true;
                Menu_Menu_CloseGame.IsEnabled = false;
                Menu_Menu_Connect.IsEnabled = true;
                Menu_Menu_Disconnect.IsEnabled = false;
            }
        }
        private void Menu_Menu_CloseGame_Click(object sender, RoutedEventArgs e)
        {
            Menu_Menu_OpenGame.IsEnabled = true;
            Menu_Menu_CloseGame.IsEnabled = false;
            Menu_Menu_Connect.IsEnabled = true;
            Menu_Menu_Disconnect.IsEnabled = false;
        }
        #endregion
        #region Connect to / Disconnect from Session (INTERNET OR LAN SESSIONS)
        private void Menu_Menu_Connect_Click(object sender, RoutedEventArgs e)
        {
            Menu_Menu_OpenGame.IsEnabled = false;
            Menu_Menu_CloseGame.IsEnabled = false;
            Menu_Menu_Connect.IsEnabled = false;
            Menu_Menu_Disconnect.IsEnabled = true;
        }
        private void Menu_Menu_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            Menu_Menu_OpenGame.IsEnabled = true;
            Menu_Menu_CloseGame.IsEnabled = false;
            Menu_Menu_Connect.IsEnabled = true;
            Menu_Menu_Disconnect.IsEnabled = false;
        }
        #endregion
    }
}