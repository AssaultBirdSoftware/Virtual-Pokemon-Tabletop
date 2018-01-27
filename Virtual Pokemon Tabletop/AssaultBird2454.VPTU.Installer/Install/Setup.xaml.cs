using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Installer.Install
{
    /// <summary>
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        public Setup()
        {
            InitializeComponent();

            Install_Directory.Text = App.Path_InstallDir;
            License.Text = new WebClient().DownloadString("https://raw.githubusercontent.com/AssaultBird2454/Virtual-Pokemon-Tabletop/Alpha/LICENSE");
        }

        private void Install_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Hide();

            Installer.Configure config = new Installer.Configure();
            config.ShowDialog();

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
