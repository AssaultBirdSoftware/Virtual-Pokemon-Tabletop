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

        public MainWindow()
        {
            InitializeComponent();
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
                /*TCP_Client = new AssaultBird2454.VPTU.Networking.Client.TCP.TCP_Client(IPAddress.Parse(Address.Text), new Action<string>(new Action<string>((Data) =>
                {
                    console.Dispatcher.Invoke(new Action(() => console.AppendText("\n[" + DateTime.Now.ToShortTimeString() + "] -> " + Data)));
                })));

                TCP_Client.Connect();*/
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to Connect\n\n" + ex.ToString());
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.SendData(Data.Text);
        }
        private void Send10_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
            TCP_Client.SendData(Data.Text);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            TCP_Client.Disconnect();
        }
    }
}
