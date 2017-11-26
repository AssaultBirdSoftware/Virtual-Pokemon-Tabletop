using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private static string sentry_cid
        {
            get
            {
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.ServerConsole.sentry_cid.txt"))
                {
                    using (StreamReader read = new StreamReader(str))
                    {
                        return read.ReadToEnd();
                    }
                }
            }
        }

        private static RavenClient ravenClient;
        private static bool Debug = true;


        static void Main(string[] args)
        {
            try
            {
                ravenClient = new RavenClient(sentry_cid);
            }
            catch { Debug = false; }

            try
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
            catch (Exception ex)
            {
                if (Debug)
                {
                    ravenClient.Capture(new SentryEvent(ex));
                }
            }
            return;
        }

        private static void ServerInterface_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
