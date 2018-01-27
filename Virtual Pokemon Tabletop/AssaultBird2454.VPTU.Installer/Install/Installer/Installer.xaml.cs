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
    /// Interaction logic for Installer.xaml
    /// </summary>
    public partial class Installer : Window
    {
        Data.InstallationConfig config;

        public Installer(Data.InstallationConfig _config)
        {
            InitializeComponent();
            config = _config;
        }

        public void Install()
        {
            Send("Starting VPTU Installer");
        }

        public void Send(string Message)
        {
            Console.AppendText(Environment.NewLine + Message);
            Console.ScrollToEnd();
        }

        public void Set_Primary_Task(string Task)
        {
            Progress_Main_Description.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Main_Description.Content = Task;
            }));
        }
        public void Set_Primary_Stage(int Current)
        {
            Progress_Main_Bar.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Main_Bar.Value = Current;
            }));
        }
        public void Set_Primary_Stage_Incrament()
        {
            Progress_Main_Bar.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Main_Bar.Value = Progress_Main_Bar.Value + 1;
            }));
        }
        public void Set_Primary_Max(int Max)
        {
            Progress_Main_Bar.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Main_Bar.Maximum = Max;
            }));
        }

        public void Set_Action_Task(string Task)
        {
            Progress_Action_Description.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Action_Description.Content = Task;
            }));
        }
        public void Set_Action_Stage(int Current)
        {
            Progress_Action_Bar.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Action_Bar.Value = Current;
            }));
        }
        public void Set_Action_Indeterminate(bool Enabled)
        {
            Progress_Action_Bar.IsIndeterminate = Enabled;
        }
        public void Set_Action_Max(int Max)
        {
            Progress_Action_Bar.Dispatcher.Invoke(new Action(() =>
            {
                Progress_Action_Bar.Maximum = Max;
            }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Install();
        }
    }
}
