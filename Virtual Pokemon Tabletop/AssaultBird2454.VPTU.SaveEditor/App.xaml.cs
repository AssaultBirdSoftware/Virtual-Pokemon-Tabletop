using SharpRaven;
using SharpRaven.Data;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace AssaultBird2454.VPTU.SaveEditor
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
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.SaveEditor.sentry_cid.txt"))
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
                }
                catch { Debug = false; }

                try
                {
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
                catch (Exception ex)
                {
                    if (Debug)
                    {
                        ravenClient.Capture(new SentryEvent(ex));
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
