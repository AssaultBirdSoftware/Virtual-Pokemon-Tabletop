
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

        static void Main(string[] args)
        {
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

            Logger.Write("Downloading Application Files", Console_LogLevel.Info);
            bool Download_Complete = false;
            WebClient client = new WebClient();
            client.DownloadFileCompleted += (s, e) =>
            {
                Download_Complete = true;
            };
            client.DownloadProgressChanged += (s, e) =>
            {
                Logger.Write("Downloading Application Files " + e.ProgressPercentage, Console_LogLevel.Info);
            };
            client.DownloadFileTaskAsync(LatestVersion.Download_URL, AssemblyDirectory + "\\Virtual Pokemon Tabletop.zip");

            while (!Download_Complete) { /* Wait for download */}

            //Logger.Write("Deleting Old Application Files", Console_LogLevel.Info);

            using (FileStream FileStream = new FileStream(AssemblyDirectory + "\\Virtual Pokemon Tabletop.zip", FileMode.Open))
            using (System.IO.Compression.ZipArchive Archive = new System.IO.Compression.ZipArchive(FileStream, ZipArchiveMode.Read))
            {
                Logger.Write("Deleting Application Files", Console_LogLevel.Info);
                foreach (ZipArchiveEntry entry in Archive.Entries)
                {
                    if (File.Exists(ParentDir + "\\" + entry.FullName))
                    {
                        Logger.Write("Deleting File: " + entry.FullName, Console_LogLevel.Info);
                        File.Delete(ParentDir + "\\" + entry.FullName);
                    }
                }

                Logger.Write("Extracting Application Files", Console_LogLevel.Info);
                Archive.ExtractToDirectory(ParentDir);
            }

            Logger.Write("Update / Installation Complete!", Console_LogLevel.Info);
            Logger.Write("Would you like to open the launcher? [Y/N]", Console_LogLevel.Info);

            #region Open Launcher
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Logger.Write("Opening Launcher & Quiting", Console_LogLevel.Info);
                    Process.Start(new ProcessStartInfo(ParentDir + "\\Launcher.exe", ""));
                    return;
                }
                else if (key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    Logger.Write("Closing", Console_LogLevel.Info);
                    Thread.Sleep(10000);
                    return;
                }
            }
            #endregion
        }

        public static Data LatestVersion { get; set; }
        public static bool CheckForUpdates()
        {
            try
            {
                string url = "http://vptu.assaultbirdsoftware.me/Updater/LatestVersion.json";
                LatestVersion = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>((new WebClient()).DownloadString(url));

                #region Get Version (Latest)
                int[] Version_Info = new int[4];
                int i = 0;
                foreach (string id in LatestVersion.Version_ID.Split('.'))
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
            public string Version_ID { get; set; }
            public ReleaseStream Version_Type { get; set; }
            public string Commit_ID { get; set; }
            public string Version_Name { get; set; }
            public string Download_URL { get; set; }
        }
    }
}
