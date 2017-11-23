using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Dependant Forms
        #region Pokedex
        /// <summary>
        /// The control that handels the Pokedex List Functions
        /// </summary>
        private UI.Pokedex _PokedexList_Form;
        /// <summary>
        /// The MDI window that handels the Pokedex List Functions
        /// </summary>
        private WPF.MDI.MdiChild _PokedexList_Window;
        /// <summary>
        /// Gets the control that handels the Pokedex List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Pokedex PokedexList_Form()
        {
            if (_PokedexList_Form == null)// If the list control does not exist
            {
                _PokedexList_Form = new UI.Pokedex();// Create the control

                Menu_View_Pokedex.Dispatcher.Invoke(new Action(() => Menu_View_Pokedex.IsChecked = true));// Check the menu box
                _PokedexList_Window = new WPF.MDI.MdiChild()
                {
                    Title = "Pokedex List",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokedex.png", UriKind.Absolute)),
                    Content = _PokedexList_Form
                };// Create the window
                _PokedexList_Window.Closing += PokedexList_Window_Closing;// Set up an event
                MDI.Children.Add(_PokedexList_Window);// Add the window

                return _PokedexList_Form;// Return the control
            }
            else
            {
                Menu_View_Pokedex.Dispatcher.Invoke(new Action(() => Menu_View_Pokedex.IsChecked = true));// Check the menu box
                return _PokedexList_Form;// Return the control if it already exists
            }
        }

        // On Pokedex_Form Window Closing
        private void PokedexList_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_Pokedex.IsChecked = false;
            _PokedexList_Form = null;
            _PokedexList_Window = null;
        }
        #endregion
        #region Connect
        /// <summary>
        /// The control that handels the Connect List Functions
        /// </summary>
        private UI.Connect _Connect_Form;
        /// <summary>
        /// The MDI window that handels the Connect List Functions
        /// </summary>
        private WPF.MDI.MdiChild _Connect_Window;
        /// <summary>
        /// Gets the control that handels the Connect List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Connect Connect_Form()
        {
            if (_Connect_Form == null)// If the list control does not exist
            {
                _Connect_Form = new UI.Connect();// Create the control

                Menu_Menu_Connect.IsChecked = true;// Check the menu box
                _Connect_Window = new WPF.MDI.MdiChild()
                {
                    Title = "Connect to Server",
                    //Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Connect.png", UriKind.Absolute)),
                    Content = _Connect_Form,
                    Resizable = false,
                    Width = 395,
                    Height = 165
                };// Create the window
                _Connect_Window.Closing += Connect_Window_Closing;// Set up an event
                MDI.Children.Add(_Connect_Window);// Add the window

                return _Connect_Form;// Return the control
            }
            else
            {
                Menu_Menu_Connect.IsChecked = true;// Check the menu box
                return _Connect_Form;// Return the control if it already exists
            }
        }

        private void Connect_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_Menu_Connect.IsChecked = false;
            _Connect_Form = null;
            _Connect_Window = null;
        }
        #endregion

        #region Loggers
        /// <summary>
        /// The control that handels the ServerConsole List Functions
        /// </summary>
        private UI.Console _ServerConsole_Form;
        /// <summary>
        /// The MDI window that handels the ServerConsole List Functions
        /// </summary>
        private WPF.MDI.MdiChild _ServerConsole_Window;
        /// <summary>
        /// Gets the control that handels the ServerConsole List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Console ServerConsole_Form()
        {
            if (_ServerConsole_Form == null)// If the list control does not exist
            {
                _ServerConsole_Form = new UI.Console(true);// Create the control

                Menu_View_ServerConsole.IsChecked = true;// Check the menu box
                _ServerConsole_Window = new WPF.MDI.MdiChild()
                {
                    Title = "Server Console",
                    //Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\ServerConsole.png", UriKind.Absolute)),
                    Content = _ServerConsole_Form,
                    CloseBox = false
                };// Create the window
                _ServerConsole_Window.Closing += ServerConsole_Window_Closing;// Set up an event
                //_ServerConsole_Window. _ServerConsole_Window.Visibility = Visibility.Visible;
                MDI.Children.Add(_ServerConsole_Window);// Add the window

                return _ServerConsole_Form;// Return the control
            }
            else
            {
                _ServerConsole_Window.Visibility = Visibility.Visible;
                Menu_View_ServerConsole.IsChecked = true;// Check the menu box
                return _ServerConsole_Form;// Return the control if it already exists
            }
        }

        /// <summary>
        /// The control that handels the ClientConsole List Functions
        /// </summary>
        private UI.Console _ClientConsole_Form;
        /// <summary>
        /// The MDI window that handels the ClientConsole List Functions
        /// </summary>
        private WPF.MDI.MdiChild _ClientConsole_Window;
        /// <summary>
        /// Gets the control that handels the ClientConsole List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Console ClientConsole_Form()
        {
            if (_ClientConsole_Form == null)// If the list control does not exist
            {
                _ClientConsole_Form = new UI.Console(true);// Create the control

                Menu_View_ClientConsole.IsChecked = true;// Check the menu box
                _ClientConsole_Window = new WPF.MDI.MdiChild()
                {
                    Title = "Client Console",
                    //Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\ClientConsole.png", UriKind.Absolute)),
                    Content = _ClientConsole_Form,
                    CloseBox = false
                };// Create the window
                _ClientConsole_Window.Closing += ClientConsole_Window_Closing;// Set up an event
                //_ClientConsole_Window. _ClientConsole_Window.Visibility = Visibility.Visible;
                MDI.Children.Add(_ClientConsole_Window);// Add the window

                return _ClientConsole_Form;// Return the control
            }
            else
            {
                _ClientConsole_Window.Visibility = Visibility.Visible;
                Menu_View_ClientConsole.IsChecked = true;// Check the menu box
                return _ClientConsole_Form;// Return the control if it already exists
            }
        }

        private void ServerConsole_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_ServerConsole.IsChecked = false;
            _ServerConsole_Form = null;
            _ServerConsole_Window = null;
        }
        private void ClientConsole_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_ClientConsole.IsChecked = false;
            _ClientConsole_Form = null;
            _ClientConsole_Window = null;
        }
        #endregion
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Program.MainWindow = this;
            Title = "Virtual Pokemon Tabletop - Client (Version: " + Program.VersioningInfo.Version + ") (Commit: " + Program.VersioningInfo.Compile_Commit.Remove(7) + ")";
        }

        #region Menu, Tool Bar & Status Bar
        #region Menu
        #region Menu
        // Connect to Table
        private void Menu_Menu_Connect_Click(object sender, RoutedEventArgs e)
        {
            Connect_Form();
        }
        #endregion
        #region View
        private void Menu_View_Pokedex_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_View_Pokedex_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #endregion

        #region Tools
        private void Tools_Pokedex_Click(object sender, RoutedEventArgs e)
        {
            PokedexList_Form();
        }
        #endregion

        #region Status
        #region Server

        #endregion
        #region Client
        internal void Status_Set_Color(Color color)
        {
            Status.Dispatcher.Invoke(() =>
            {
                Status.Background = new SolidColorBrush(color);
            });
        }
        /// <summary>
        /// Sets the StatusBar Address Item
        /// </summary>
        /// <param name="Address"></param>
        internal void Status_Set_Address(string Address)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Address != "" || Address != null)
                {
                    Status_SvAddress.Content = "Server Address: " + Address;
                }
                else
                {
                    Status_SvAddress.Content = "Server Address: None";
                }
            });
        }
        /// <summary>
        /// Sets the StatusBar Port Item
        /// </summary>
        /// <param name="Port"></param>
        internal void Status_Set_Port(int Port)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Port != 0)
                {
                    Status_SvPort.Content = "Server Port: " + Port;
                }
                else
                {
                    Status_SvPort.Content = "Server Port: 0";
                }
            });
        }
        /// <summary>
        /// Sets the StatusBar Ping Item
        /// </summary>
        /// <param name="Ping"></param>
        internal void Status_Set_Ping(int Ping)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Ping != 0)
                {
                    Status_Ping.Content = "Ping: " + Ping;
                }
                else
                {
                    Status_Ping.Content = "Ping: 0";
                }
            });
        }
        /// <summary>
        /// Sets the StatusBar PlayerName Item
        /// </summary>
        /// <param name="PlayerName"></param>
        internal void Status_Set_PlayerName(string PlayerName)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (PlayerName != "" || PlayerName != null)
                {
                    Status_Username.Content = "PlayerName: " + PlayerName;
                }
                else
                {
                    Status_Username.Content = "PlayerName: None";
                }
            });
        }
        /// <summary>
        /// Sets the StatusBar CampaignName Item
        /// </summary>
        /// <param name="CampaignName"></param>
        internal void Status_Set_CampaignName(string CampaignName)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (CampaignName != "" || CampaignName != null)
                {
                    Status_CampaignName.Content = "Campaign Name: " + CampaignName;
                }
                else
                {
                    Status_CampaignName.Content = "Campaign Name: None";
                }
            });
        }
        #endregion
        #endregion
        #endregion

        #region Command Handelers
        internal void Pokedex_Pokemon_Get_Executed(object Data)
        {
            PokedexList_Form().Pokedex_Pokemon_Get_Executed(((VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon_Get)Data).Pokemon_Dex);
        }
        #endregion
    }
}
