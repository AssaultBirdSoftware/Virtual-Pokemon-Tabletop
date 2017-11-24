using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public class Program
    {
        public static VPTU.Server.Instances.ServerInstance Instance;
        public static Server_UI ServerInterface;
        public static Thread ServerInterface_Thread;

        static void Main(string[] args)
        {
            ServerInterface_Thread = new Thread(new ThreadStart(() =>
            {
                ServerInterface = new Server_UI();
                ServerInterface.ShowDialog();
            }));
            ServerInterface_Thread.ApartmentState = ApartmentState.STA;
            ServerInterface_Thread.Start();
            ServerInterface_Thread.Join();
        }

        private static void ServerInterface_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
