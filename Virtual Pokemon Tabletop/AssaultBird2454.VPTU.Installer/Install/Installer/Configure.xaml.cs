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
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Installer.Install.Installer
{
    /// <summary>
    /// Interaction logic for Configure.xaml
    /// </summary>
    public partial class Configure : Window
    {
        public Data.InstallationConfig config = new Data.InstallationConfig();

        public Configure()
        {
            InitializeComponent();

            config.OfflineUpdates = new List<Data.OfflineUpdate>();
            config.Versions = new List<Data.InstalledVersion>();
        }

        public void Reload_List()
        {
            Instance_List.Items.Clear();

            foreach (Data.InstalledVersion inst in config.Versions)
            {
                Instance_List.Items.Add(inst);
            }
        }

        private void Add_Instance_Button_Click(object sender, RoutedEventArgs e)
        {
            Launcher.Edit_Instance EI = new Launcher.Edit_Instance();
            if (EI.ShowDialog() == true)
            {
                config.Versions.Add(EI.Instance);
                Instance_List.Items.Add(EI.Instance);
            }
        }

        private void Edit_Instance_Button_Click(object sender, RoutedEventArgs e)
        {
            Launcher.Edit_Instance EI = new Launcher.Edit_Instance((Data.InstalledVersion)Instance_List.SelectedItem);
            if (EI.ShowDialog() == true)
            {
                Reload_List();
            }
        }

        private void Install_Button_Click(object sender, RoutedEventArgs e)
        {
            Installer install = new Installer(config);
            if (install.ShowDialog() == true)
            {

            }
        }
    }
}
