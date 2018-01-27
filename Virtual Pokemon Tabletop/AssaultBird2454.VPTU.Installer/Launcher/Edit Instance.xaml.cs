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

namespace AssaultBird2454.VPTU.Installer.Launcher
{
    /// <summary>
    /// Interaction logic for Edit_Instance.xaml
    /// </summary>
    public partial class Edit_Instance : Window
    {
        public Data.InstalledVersion Instance { get; set; }
        private List<Data.VersionName> Versions { get; set; }

        public Edit_Instance(Data.InstalledVersion _Instance = null)
        {
            InitializeComponent();

            Load_Versions();

            AutoUpdate_ReleaseStream.ItemsSource = Enum.GetValues(typeof(Data.Stream));
            AutoUpdate_ReleaseStream.SelectedIndex = 0;
            AutoUpdate_Mode.ItemsSource = Enum.GetValues(typeof(Data.Mode));
            AutoUpdate_Mode.SelectedIndex = 0;
            Install_Dir.Text = App.Path_VersionsDir + Instance_Name.Text;

            if (_Instance == null)
            {
                Instance = new Data.InstalledVersion();
            }
            else
            {
                Instance = _Instance;
                Load();
            }
        }

        private void Load_Versions()
        {
            List<Data.VersionName> _Versions = new List<Data.VersionName>();
            if (Show_Alpha.IsChecked == true)
                try
                {
                    _Versions.AddRange(UpdaterAPI.UpdaterAPI.Version_List_Alpha);
                }
                catch { }
            if (Show_Beta.IsChecked == true)
                try
                {
                    _Versions.AddRange(UpdaterAPI.UpdaterAPI.Version_List_Beta);
                }
                catch { }
            if (Show_Master.IsChecked == true)
                try
                {
                    _Versions.AddRange(UpdaterAPI.UpdaterAPI.Version_List_Master);
                }
                catch { }

            Versions = _Versions.OrderByDescending(o => o.Build_ID).ToList();

            Instance_Version.ItemsSource = Versions;
            Instance_Version.SelectedIndex = 0;
        }

        public void Load()
        {
            Instance_Name.Text = Instance.Name;
            Instance_Version.SelectedItem = Versions.Find(x => x.Build_ID == Instance.Build_ID);
            AutoUpdate_Enabled.IsChecked = Instance.AutoUpdating_Enabled;

            if (Instance.AutoUpdating_Enabled)
            {
                AutoUpdate_ReleaseStream.SelectedItem = Instance.AutoUpdating_Config.ReleaseStream;
                AutoUpdate_Mode.SelectedItem = Instance.AutoUpdating_Config.Update_Mode;
                AutoUpdate_Backup.IsChecked = Instance.AutoUpdating_Config.Update_Actions.Contains(Data.UpdateAction.Backup);
                AutoUpdate_Duplicate.IsChecked = Instance.AutoUpdating_Config.Update_Actions.Contains(Data.UpdateAction.Duplicate);
            }
        }
        public void Save()
        {
            Instance.Name = Instance_Name.Text;
            Instance.Build_ID = ((Data.VersionName)Instance_Version.SelectedItem).Build_ID;
            Instance.Version_Name = ((Data.VersionName)Instance_Version.SelectedItem).Version_Name;
            Instance.AutoUpdating_Enabled = (bool)AutoUpdate_Enabled.IsChecked;

            if (Instance.AutoUpdating_Enabled)
            {
                Instance.AutoUpdating_Config.ReleaseStream = (Data.Stream)AutoUpdate_ReleaseStream.SelectedItem;
                Instance.AutoUpdating_Config.Update_Mode = (Data.Mode)AutoUpdate_Mode.SelectedItem;

                Instance.AutoUpdating_Config.Update_Actions.Clear();
                if (AutoUpdate_Backup.IsChecked == true)
                    Instance.AutoUpdating_Config.Update_Actions.Add(Data.UpdateAction.Backup);
                if (AutoUpdate_Duplicate.IsChecked == true)
                    Instance.AutoUpdating_Config.Update_Actions.Add(Data.UpdateAction.Duplicate);
            }
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
            DialogResult = true;
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Instance_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Install_Dir.Text = App.Path_VersionsDir + Instance_Name.Text;
        }
    }
}
