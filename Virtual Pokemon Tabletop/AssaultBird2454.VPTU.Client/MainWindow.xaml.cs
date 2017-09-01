using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        private void Menu_Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Main_Closed(object sender, EventArgs e)
        {
            File.Delete(AssemblyDirectory + "\\Client.pid");
        }
    }
}