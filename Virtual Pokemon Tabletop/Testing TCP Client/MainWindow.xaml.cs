using System;
using System.Net;
using System.Windows;
using AssaultBird2454.VPTU.Networking.Client.Command_Handeler;
using AssaultBird2454.VPTU.Networking.Client.TCP;
using AssaultBird2454.VPTU.Networking.Data;

namespace Testing_TCP_Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Client_CommandHandeler cmdhand;
        private TCP_Client TCP_Client;

        public MainWindow()
        {
            InitializeComponent();
            cmdhand = new Client_CommandHandeler();

            cmdhand.RegisterCommand<MessageData>("Message", MessageRecv);
        }

        private void MessageRecv(object Data)
        {
            console.Dispatcher.Invoke(() => console.AppendText("\n[" + DateTime.Now.ToShortTimeString() + "] -> " +
                                                               ((MessageData) Data).Message));
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TCP_Client.Disconnect();
                TCP_Client = null;
            }
            catch
            {
            }

            try
            {
                TCP_Client = new TCP_Client(IPAddress.Parse(Address.Text), cmdhand);
                TCP_Client.ConnectionStateEvent += TCP_Client_ConnectionStateEvent;

                TCP_Client.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connect\n\n" + ex);
            }
        }

        private void TCP_Client_ConnectionStateEvent(Client_ConnectionStatus ConnectionState)
        {
            State.Dispatcher.Invoke(new Action(() => State.Content = ConnectionState.ToString()));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.SendData(new MessageData(Data.Text));
        }

        private void Send10_Click(object sender, RoutedEventArgs e)
        {
            var DataObj = new MessageData(Data.Text);

            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
            TCP_Client.SendData(DataObj);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.Disconnect();
        }

        private void EnableSSL_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.Enable_SSL();
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