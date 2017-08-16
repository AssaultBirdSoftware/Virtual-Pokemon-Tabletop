using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server
{
    public class Main
    {
        /// <summary>
        /// A ConsoleLogger Instance
        /// </summary>
        internal static Class.Logging.Logger ConsoleLogger;

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
        public static ProjectInfo VersioningInfo { get; private set; }

        public Main(string[] args = null)
        {
            #region Versioning Info
            using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.Server.ProjectVariables.json"))
            {
                using (StreamReader read = new StreamReader(str))
                {
                    VersioningInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                }
            }
            #endregion


        }

        /// <summary>
        /// Returns the instance of the console logger used for this server instance
        /// </summary>
        /// <returns>Console Logger</returns>
        public Class.Logging.Logger GetLogger()
        {
            return ConsoleLogger;
        }
    }
}