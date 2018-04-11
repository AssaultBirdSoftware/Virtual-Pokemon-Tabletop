using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AssaultBird2454.VPTU.Sentry;
using SharpRaven;
using SharpRaven.Data;
using Application = System.Windows.Application;

namespace AssaultBird2454.VPTU.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {    
        private bool Debug = true;

        static internal RavenClient ravenClient;

        private readonly Thread thread;

        public App()
        {        
            thread = new Thread(() =>
            {
                try
                {
                    ravenClient = new RavenClient(sentry_cid);
                    ravenClient.Release = VersionInfo.VersioningInfo.Version + " (" +
                                          VersionInfo.VersioningInfo.Compile_Commit + ")";
                    Debug = false;
                }
                catch
                {
                    Debug = true;
                    MessageBox.Show("Client running in Debug Mode...\n\nNo Telemetry events will be sent");
                }

                try
                {
                    var window = new MainWindow();
                    window.ShowDialog();
                }
                catch (Exception ex)
                {
                    if (!Debug)
                    {
                        var cf = new Crash_Form();
                        var dr = cf.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            SentryEvent se = new SentryEvent(ex);

                            se.Tags.Add(new KeyValuePair<string, string>("Discord Name", cf.ExtraData.DiscordName));

                            ravenClient.Capture(se);
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Current.Shutdown();
        }

        private static string sentry_cid
        {
            get
            {
                using (var str = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("AssaultBird2454.VPTU.Client.sentry_cid.txt"))
                {
                    using (var read = new StreamReader(str))
                    {
                        return read.ReadToEnd();
                    }
                }
            }
        }
    }
}
