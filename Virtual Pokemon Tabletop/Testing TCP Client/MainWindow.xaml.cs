using AssaultBird2454.VPTU.Networking.Client.Command_Handeler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Testing_TCP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AssaultBird2454.VPTU.Networking.Client.TCP.TCP_Client TCP_Client;
        Client_CommandHandeler cmdhand;

        public MainWindow()
        {
            InitializeComponent();
            cmdhand = new Client_CommandHandeler();

            cmdhand.RegisterCommand<MessageData>("Message", MessageRecv);
        }

        private void MessageRecv(object Data)
        {
            console.Dispatcher.Invoke(new Action(() => console.AppendText("\n[" + DateTime.Now.ToShortTimeString() + "] -> " + ((MessageData)Data).Message)));
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TCP_Client.Disconnect();
                TCP_Client = null;
            }
            catch { }

            try
            {
                TCP_Client = new AssaultBird2454.VPTU.Networking.Client.TCP.TCP_Client(IPAddress.Parse(Address.Text), cmdhand);
                TCP_Client.ConnectionStateEvent += TCP_Client_ConnectionStateEvent;

                TCP_Client.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connect\n\n" + ex.ToString());
            }
        }

        private void TCP_Client_ConnectionStateEvent(AssaultBird2454.VPTU.Networking.Data.Client_ConnectionStatus ConnectionState)
        {
            State.Dispatcher.Invoke(new Action(() =>State.Content = ConnectionState.ToString()));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

            TCP_Client.SendData(new MessageData(Data.Text));
        }
        private void Send10_Click(object sender, RoutedEventArgs e)
        {
            MessageData DataObj = new MessageData(Data.Text);

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
    }

    public class MessageData : AssaultBird2454.VPTU.Networking.Data.NetworkCommand
    {
        public string Command
        {
            get
            {
                return "Message";
            }
        }

        public MessageData(string _Message)
        {
            Message = _Message;
        }

        public string Message;
    }
}
