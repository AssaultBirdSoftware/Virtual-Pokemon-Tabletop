using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.Client
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
                if (File.Exists(AssemblyDirectory + "\\Client.pid"))
                    if (Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\Launcher.pid")))
                            .ProcessName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show("Process Already Running!");
                        Close();
                        return;
                    }
                    else
                    {
                        File.Delete(AssemblyDirectory + "\\Client.pid");
                    }

                File.WriteAllText(AssemblyDirectory + "\\Client.pid", Process.GetCurrentProcess().Id.ToString());
            }
            catch
            {
            }

            InitializeComponent();

            #region Versioning Info

            using (var str = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("AssaultBird2454.VPTU.Client.ProjectVariables.json"))
            {
                using (var read = new StreamReader(str))
                {
                    VersioningInfo = JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    Title = "Virtual Pokemon Tabletop - Client (Version: " + VersioningInfo.Version + ") (Commit: " +
                            VersioningInfo.Compile_Commit.Remove(7) + ")";
                }
            }

            #endregion
        }

        public ProjectInfo VersioningInfo { get; }

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

        private void Menu_Menu_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Main_Closed(object sender, EventArgs e)
        {
            File.Delete(AssemblyDirectory + "\\Client.pid");
        }
    }
}