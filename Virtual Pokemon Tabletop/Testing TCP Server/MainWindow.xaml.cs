using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using AssaultBird2454.VPTU.Networking.Data;
using AssaultBird2454.VPTU.Networking.Server.Command_Handeler;
using AssaultBird2454.VPTU.Networking.Server.TCP;

namespace Testing_TCP_Server
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Server_CommandHandeler cmdhand;
        private TcpServer Server;

        public MainWindow()
        {
            InitializeComponent();
            cmdhand = new Server_CommandHandeler();

            cmdhand.RegisterCommand<MessageData>("Message", CMDHandel);
        }

        private ObservableCollection<TcpClientNode> ClientList { get; set; }

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

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientList = null;
                Server.Stop();
                Server = null;
            }
            catch
            {
            }
            try
            {
                ClientList = new ObservableCollection<TcpClientNode>();
                Clients.ItemsSource = ClientList;

                Server = new TcpServer(IPAddress.Any, cmdhand, 25444);
                try
                {
                    Server.SSL_Cert = X509Certificate.CreateFromCertFile(AssemblyDirectory + "/Cert.cer");
                }
                catch
                {
                    MessageBox.Show("Server Has No Certificate");
                }
                Server.MaxConnections = 10;

                Server.TCP_ClientState_Changed += Server_TCP_ClientState_Changed;
                Server.TCP_AcceptClients_Changed += s => AcceptConnection.IsChecked = s;
                Server.TCP_Data_Event += (s, c, d) =>
                {
                    Action act = () => MessageBox.Show("Data Recv\n\nClient ID: " + c.ID + "\nData Direction: " + d +
                                                       "\nData: " + s);
                    act.Invoke();
                };

                Server.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Start\n\n" + ex);
            }
        }

        private void CMDHandel(object Data, TcpClientNode Client)
        {
            Console.Dispatcher.Invoke(() => Console.AppendText("\n[" + DateTime.Now.ToShortTimeString() + "] -> " +
                                                               ((MessageData) Data).Message));
        }

        private void Server_TCP_ClientState_Changed(TcpClientNode Client, Client_ConnectionStatus Client_State)
        {
            if (Client_State == Client_ConnectionStatus.Connected)
                Dispatcher.Invoke(() => { ClientList.Add(Client); });
            else if (Client_State == Client_ConnectionStatus.Disconnected)
                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        ClientList.Remove(Client);
                    }
                    catch
                    {
                    }
                });
            else if (Client_State == Client_ConnectionStatus.Rejected)
                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        ClientList.Remove(Client);
                    }
                    catch
                    {
                    }
                });

            //Clients.Dispatcher.Invoke(new Action(() => Clients.Items.Refresh()));
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.Stop();
                Server = null;
            }
            catch
            {
            }
        }

        private void AcceptConn_Click(object sender, RoutedEventArgs e)
        {
            Server.AcceptClients = (bool) AcceptConnection.IsChecked;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Server.Client_SendData(new MessageData(Data.Text));
        }

        private void Send10_Click(object sender, RoutedEventArgs e)
        {
            var DataObj = new MessageData(Data.Text);

            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
            Server.Client_SendData(DataObj);
        }
    }

    public class MessageData : NetworkCommand
    {
        public string Message;

        public MessageData(string _Message)
        {
            Message = _Message;
        }

        public string Command => "Message";
    }
}