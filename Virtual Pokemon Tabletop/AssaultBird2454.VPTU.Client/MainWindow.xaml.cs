using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        private List<KeyValuePair<decimal, WPF.MDI.MdiChild>> _Species_List = new List<KeyValuePair<decimal, WPF.MDI.MdiChild>>();
        public WPF.MDI.MdiChild Species_List(decimal ID)
        {
            List<KeyValuePair<decimal, WPF.MDI.MdiChild>> val = _Species_List.FindAll(x => x.Key == ID);
            if (val.Count >= 1)
            {
                return val[0].Value;
            }
            else
            {
                UI.Pokemon_Species species = new UI.Pokemon_Species();

                WPF.MDI.MdiChild window = new WPF.MDI.MdiChild()
                {
                    Title = "Pokedex Entry - [Name]",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokedex.png", UriKind.Absolute)),
                    Content = species,
                    MaximizeBox = false,
                    Width = 359,
                    Height = 500,
                    Resizable = false
                };// Create the window
                window.Closing += PokedexSpecies_Window_Closing;// Set up an event
                MDI.Children.Add(window);// Add the window

                _Species_List.Add(new KeyValuePair<decimal, WPF.MDI.MdiChild>(ID, window));
                return window;
            }
        }

        private void PokedexSpecies_Window_Closing(object sender, RoutedEventArgs e)
        {
            _Species_List.Remove(_Species_List.Find(x => x.Value == (WPF.MDI.MdiChild)sender));
        }
        #endregion
        #region Connect
        /// <summary>
        /// The control that handels the Connect List Functions
        /// </summary>
        private UI.Connect _Connect_Form;

        public UI.Connect Connect_Form()
        {
            if (_Connect_Form == null)
            {
                _Connect_Form = new UI.Connect();
                _Connect_Form.Closing += _Connect_Form_Closing;
                return _Connect_Form;
            }
            return _Connect_Form;
        }

        private void _Connect_Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Menu_Menu_Connect.IsChecked = false;
            _Connect_Form = null;
        }
        #endregion

        #region Entities List
        /// <summary>
        /// The control that handels the EntitiesList List Functions
        /// </summary>
        private UI.Entities.EntitiesList _EntitiesList_Form;
        /// <summary>
        /// The MDI window that handels the EntitiesList List Functions
        /// </summary>
        private WPF.MDI.MdiChild _EntitiesList_Window;
        /// <summary>
        /// Gets the control that handels the EntitiesList List Functions. And creates a window if it does not exist
        /// </summary>
        public UI.Entities.EntitiesList EntitiesList_Form()
        {
            if (_EntitiesList_Form == null)// If the list control does not exist
            {
                _EntitiesList_Form = new UI.Entities.EntitiesList();// Create the control

                Menu_View_EntitiesList.IsChecked = true;// Check the menu box
                _EntitiesList_Window = new WPF.MDI.MdiChild()
                {
                    Title = "Entities",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Pokeball.png", UriKind.Absolute)),
                    Content = _EntitiesList_Form
                };// Create the window
                _EntitiesList_Window.Closing += EntitiesList_Window_Closing;// Set up an event
                MDI.Children.Add(_EntitiesList_Window);// Add the window

                return _EntitiesList_Form;// Return the control
            }
            else
            {
                Menu_View_EntitiesList.IsChecked = true;// Check the menu box
                return _EntitiesList_Form;// Return the control if it already exists
            }
        }

        private void EntitiesList_Window_Closing(object sender, RoutedEventArgs e)
        {
            Menu_View_EntitiesList.IsChecked = false;
            _EntitiesList_Form = null;
            _EntitiesList_Window = null;
        }

        private List<KeyValuePair<string, WPF.MDI.MdiChild>> _CharacterSheet_List = new List<KeyValuePair<string, WPF.MDI.MdiChild>>();
        public WPF.MDI.MdiChild CharacterSheet_List(string ID)
        {
            List<KeyValuePair<string, WPF.MDI.MdiChild>> val = _CharacterSheet_List.FindAll(x => x.Key == ID);
            if (val.Count >= 1)
            {
                return val[0].Value;
            }
            else
            {
                UI.Entities.Entities CharacterSheet = new UI.Entities.Entities();

                WPF.MDI.MdiChild window = new WPF.MDI.MdiChild()
                {
                    Title = "Character Sheet - [Name]",
                    Icon = new BitmapImage(new Uri(Program.AssemblyDirectory + @"\Resources\Unknown_Pokemon_Sprite.png", UriKind.Absolute)),
                    Content = CharacterSheet,
                    MaximizeBox = false,
                    Height = 420,
                    Width = 722,
                    Resizable = false
                };// Create the window
                window.Closing += PokedexCharacterSheet_Window_Closing;// Set up an event
                MDI.Children.Add(window);// Add the window

                _CharacterSheet_List.Add(new KeyValuePair<string, WPF.MDI.MdiChild>(ID, window));
                return window;
            }
        }

        private void PokedexCharacterSheet_Window_Closing(object sender, RoutedEventArgs e)
        {
            _CharacterSheet_List.Remove(_CharacterSheet_List.Find(x => x.Value == (WPF.MDI.MdiChild)sender));
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
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                Process.Start("https://goo.gl/Y35Z8p");
                Thread.Sleep(5000);
                MessageBox.Show("April Fools!\n\nApril 1st Joke from the Dev :P", "Troll");
            }

            InitializeComponent();
            Program.MainWindow = this;
            Program.Settings_Load();
            Title = "Virtual Pokemon Tabletop - Client (Version: " + Program.VersioningInfo.Version + ") (Commit: " + Program.VersioningInfo.Compile_Commit.Remove(7) + ")";
        }

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
            MessageBoxResult mbr = MessageBox.Show("Would you like to open a web browser to view the forums?\n\nWeb URL: http://forums.pokemontabletop.com", "Open Forums?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                Process.Start("http://forums.pokemontabletop.com");
            }
        }

        private void Menu_Help_PTUSystem_105_Pokedex_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Pokedex 1.05.pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 Pokedex document?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_I_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 1 (Introduction).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 1 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_CC_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 2 (Character Creation).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 2 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_SEaF_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 3 (Skills, Edges And Features).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 3 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_TC_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 4 (Trainer Classes).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 4 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_P_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 5 (Pokemon).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 5 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_PtG_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 6 (Playing the Game).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 6 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_C_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 7 (Combat).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 7 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_PC_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 8 (Pokemon Contests).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 8 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_GaI_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 9 (Gear and Items).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 9 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_IaR_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 10 (Indices and Reference).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 10 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
        }
        private void Menu_Help_PTUSystem_105_RtG_Click(object sender, RoutedEventArgs e)
        {
            string dir = Program.AssemblyDirectory + @"\Docs\PTU System 1.05\Chapter 11 (Running the Game).pdf";
            MessageBoxResult mbr = MessageBox.Show("Would you like to open the PTU 1.05 core document (Chapter 11 Only)?\n\nFile URI: " + dir, "Open Document?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(dir);
            }
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
        #region Auth
        internal void Auth_Login_Executed(object Data)
        {
            Server.Instances.CommandData.Auth.Login loginData = (Server.Instances.CommandData.Auth.Login)Data;

            if (loginData.Auth_State == Server.Instances.CommandData.Auth.AuthState.Passed)
            {
                Status_Set_PlayerName(loginData.UserData.IC_Name);
            }
            else if (loginData.Auth_State != Server.Instances.CommandData.Auth.AuthState.Passed)
            {
                Status_Set_PlayerName("Not Authenticated");
            }
        }
        internal void Auth_Logout_Executed(object Data)
        {
            Server.Instances.CommandData.Auth.Login loginData = (Server.Instances.CommandData.Auth.Login)Data;

            if (loginData.Auth_State == Server.Instances.CommandData.Auth.AuthState.DeAuthenticated)
            {
                Status_Set_PlayerName("Not Authenticated");
            }
        }
        #endregion

        #region Pokedex
        internal void Pokedex_Pokemon_GetList_Executed(object Data)
        {
            PokedexList_Form().Pokedex_Pokemon_Get_Executed(((VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon_GetList)Data).Pokemon_Dex);
        }
        internal void Pokedex_Pokemon_Get_Executed(object Data)
        {
            VPTU.Pokedex.Pokemon.PokemonData pdata = (VPTU.Pokedex.Pokemon.PokemonData)((VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon)Data).PokemonData;

            this.Dispatcher.Invoke(new Action(() =>
            {
                WPF.MDI.MdiChild Window = Species_List(pdata.Species_DexID);

                Window.Title = "Pokedex Entry - " + pdata.Species_Name;
                ((UI.Pokemon_Species)(Window).Content).Update(pdata);
            }));
        }
        internal void Resources_Image_Get_Pokedex_Executed(object Data)
        {
            Server.Instances.CommandData.Resources.ImageResource IRD = (Server.Instances.CommandData.Resources.ImageResource)Data;

            if (IRD.UseCommand == "Pokedex_Species")// Pokedex Card Viewer
            {
                try
                {
                    decimal id = Decimal.Parse(IRD.UseID);
                    this.Dispatcher.Invoke(new Action(() => ((UI.Pokemon_Species)(Species_List(id)).Content).UpdateImage(IRD.Image)));
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Entities
        internal void Entities_All_GetList_Executed(object Data)
        {
            Server.Instances.CommandData.Entities.Entities_All_GetList EAGL = (Server.Instances.CommandData.Entities.Entities_All_GetList)Data;

            this.Dispatcher.Invoke(new Action(() => EntitiesList_Form().EntitiesManager_ReloadList(EAGL.Folders, EAGL.Entrys)));
        }
        internal void Entities_Pokemon_Get_Executed(object Data)
        {
            Server.Instances.CommandData.Entities.Entities_Pokemon_Get Pokemon = (Server.Instances.CommandData.Entities.Entities_Pokemon_Get)Data;

            this.Dispatcher.Invoke(new Action(() =>
            {
                WPF.MDI.MdiChild Window = CharacterSheet_List(Pokemon.ID);
                UI.Entities.Entities EntitiesForm = ((UI.Entities.Entities)(Window).Content);

                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Pokemon.Image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                Window.Icon = bitmapSource;
                Window.Title = "Character Sheet - " + Pokemon.Pokemon.Name;
                EntitiesForm.Update_Pokemon(Pokemon.Pokemon);
                EntitiesForm.Update_Token(Pokemon.Image);
            }));
        }
        internal void Resources_Image_Get_Entities_Executed(object Data)
        {
            Server.Instances.CommandData.Resources.ImageResource IRD = (Server.Instances.CommandData.Resources.ImageResource)Data;

            if (IRD.UseCommand == "Entities_List")// Pokedex Card Viewer
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() => EntitiesList_Form().UpdateImage(IRD.UseID, IRD.Image)));
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Resources

        #endregion

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.Settings_Save();
        }

        private void Menu_Menu_ManageID_Click(object sender, RoutedEventArgs e)
        {
            UI.Manage_Identities ID = new UI.Manage_Identities();

            ID.ShowDialog();
        }
    }
}
