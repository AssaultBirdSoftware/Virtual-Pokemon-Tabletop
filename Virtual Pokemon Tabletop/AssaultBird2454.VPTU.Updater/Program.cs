
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                return System.IO.Path.GetDirectoryName(path);
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
                    break;
                }
                else if (key == ConsoleKey.N)
                {
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
                    break;
                }
                else if (key == ConsoleKey.N)
                {
                    break;
                }
                else
                {

                }
            }
            if (!Agree_Licence)
            {
                Logger.Write("Install Canceled: Canceled By User...", Console_LogLevel.Error);
                Logger.Write("Closing Updater...", Console_LogLevel.Info);
                Thread.Sleep(10000);
                return;
            }
            #endregion

            Logger.Write("Killing Running Processes... Please Wait...", Console_LogLevel.Info);
            List<Process> processes = new List<Process>();

            try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\SaveEditor.pid")))); } catch { }
            try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\Client.pid")))); } catch { }
            try { processes.Add(Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\Launcher.pid")))); } catch { }

            foreach (Process proc in processes)
            {
                Logger.Write("Killing Process: " + proc.ProcessName, Console_LogLevel.Info);
                proc.Kill();
            }

            Logger.Write("Removing Process Locks", Console_LogLevel.Info);
            File.Delete(AssemblyDirectory + "\\SaveEditor.pid");
            File.Delete(AssemblyDirectory + "\\Client.pid");
            File.Delete(AssemblyDirectory + "\\Launcher.pid");

            Logger.Write("Downloading Application Files", Console_LogLevel.Info);
            WebClient client = new WebClient();
            client.DownloadFileTaskAsync(LatestVersion.Download_URL, AssemblyDirectory + "\\Virtual Pokemon Tabletop.zip");

            //Logger.Write("Deleting Old Application Files", Console_LogLevel.Info);

            Logger.Write("Extracting & Overwriting Application Files", Console_LogLevel.Info);
            using (System.IO.Compression.DeflateStream deflate = new System.IO.Compression.DeflateStream(new FileStream(AssemblyDirectory + "\\Virtual Pokemon Tabletop.zip", FileMode.Open, FileAccess.Read), System.IO.Compression.CompressionMode.Decompress))
            {
                
            }

            // Done
            Console.ReadLine();
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

                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(AssemblyDirectory + @"\Launcher.exe");

                if (FVI.ProductMajorPart < Version_Info[0] || FVI.ProductMinorPart < Version_Info[1] || FVI.ProductBuildPart < Version_Info[2] || FVI.ProductPrivatePart < Version_Info[3])
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
