using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher.Update
{
    internal class AutoUpdater
    {
        public static ProjectInfo VersioningInfo { get; set; }

        public static Data LatestVersion { get; set; }
        public static void CheckForUpdates()
        {
            #region Versioning Info
            using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("Launcher.ProjectVariables.json"))
            {
                using (StreamReader read = new StreamReader(str))
                {
                    VersioningInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                }
            }
            #endregion

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

                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(MainWindow.AssemblyDirectory + @"\Launcher.exe");

                bool update = false;
                if (FVI.ProductMajorPart > Version_Info[0])
                {
                    if (FVI.ProductMinorPart > Version_Info[1])
                    {
                        if (FVI.ProductBuildPart > Version_Info[2])
                        {
                            if (FVI.ProductPrivatePart > Version_Info[3])
                            {
                                //If -.-.-.^ is greater than current
                            }
                            else { update = true; }
                            //If -.-.^.x is greater than current
                        }
                        else { update = true; }
                        //If -.^.x.x is greater than current
                    }
                    else { update = true; }
                    //If ^.x.x.x is greater than current
                }
                else { update = true; }

                if (update)
                {
                    UpdateAvaliable();
                }
            }
            catch { /* Broken JSON, Dont Care */ }
        }

        private static void UpdateAvaliable()
        {
            MessageBoxResult mbr = MessageBox.Show("An update for Virtual Pokemon Tabletop was found...\n\nCurrent Version: " + VersioningInfo.Version + "\nLatest Version: " + LatestVersion.Version_Name, "Virtual Pokemon Tabletop Update", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            if(mbr == MessageBoxResult.Yes)
            {
                Directory.CreateDirectory(MainWindow.AssemblyDirectory + "/Updater/");
                WebClient wc = new WebClient();
                wc.DownloadFile("http://vptu.assaultbirdsoftware.me/Updater/UpdateApp/VPTU_Updater.zip", MainWindow.AssemblyDirectory + "/Updater/VPTU_Updater.zip");

                using (FileStream FileStream = new FileStream(MainWindow.AssemblyDirectory + "/Updater/VPTU_Updater.zip", FileMode.Open))
                using (System.IO.Compression.ZipArchive Archive = new System.IO.Compression.ZipArchive(FileStream, ZipArchiveMode.Read))
                {
                    Archive.ExtractToDirectory(MainWindow.AssemblyDirectory + "/Updater/");
                }
                Process.Start(MainWindow.AssemblyDirectory + "/Updater/Updater.exe", "");
                Process.GetCurrentProcess().Kill();
            }
            else if (mbr == MessageBoxResult.No)
            {
                // Nothing
            }
        }
    }
}
