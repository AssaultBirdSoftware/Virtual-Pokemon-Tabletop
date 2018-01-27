using AssaultBird2454.VPTU.Installer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Installer.UpdaterAPI
{
    public static class UpdaterAPI
    {
        private static ResourceManager Updater_ResourceManager = new ResourceManager("AssaultBird2454.VPTU.Installer.UpdaterAPI.UpdaterResources", Assembly.GetExecutingAssembly());

        /// <summary>
        /// Returns the build_id of the latest version
        /// </summary>
        public static int Latest_Version
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Latest_Version");

                return Convert.ToInt32(new WebClient().DownloadString(URL));
            }
        }
        /// <summary>
        /// Returns the build_id of the latest version in the alpha release stream
        /// </summary>
        public static int Latest_Version_Alpha
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Latest_Version_Alpha");

                try
                {
                    return Convert.ToInt32(new WebClient().DownloadString(URL));
                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Returns the build_id of the latest version in the beta release stream
        /// </summary>
        public static int Latest_Version_Beta
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Latest_Version_Beta");

                try
                {
                    return Convert.ToInt32(new WebClient().DownloadString(URL));
                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Returns the build_id of the latest version in the master release stream
        /// </summary>
        public static int Latest_Version_Master
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Latest_Version_Master");

                try
                {
                    return Convert.ToInt32(new WebClient().DownloadString(URL));
                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Returns the build_id of the oldest supported version
        /// </summary>
        public static int Oldest_Version_Allowed
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Oldest_Version_Allowed");

                try
                {
                    return Convert.ToInt32(new WebClient().DownloadString(URL));
                }
                catch
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("This code is not read for use (21/01/2018)")]
        public static string UpdateInfo_All
        {
            get
            {
                return "";
                // return Updater_ResourceManager.GetString("UpdateInfo_All").TrimStart('"').TrimEnd('"');
            }
        }

        public static Version_List Version_List
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Version_List");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Version_List>(new WebClient().DownloadString(URL));
            }
        }
        public static List<VersionName> Versions_List_All
        {
            get
            {
                List<VersionName> versions = new List<VersionName>();
                versions.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(Updater_ResourceManager.GetString("Version_List_Alpha"))));
                versions.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(Updater_ResourceManager.GetString("Version_List_Beta"))));
                versions.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(Updater_ResourceManager.GetString("Version_List_Master"))));

                return versions;
            }
        }
        public static VersionName Version_List_Find(int ID)
        {
            return Versions_List_All.Find(x => x.Build_ID == ID);
        }
        public static List<VersionName> Version_List_Alpha
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Version_List_Alpha");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(URL));
            }
        }
        public static List<VersionName> Version_List_Beta
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Version_List_Beta");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(URL));
            }
        }
        public static List<VersionName> Version_List_Master
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Version_List_Master");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<VersionName>>(new WebClient().DownloadString(URL));
            }
        }

        public static List<Data.UpdateData> Get_Versions
        {
            get
            {
                var URL = Updater_ResourceManager.GetString("Versions");

                var Data = new WebClient().DownloadString(URL);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<UpdateData>>(Data);
            }
        }
        public static Data.UpdateData Get_Version(int ID)
        {
            var URL = Updater_ResourceManager.GetString("Version_Get").Replace("[ID]", ID.ToString());

            var Data = new WebClient().DownloadString(URL);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateData>(Data);
        }
    }
}
