using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Update
{
    internal class AutoUpdater
    {
        public Data LatestVersion { get; set; }

        public void CheckForUpdates()
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

                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(MainWindow.AssemblyDirectory + @"\" + Process.GetCurrentProcess().MainModule.FileName);

                if (FVI.ProductMajorPart > Version_Info[0] || FVI.ProductMinorPart > Version_Info[1] || FVI.ProductBuildPart > Version_Info[2] || FVI.ProductPrivatePart > Version_Info[3])
                {
                    //If _.x.x.x is greater than current
                    //If x._.x.x is greater than current
                    //If x.x._.x is greater than current
                    //If x.x.x._ is greater than current

                    //Update Avaliable
                }
            }
            catch { /* Broken JSON, Dont Care */}
        }
    }
}
