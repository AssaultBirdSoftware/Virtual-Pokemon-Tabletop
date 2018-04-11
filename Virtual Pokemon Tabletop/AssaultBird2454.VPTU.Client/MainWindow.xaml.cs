using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AssaultBird2454.VPTU.Client.UI;
using AssaultBird2454.VPTU.Client.UI.Entities;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Auth;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Entities;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Resources;
using WPF.MDI;
using Console = AssaultBird2454.VPTU.Client.UI.Console;
using SharpRaven.Data;

namespace AssaultBird2454.VPTU.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                Process.Start("https://goo.gl/Y35Z8p");
                App.ravenClient.Capture(new SentryEvent($"April_Fools{DateTime.Now.Year}"));
                Thread.Sleep(5000);
                MessageBox.Show("April Fools!\n\nApril 1st Joke from the Dev :P", "Troll");
            }

            InitializeComponent();
            Program.MainWindow = this;
            Program.Settings_Load();
            Title = "Virtual Pokemon Tabletop - Client (Version: " + Program.VersioningInfo.Version + ") (Commit: " +
                    Program.VersioningInfo.Compile_Commit.Remove(7) + ")";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Program.Settings_Save();
        }

        private void Menu_Menu_ManageID_Click(object sender, RoutedEventArgs e)
        {
            var ID = new Manage_Identities();

            ID.ShowDialog();
        }

        #region Dependant Forms

        #region Pokedex

        /// <summary>
        ///     The control that handels the Pokedex List Functions
        /// </summary>
        private UI.Pokedex _PokedexList_Form;

        /// <summary>
        ///     The MDI window that handels the Pokedex List Functions
        /// </summary>
        private MdiChild _PokedexList_Window;

        /// <summary>
        ///     Gets the control that handels the Pokedex List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Pokedex PokedexList_Form()
        {
            if (_PokedexList_Form == null) // If the list control does not exist
            {
                _PokedexList_Form = new UI.Pokedex(); // Create the control

                Menu_View_Pokedex.Dispatcher.Invoke(
                    new Action(() => Menu_View_Pokedex.IsChecked = true)); // Check the menu box
                _PokedexList_Window = new MdiChild
                {
                    Title = "Pokedex List",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokedex.png",
                        UriKind.Absolute)),
                    Content = _PokedexList_Form
                }; // Create the window
                _PokedexList_Window.Closing += PokedexList_Window_Closing; // Set up an event
                MDI.Children.Add(_PokedexList_Window); // Add the window

                return _PokedexList_Form; // Return the control
            }
            Menu_View_Pokedex.Dispatcher.Invoke(
                new Action(() => Menu_View_Pokedex.IsChecked = true)); // Check the menu box
            return _PokedexList_Form; // Return the control if it already exists
        }

        // On Pokedex_Form Window Closing
        private void PokedexList_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_Pokedex.IsChecked = false;
            _PokedexList_Form = null;
            _PokedexList_Window = null;
        }

        private readonly List<KeyValuePair<decimal, MdiChild>> _Species_List =
            new List<KeyValuePair<decimal, MdiChild>>();

        public MdiChild Species_List(decimal ID)
        {
            var val = _Species_List.FindAll(x => x.Key == ID);
            if (val.Count >= 1)
                return val[0].Value;
            var species = new Pokemon_Species();

            var window = new MdiChild
            {
                Title = "Pokedex Entry - [Name]",
                Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokedex.png",
                    UriKind.Absolute)),
                Content = species,
                MaximizeBox = false,
                Width = 359,
                Height = 500,
                Resizable = false
            }; // Create the window
            window.Closing += PokedexSpecies_Window_Closing; // Set up an event
            MDI.Children.Add(window); // Add the window

            _Species_List.Add(new KeyValuePair<decimal, MdiChild>(ID, window));
            return window;
        }

        private void PokedexSpecies_Window_Closing(object sender, RoutedEventArgs e)
        {
            _Species_List.Remove(_Species_List.Find(x => x.Value == (MdiChild)sender));
        }

        #endregion

        #region Connect

        /// <summary>
        ///     The control that handels the Connect List Functions
        /// </summary>
        private Connect _Connect_Form;

        public Connect Connect_Form()
        {
            if (_Connect_Form == null)
            {
                _Connect_Form = new Connect();
                _Connect_Form.Closing += _Connect_Form_Closing;
                return _Connect_Form;
            }
            return _Connect_Form;
        }

        private void _Connect_Form_Closing(object sender, CancelEventArgs e)
        {
            Menu_Menu_Connect.IsChecked = false;
            _Connect_Form = null;
        }

        #endregion

        #region Entities List

        /// <summary>
        ///     The control that handels the EntitiesList List Functions
        /// </summary>
        private EntitiesList _EntitiesList_Form;

        /// <summary>
        ///     The MDI window that handels the EntitiesList List Functions
        /// </summary>
        private MdiChild _EntitiesList_Window;

        /// <summary>
        ///     Gets the control that handels the EntitiesList List Functions. And creates a window if it does not exist
        /// </summary>
        public EntitiesList EntitiesList_Form()
        {
            if (_EntitiesList_Form == null) // If the list control does not exist
            {
                _EntitiesList_Form = new EntitiesList(); // Create the control

                Menu_View_EntitiesList.IsChecked = true; // Check the menu box
                _EntitiesList_Window = new MdiChild
                {
                    Title = "Entities",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokeball.png",
                        UriKind.Absolute)),
                    Content = _EntitiesList_Form
                }; // Create the window
                _EntitiesList_Window.Closing += EntitiesList_Window_Closing; // Set up an event
                MDI.Children.Add(_EntitiesList_Window); // Add the window

                return _EntitiesList_Form; // Return the control
            }
            Menu_View_EntitiesList.IsChecked = true; // Check the menu box
            return _EntitiesList_Form; // Return the control if it already exists
        }

        private void EntitiesList_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_EntitiesList.IsChecked = false;
            _EntitiesList_Form = null;
            _EntitiesList_Window = null;
        }

        private readonly List<KeyValuePair<string, MdiChild>> _CharacterSheet_List =
            new List<KeyValuePair<string, MdiChild>>();

        public MdiChild CharacterSheet_List(string ID)
        {
            var val = _CharacterSheet_List.FindAll(x => x.Key == ID);
            if (val.Count >= 1)
                return val[0].Value;
            var CharacterSheet = new Entities();

            var window = new MdiChild
            {
                Title = "Character Sheet - [Name]",
                Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Unknown_Pokemon_Sprite.png",
                    UriKind.Absolute)),
                Content = CharacterSheet,
                MaximizeBox = false,
                Height = 420,
                Width = 722,
                Resizable = false
            }; // Create the window
            window.Closing += PokedexCharacterSheet_Window_Closing; // Set up an event
            MDI.Children.Add(window); // Add the window

            _CharacterSheet_List.Add(new KeyValuePair<string, MdiChild>(ID, window));
            return window;
        }

        private void PokedexCharacterSheet_Window_Closing(object sender, RoutedEventArgs e)
        {
            _CharacterSheet_List.Remove(_CharacterSheet_List.Find(x => x.Value == (MdiChild)sender));
        }

        #endregion

        #region Loggers

        /// <summary>
        ///     The control that handels the ServerConsole List Functions
        /// </summary>
        private Console _ServerConsole_Form;

        /// <summary>
        ///     The MDI window that handels the ServerConsole List Functions
        /// </summary>
        private MdiChild _ServerConsole_Window;

        /// <summary>
        ///     Gets the control that handels the ServerConsole List Functions. And creates a window if it does not exist
        /// </summary>
        public Console ServerConsole_Form()
        {
            if (_ServerConsole_Form == null) // If the list control does not exist
            {
                _ServerConsole_Form = new Console(true); // Create the control

                Menu_View_ServerConsole.IsChecked = true; // Check the menu box
                _ServerConsole_Window = new MdiChild
                {
                    Title = "Server Console",
                    //Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\ServerConsole.png", UriKind.Absolute)),
                    Content = _ServerConsole_Form,
                    CloseBox = false
                }; // Create the window
                _ServerConsole_Window.Closing += ServerConsole_Window_Closing; // Set up an event
                //_ServerConsole_Window. _ServerConsole_Window.Visibility = Visibility.Visible;
                MDI.Children.Add(_ServerConsole_Window); // Add the window

                return _ServerConsole_Form; // Return the control
            }
            _ServerConsole_Window.Visibility = Visibility.Visible;
            Menu_View_ServerConsole.IsChecked = true; // Check the menu box
            return _ServerConsole_Form; // Return the control if it already exists
        }

        /// <summary>
        ///     The control that handels the ClientConsole List Functions
        /// </summary>
        private Console _ClientConsole_Form;

        /// <summary>
        ///     The MDI window that handels the ClientConsole List Functions
        /// </summary>
        private MdiChild _ClientConsole_Window;

        /// <summary>
        ///     Gets the control that handels the ClientConsole List Functions. And creates a window if it does not exist
        /// </summary>
        public Console ClientConsole_Form()
        {
            if (_ClientConsole_Form == null) // If the list control does not exist
            {
                _ClientConsole_Form = new Console(true); // Create the control

                Menu_View_ClientConsole.IsChecked = true; // Check the menu box
                _ClientConsole_Window = new MdiChild
                {
                    Title = "Client Console",
                    //Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\ClientConsole.png", UriKind.Absolute)),
                    Content = _ClientConsole_Form,
                    CloseBox = false
                }; // Create the window
                _ClientConsole_Window.Closing += ClientConsole_Window_Closing; // Set up an event
                //_ClientConsole_Window. _ClientConsole_Window.Visibility = Visibility.Visible;
                MDI.Children.Add(_ClientConsole_Window); // Add the window

                return _ClientConsole_Form; // Return the control
            }
            _ClientConsole_Window.Visibility = Visibility.Visible;
            Menu_View_ClientConsole.IsChecked = true; // Check the menu box
            return _ClientConsole_Form; // Return the control if it already exists
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

        #region Menu, Tool Bar & Status Bar

        #region Menu

        #region Menu

        // Connect to Table
        private void Menu_Menu_Connect_Click(object sender, RoutedEventArgs e)
        {
            Connect_Form().ShowDialog();
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

        #region Help

        private void Menu_Help_Forums_Click(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show(
                "Would you like to open a web browser to view the forums?\n\nWeb URL: http://forums.pokemontabletop.com",
                "Open Forums?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start("http://forums.pokemontabletop.com");
        }

        private void Menu_Help_PTUSystem_105_Pokedex_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Pokedex 1.05.pdf";
            var mbr = MessageBox.Show("Would you like to open the PTU 1.05 Pokedex document?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_I_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 1 (Introduction).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 1 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_CC_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 2 (Character Creation).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 2 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_SEaF_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 3 (Skills, Edges And Features).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 3 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_TC_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 4 (Trainer Classes).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 4 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_P_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 5 (Pokemon).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 5 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_PtG_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 6 (Playing the Game).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 6 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_C_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 7 (Combat).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 7 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_PC_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 8 (Pokemon Contests).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 8 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_GaI_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 9 (Gear and Items).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 9 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_IaR_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 10 (Indices and Reference).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 10 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        private void Menu_Help_PTUSystem_105_RtG_Click(object sender, RoutedEventArgs e)
        {
            var dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 11 (Running the Game).pdf";
            var mbr = MessageBox.Show(
                "Would you like to open the PTU 1.05 core document (Chapter 11 Only)?\n\nFile URI: " + dir,
                "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
                Process.Start(dir);
        }

        #endregion

        #endregion

        #region Tools

        private void Tools_Pokedex_Click(object sender, RoutedEventArgs e)
        {
            PokedexList_Form();
        }

        private void Tools_Entities_Click(object sender, RoutedEventArgs e)
        {
            EntitiesList_Form();
        }

        #endregion

        #region Status

        #region Server

        #endregion

        #region Client

        internal void Status_Set_Color(Color color)
        {
            Status.Dispatcher.Invoke(() => { Status.Background = new SolidColorBrush(color); });
        }

        /// <summary>
        ///     Sets the StatusBar Address Item
        /// </summary>
        /// <param name="Address"></param>
        internal void Status_Set_Address(string Address)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Address != "" || Address != null)
                    Status_SvAddress.Content = "Server Address: " + Address;
                else
                    Status_SvAddress.Content = "Server Address: None";
            });
        }

        /// <summary>
        ///     Sets the StatusBar Port Item
        /// </summary>
        /// <param name="Port"></param>
        internal void Status_Set_Port(int Port)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Port != 0)
                    Status_SvPort.Content = "Server Port: " + Port;
                else
                    Status_SvPort.Content = "Server Port: 0";
            });
        }

        /// <summary>
        ///     Sets the StatusBar Ping Item
        /// </summary>
        /// <param name="Ping"></param>
        internal void Status_Set_Ping(int Ping)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (Ping != 0)
                    Status_Ping.Content = "Ping: " + Ping;
                else
                    Status_Ping.Content = "Ping: 0";
            });
        }

        /// <summary>
        ///     Sets the StatusBar PlayerName Item
        /// </summary>
        /// <param name="PlayerName"></param>
        internal void Status_Set_PlayerName(string PlayerName)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (PlayerName != "" || PlayerName != null)
                    Status_Username.Content = "PlayerName: " + PlayerName;
                else
                    Status_Username.Content = "PlayerName: None";
            });
        }

        /// <summary>
        ///     Sets the StatusBar CampaignName Item
        /// </summary>
        /// <param name="CampaignName"></param>
        internal void Status_Set_CampaignName(string CampaignName)
        {
            Status.Dispatcher.Invoke(() =>
            {
                if (CampaignName != "" || CampaignName != null)
                    Status_CampaignName.Content = "Campaign Name: " + CampaignName;
                else
                    Status_CampaignName.Content = "Campaign Name: None";
            });
        }

        #endregion

        #endregion

        #endregion

        #region Command Handelers

        #region Auth

        internal Networking.Data.Response Auth_Login_Executed(object Data, bool Waiting)
        {
            var loginData = (Login)Data;

            if (loginData.Auth_State == AuthState.Authenticated && loginData.Response == Networking.Data.ResponseCode.OK)
                Status_Set_PlayerName(loginData.UserData.IC_Name);
            else if (loginData.Auth_State != AuthState.DeAuthenticated && loginData.Response == Networking.Data.ResponseCode.Failed)
                Status_Set_PlayerName("Not Authenticated");

            return new Networking.Data.Response() { Code = Networking.Data.ResponseCode.OK, Data = null, Message = "" };

        }

        internal void Auth_Logout_Executed(object Data)
        {
            var loginData = (Login)Data;

            if (loginData.Auth_State == AuthState.DeAuthenticated)
                Status_Set_PlayerName("Not Authenticated");
        }

        #endregion

        #region Pokedex

        internal Networking.Data.Response Pokedex_Pokemon_GetList_Executed(object Data, bool Waiting)
        {
            PokedexList_Form().Pokedex_Pokemon_Get_Executed(((Pokedex_Pokemon_GetList)Data).Pokemon_Dex);

            return new Networking.Data.Response() { Code = Networking.Data.ResponseCode.OK, Data = null, Message = "" };
        }

        internal Networking.Data.Response Pokedex_Pokemon_Get_Executed(object Data, bool Waiting)
        {
            var pdata = ((Pokedex_Pokemon)Data).PokemonData;

            Dispatcher.Invoke(() =>
            {
                var Window = Species_List(pdata.Species_DexID);

                Window.Title = "Pokedex Entry - " + pdata.Species_Name;
                ((Pokemon_Species)Window.Content).Update(pdata);
            });

            return new Networking.Data.Response() { Code = Networking.Data.ResponseCode.OK, Data = null, Message = "" };
        }
        #endregion

        #region Entities

        internal Networking.Data.Response Entities_All_GetList_Executed(object Data, bool Waiting)
        {
            var EAGL = (Entities_All_GetList)Data;

            Dispatcher.Invoke(() => EntitiesList_Form().EntitiesManager_ReloadList(EAGL.Folders, EAGL.Entrys, EAGL.UserList));

            return new Networking.Data.Response() { Code = Networking.Data.ResponseCode.OK, Data = null, Message = "" };
        }

        internal Networking.Data.Response Entities_Pokemon_Get_Executed(object Data, bool Waiting)
        {
            var Pokemon = (Entities_Pokemon_Get)Data;

            Dispatcher.Invoke(() =>
            {
                var Window = CharacterSheet_List(Pokemon.ID);
                var EntitiesForm = (Entities)Window.Content;

                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Pokemon.Image.GetHbitmap(), IntPtr.Zero,
                    Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                Window.Icon = bitmapSource;
                Window.Title = "Character Sheet - " + Pokemon.Pokemon.Name;
                EntitiesForm.Update_Pokemon(Pokemon.Pokemon);
                EntitiesForm.Update_Token(Pokemon.Image);
            });

            return new Networking.Data.Response() { Code = Networking.Data.ResponseCode.OK, Data = null, Message = "" };
        }
        #endregion

        #region Resources

        #endregion

        #endregion
    }
}
