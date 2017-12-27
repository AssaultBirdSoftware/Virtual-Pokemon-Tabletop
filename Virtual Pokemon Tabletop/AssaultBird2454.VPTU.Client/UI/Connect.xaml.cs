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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for Connect.xaml
    /// </summary>
    public partial class Connect : Window
    {
        public Connect()
        {
            InitializeComponent();

            Load_IDs();
        }

        public void Load_IDs()
        {
            User_Identity.Items.Clear();
            User_Identity.Items.Add(new ComboBoxItem()
            {
                Content = "Guest (No Identity)",
                Tag = null
            });
            User_Identity.SelectedIndex = 0;

            foreach (Authentication_Manager.Data.ClientIdentity id in Program.Identities)
            {
                User_Identity.Items.Add(new ComboBoxItem()
                {
                    Content = id.ICN,
                    Tag = id
                });
            }
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            Button_Connect.IsEnabled = false;
            try
            {
                Program.ClientInstance = new Server.Instances.ClientInstance(Program.MainWindow.ClientConsole_Form(), IPAddress.Parse(Server_Address.Text), (int)Server_Port.Value);
                Program.Setup_Client();
                Thread thread = new Thread(new ThreadStart(new Action(() =>
                {
                    Program.ClientInstance.StartClientInstance();
                    Program.ClientInstance.Client.SendData(new Server.Instances.CommandData.Connection.Connect() { Connection_State = Server.Instances.CommandData.Connection.ConnectionStatus.OK, Version = Program.VersioningInfo.Version, Commit = Program.VersioningInfo.Compile_Commit });

                    User_Identity.Dispatcher.Invoke(() =>
                    {
                        if (User_Identity.SelectedIndex >= 1)
                        {
                            Program.ClientInstance.Client.SendData(new Server.Instances.CommandData.Auth.Login() { Client_Key = ((Authentication_Manager.Data.ClientIdentity)((ComboBoxItem)User_Identity.SelectedItem).Tag).AuthKey });
                        }
                    });
                })));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (FormatException)
            {

            }
            Close();
        }

        private void User_Identity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (User_Identity.SelectedIndex == 0 || User_Identity.SelectedIndex == -1)
            {
                Server_Address.IsEnabled = true;
                Server_Address.Text = "";
                Server_Port.IsEnabled = true;
                Server_Port.Value = 25444;
            }
            else
            {
                Authentication_Manager.Data.ClientIdentity Data = ((Authentication_Manager.Data.ClientIdentity)((ComboBoxItem)User_Identity.SelectedItem).Tag);

                Server_Address.IsEnabled = false;
                Server_Address.Text = Data.Server_Address;
                Server_Port.IsEnabled = false;
                Server_Port.Value = Data.Server_Port;
            }
        }

        private void User_ManageIdentities_Click(object sender, RoutedEventArgs e)
        {
            UI.Manage_Identities ID = new UI.Manage_Identities();

            ID.ShowDialog();

            Load_IDs();
        }
    }
}
