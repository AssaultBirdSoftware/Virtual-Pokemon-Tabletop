using SharpRaven;
using SharpRaven.Data;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string sentry_cid
        {
            get
            {
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("Launcher.sentry_cid.txt"))
                {
                    using (StreamReader read = new StreamReader(str))
                    {
                        return read.ReadToEnd();
                    }
                }
            }
        }

        private RavenClient ravenClient;
        private bool Debug = true;

        private Thread thread;

        public App()
        {
            thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    ravenClient = new RavenClient(sentry_cid);
                    ravenClient.Release = VersionInfo.VersioningInfo.Version + " (" + VersionInfo.VersioningInfo.Compile_Commit + ")";
                    Debug = false;
                }
                catch { Debug = true; }

                try
                {
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
                catch (Exception ex)
                {
                    if (!Debug)
                    {
                        AssaultBird2454.VPTU.Sentry.Crash_Form cf = new AssaultBird2454.VPTU.Sentry.Crash_Form();
                        System.Windows.Forms.DialogResult dr = cf.ShowDialog();

                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            SentryEvent se = new SentryEvent(ex);

                            se.Tags.Add(new System.Collections.Generic.KeyValuePair<string, string>("Discord Name", cf.ExtraData.DiscordName));

                            ravenClient.Capture(se);
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                return;
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Application.Current.Shutdown();
        }
    }
}
