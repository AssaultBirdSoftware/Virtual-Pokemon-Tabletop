using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AssaultBird2454.VPTU.Sentry;
using CommandLine;
using SharpRaven;
using SharpRaven.Data;
using Application = System.Windows.Application;

namespace AssaultBird2454.VPTU.SaveEditor
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool Debug = true;

        private RavenClient ravenClient;

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
                }

                try
                {
                    MainWindow window = new MainWindow();

                    Parser.Default.ParseArguments<Options>(Environment.GetCommandLineArgs()).WithParsed<Options>(new Action<Options>((options) =>
                    {
                        #region OpenFile
                        if (options.OpenFile != null && options.OpenFile != "")
                        {
                            if (File.Exists(options.OpenFile))
                            {
                                try
                                {
                                    window.Load(options.OpenFile);
                                }
                                catch (Exception e)
                                {

                                }
                            }
                            else
                            {
                                /* File does not exist */
                            }
                        }
                        #endregion
                    }));

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
                    .GetManifestResourceStream("AssaultBird2454.VPTU.SaveEditor.sentry_cid.txt"))
                {
                    using (var read = new StreamReader(str))
                    {
                        return read.ReadToEnd();
                    }
                }
            }
        }
    }

    class Options
    {
        [Option('o', "open", Required = false, HelpText = "Opens a file at startup", Default = "")]
        public string OpenFile { get; set; }
    }
}