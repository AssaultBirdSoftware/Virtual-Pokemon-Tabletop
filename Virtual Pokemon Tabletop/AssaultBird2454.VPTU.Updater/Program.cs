using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Updater
{
    class Program
    {
        /// <summary>
        /// Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static string ParentDir
        {
            get
            {
                return Directory.GetParent(AssemblyDirectory).FullName;
            }
        }

        private static string sentry_cid
        {
            get
            {
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.Updater.sentry_cid.txt"))
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
                Debug = false;
            }
            catch { Debug = true; }

            try
            {
                #region Setup
                Logger.Write("Virtual Pokemon Tabletop Updater", Console_LogLevel.Info);

                #region Licence
                Logger.Write("This software is licenced under the MIT Licence. By installing and / or updating you are agreeing to this licence", Console_LogLevel.Notice);
                Logger.Write("Do you agree? [Y/N]", Console_LogLevel.Notice);
                bool Agree_Licence = false;
                while (true)
                {
                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Y)
                    {
                        Agree_Licence = true;
                        Console.WriteLine();
                        break;
                    }
                    else if (key == ConsoleKey.N)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else
                    {

                    }
                }
                if (!Agree_Licence)
                {
                    Logger.Write("Install Canceled: User failed to agree to licence!", Console_LogLevel.Error);
                    Logger.Write("Closing Updater...", Console_LogLevel.Info);
                    Thread.Sleep(10000);
                    return;
                }
                #endregion

                if (!CheckForUpdates())
                {
                    Logger.Write("No Updates Found!", Console_LogLevel.Info);
                    Logger.Write("Closing Updater...", Console_LogLevel.Info);
                    Thread.Sleep(10000);
                    return;
                }

                #region Close Processes
                Logger.Write("During the Updating / Installing process, all VPTU Instances will be terminated!", Console_LogLevel.Notice);
                Logger.Write("Kill All VPTU Processes and proceed? [Y/N]", Console_LogLevel.Notice);
                bool Agree_KillProcess = false;
                while (true)
                {
                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Y)
                    {
                        Agree_KillProcess = true;
                        Console.WriteLine();
                        break;
                    }
                    else if (key == ConsoleKey.N)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else
                    {

                    }
                }
                if (!Agree_KillProcess)
                {
                    Logger.Write("Install Canceled: Canceled By User...", Console_LogLevel.Error);
                    Logger.Write("Closing Updater...", Console_LogLevel.Info);
                    Thread.Sleep(10000);
                    return;
                }
                #endregion

                Logger.Write("Killing Running Processes... Please Wait...", Console_LogLevel.Info);
                List<Process> processes = new List<Process>();

                try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(ParentDir + "\\SaveEditor.pid")))); } catch { }
                try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(ParentDir + "\\Client.pid")))); } catch { }
                try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(ParentDir + "\\Launcher.pid")))); } catch { }

                foreach (Process proc in processes)
                {
                    Logger.Write("Killing Process: " + proc.ProcessName, Console_LogLevel.Info);
                    proc.Kill();

                    while (!proc.HasExited) { }
                }

                Logger.Write("Removing Process Locks", Console_LogLevel.Info);
                File.Delete(ParentDir + "\\SaveEditor.pid");
                File.Delete(ParentDir + "\\Client.pid");
                File.Delete(ParentDir + "\\Launcher.pid");
                #endregion

                Logger.Write("Downloading Application Files", Console_LogLevel.Info);
                DownloadFile(LatestVersion.Download_Bin, AssemblyDirectory + "..\\..\\Updater\\Virtual Pokemon Tabletop.zip");

                Logger.Write("Downloading Update Script", Console_LogLevel.Info);
                DownloadFile(LatestVersion.Download_Bin, AssemblyDirectory + "..\\..\\Updater\\Update.bat");

                foreach (KeyValuePair<string, string> file in LatestVersion.Update_Script_Files)
                {
                    Logger.Write("Downloading Update File " + file.Key, Console_LogLevel.Info);
                    DownloadFile(file.Value, AssemblyDirectory + "..\\..\\Updater\\" + file.Key.Replace('-', '.'));
                }
            }
            catch (Exception ex)
            {
                if (!Debug)
                {
                    Crash_Form cf = new Crash_Form();
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
        }

        private static void DownloadFile(string Src, string Dst)
        {
            bool Download_Complete = false;
            int Download_Row = Logger.Cursor_Top;
            WebClient client = new WebClient();
            client.DownloadFileCompleted += (s, e) =>
            {
                Logger.Cursor_Top = Download_Row + 1;
                Logger.Cursor_Left = 0;
                Download_Complete = true;
            };
            client.DownloadProgressChanged += (s, e) =>
            {
                char[] bar = new char[20] { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', };
                int progress = e.ProgressPercentage / 5;

                while (progress >= 1)
                {
                    bar[progress] = '=';
                }

                Logger.Write("[" + bar.ToString() + "] " + e.ProgressPercentage + "% (" + e.BytesReceived + "/" + e.TotalBytesToReceive + ")", Console_LogLevel.Info);
                Logger.Cursor_Top = Download_Row;
                Logger.Cursor_Left = 0;
            };
            client.DownloadFileTaskAsync(Src, Dst);

            while (!Download_Complete) { /* Wait for download */}
        }

        public static Data LatestVersion { get; set; }
        public static bool CheckForUpdates()
        {
            try
            {
                string url = Properties.Resources.Updater_Version_GetInfo.Replace("[ID]", (new WebClient().DownloadString(Properties.Resources.Updater_LatestID).Replace('"', ' ').Trim()));
                string VersionDataString = (new WebClient()).DownloadString(url);
                LatestVersion = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>(VersionDataString);

                #region Get Version (Latest)
                int[] Version_Info = new int[4];
                int i = 0;
                foreach (string id in LatestVersion.Version_String.Split('.'))
                {
                    Version_Info[i] = Convert.ToInt32(id);
                    i++;
                }
                #endregion

                if (!File.Exists(ParentDir + @"\Launcher.exe")) { return true; }

                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(ParentDir + @"\Launcher.exe");

                if (FVI.ProductMajorPart < Version_Info[0] ||
                    (FVI.ProductMajorPart <= Version_Info[0] && FVI.ProductMinorPart < Version_Info[1]) ||
                    (FVI.ProductMajorPart <= Version_Info[0] && FVI.ProductMinorPart <= Version_Info[1] && FVI.ProductBuildPart < Version_Info[2]) ||
                    (FVI.ProductMajorPart <= Version_Info[0] && FVI.ProductMinorPart <= Version_Info[1] && FVI.ProductBuildPart <= Version_Info[2] && FVI.ProductPrivatePart < Version_Info[3]))
                {
                    //If _.x.x.x is greater than current
                    //If x._.x.x is greater than current
                    //If x.x._.x is greater than current
                    //If x.x.x._ is greater than current

                    //Update Avaliable
                    return true;
                }
                else { return false; }
            }
            catch { /* Broken JSON, Dont Care */ return false; }
        }

        public enum ReleaseStream { Alpha, Beta, Master }
        internal class Data
        {
            public string Version_String { get; set; }
            public string Version_Name { get; set; }
            public string Commit_ID { get; set; }
            public bool Commit_Verified { get; set; }
            public ReleaseStream Version_Type { get; set; }
            public string Download_Bin { get; set; }
            public string Update_Script { get; set; }
            public List<KeyValuePair<string, string>> Update_Script_Files { get; set; }
            public string Release_Page { get; set; }
        }
    }
}
