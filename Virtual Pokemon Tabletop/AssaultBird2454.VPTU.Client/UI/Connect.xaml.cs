using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.Authentication_Manager.Data;
using AssaultBird2454.VPTU.Server.Instances;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Auth;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Connection;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for Connect.xaml
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
            User_Identity.Items.Add(new ComboBoxItem
            {
                Content = "Guest (No Identity)",
                Tag = null
            });
            User_Identity.SelectedIndex = 0;

            foreach (var id in Program.Identities)
                User_Identity.Items.Add(new ComboBoxItem
                {
                    Content = id.ICN,
                    Tag = id
                });
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            Button_Connect.IsEnabled = false;
            try
            {
                Program.ClientInstance = new ClientInstance(Program.MainWindow.ClientConsole_Form(),
                    IPAddress.Parse(Server_Address.Text), (int) Server_Port.Value);
                Program.Setup_Client();
                var thread = new Thread(() =>
                {
                    Program.ClientInstance.StartClientInstance();
                    Program.ClientInstance.Client.SendData(new Server.Instances.CommandData.Connection.Connect
                    {
                        Connection_State = ConnectionStatus.OK,
                        Version = Program.VersioningInfo.Version,
                        Commit = Program.VersioningInfo.Compile_Commit
                    });

                    User_Identity.Dispatcher.Invoke(() =>
                    {
                        if (User_Identity.SelectedIndex >= 1)
                            Program.ClientInstance.Client.SendData(new Login
                            {
                                Client_Key = ((ClientIdentity) ((ComboBoxItem) User_Identity.SelectedItem).Tag).AuthKey
                            });
                    });
                });
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
                var Data = (ClientIdentity) ((ComboBoxItem) User_Identity.SelectedItem).Tag;

                Server_Address.IsEnabled = false;
                Server_Address.Text = Data.Server_Address;
                Server_Port.IsEnabled = false;
                Server_Port.Value = Data.Server_Port;
            }
        }

        private void User_ManageIdentities_Click(object sender, RoutedEventArgs e)
        {
            var ID = new Manage_Identities();

            ID.ShowDialog();

            Load_IDs();
        }
    }
}