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
                ravenClient.Release = VersionInfo.VersioningInfo.Version + " (" + VersionInfo.VersioningInfo.Compile_Commit + ")";
            }
            catch { Debug = false; }

            try
            {
                ServerInterface_Thread = new Thread(new ThreadStart(() =>
                {
                    ServerInterface = new Server_UI();
                    ServerInterface.ShowDialog();
                }));
                ServerInterface_Thread.SetApartmentState(ApartmentState.STA);
                ServerInterface_Thread.Start();
                ServerInterface_Thread.Join();
            }
            catch (Exception ex)
            {
                if (Debug)
                {
                    AssaultBird2454.VPTU.Sentry.Crash_Form cf = new AssaultBird2454.VPTU.Sentry.Crash_Form();
                    System.Windows.Forms.DialogResult dr = cf.ShowDialog();

                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        SentryEvent se = new SentryEvent(ex);

                        se.Tags.Add(new KeyValuePair<string, string>("Discord Name", cf.ExtraData.DiscordName));

                        ravenClient.Capture(se);
                    }
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
