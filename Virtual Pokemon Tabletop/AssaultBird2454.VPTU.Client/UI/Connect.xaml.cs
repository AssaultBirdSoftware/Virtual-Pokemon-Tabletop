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
    public partial class Connect : UserControl
    {
        public Connect()
        {
            InitializeComponent();
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Program.ClientInstance = new Server.Instances.ClientInstance(Program.MainWindow.ClientConsole_Form(), IPAddress.Parse(Server_Address.Text), (int)Server_Port.Value);
                Program.Setup_Client();
                Thread thread = new Thread(new ThreadStart(new Action(() => Program.ClientInstance.StartClientInstance())));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (FormatException)
            {

            }
        }
    }
}
