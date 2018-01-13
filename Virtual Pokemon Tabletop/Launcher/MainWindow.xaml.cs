using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using Launcher.Notice;
using Launcher.Update;

namespace Launcher
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                if (File.Exists(AssemblyDirectory + "\\Launcher.pid"))
                    if (Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\Launcher.pid")))
                            .ProcessName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show("Process Already Running!");
                        Close();
                        return;
                    }
                    else
                    {
                        File.Delete(AssemblyDirectory + "\\Launcher.pid");
                    }

                File.WriteAllText(AssemblyDirectory + "\\Launcher.pid", Process.GetCurrentProcess().Id.ToString());
            }
            catch
            {
            }

            if (Directory.Exists(AssemblyDirectory + "\\Updater"))
            {
                foreach (var file in Directory.GetFiles(AssemblyDirectory + "\\Updater"))
                    File.Delete(file);
                Directory.Delete(AssemblyDirectory + "\\Updater"); // Delete the Update Folder
            }

            InitializeComponent();

            try
            {
                if (File.Exists(ConfigFile_Directory))
                {
                    VPTU_Settings =
                        Newtonsoft.Json.JsonConvert
                            .DeserializeObject<Settings
                            >(File.ReadAllText(ConfigFile_Directory)); // Loads the settings file
                }
                else
                {
                    VPTU_Settings = new Settings(); // Creates a settings class

                    var fs = new FileStream(ConfigFile_Directory, FileMode.OpenOrCreate);
                    var wr = new StreamWriter(fs);

                    string str = Newtonsoft.Json.JsonConvert.SerializeObject(VPTU_Settings);
                    wr.WriteLine(str);
                    wr.Flush();
                    wr.Dispose();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
            }

            AutoUpdater.CheckForUpdates(VPTU_Settings.Updater_Streams); // Check for updates

            //NoticeHandel = new Notice.MessageHandeler();// Creates a Notice Class
            //NoticeHandel.GetMessages(this);// Gets the notices and loads them
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        ///     Used to open the settings
        /// </summary>
        /// <param name="sender">Object that sent the request to open the settings</param>
        /// <param name="e">args</param>
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            var Settings = new SettingsWindow();
            Settings.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete(AssemblyDirectory + "\\Launcher.pid");
        }

        #region Variables

        /// <summary>
        ///     Settings Class
        /// </summary>
        private readonly Settings VPTU_Settings;

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

        /// <summary>
        ///     Config Path
        /// </summary>
        public static string ConfigFile_Directory => AssemblyDirectory + "/Settings.json";

        /// <summary>
        ///     Client Path
        /// </summary>
        public static string ClientExecutible_Path => AssemblyDirectory + "/Client.exe";

        /// <summary>
        ///     Server Path
        /// </summary>
        public static string ServerExecutible_Path => AssemblyDirectory + "/Server.exe";

        /// <summary>
        ///     Save Editor Path
        /// </summary>
        public static string SaveEditExecutible_Path => AssemblyDirectory + "/SaveEditor.exe";

        internal MessageHandeler NoticeHandel { get; set; }

        #endregion

        #region Settings

        /// <summary>
        ///     Loads a config file from the root of the executing directory
        /// </summary>
        private void Load_Settings()
        {
            using (var stream = new FileStream(ConfigFile_Directory, FileMode.Open))
            {
                //string configjson =
            }
        }

        /// <summary>
        ///     Loads a config file from the root of the executing directory
        /// </summary>
        private void Save_Settings()
        {
        }

        #endregion

        #region Start Application Buttons

        private Process ClientProcess;

        private void LaunchClient_Button_Click(object sender, RoutedEventArgs e)
        {
            ClientProcess = new Process();
            ClientProcess.StartInfo = new ProcessStartInfo(ClientExecutible_Path);
            ClientProcess.StartInfo.UseShellExecute = false;
            ClientProcess.Start();
        }

        private Process SaveEditProcess;

        private void LaunchSaveEditor_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveEditProcess = new Process();
            SaveEditProcess.StartInfo = new ProcessStartInfo(SaveEditExecutible_Path);
            SaveEditProcess.StartInfo.UseShellExecute = false;
            SaveEditProcess.Start();
        }

        #endregion
    }
}