using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Installer.Data
{
    /// <summary>
    /// Defines what release stream to use
    /// </summary>
    public enum Stream
    {
        Alpha = 0,
        Beta = 1,
        master = 2
    }
    /// <summary>
    /// Define when updates can be downloaded and installed
    /// </summary>
    public enum Mode
    {
        Install = 0, // Download and install new updates
        Download = 1, // Download new updates, Ask before installing updates
        Prompt = 2 // Dont Download new updates, Ask to download & install
    }
    /// <summary>
    /// Actions to perform before installing updates
    /// </summary>
    public enum UpdateAction
    {
        Duplicate = 0, // Copy the instance as another instance before update
        Backup = 1, // Backup all instance files to backup folder before update
    }

    public class InstallationConfig
    {
        public List<InstalledVersion> Versions { get; set; }
        public List<OfflineUpdate> OfflineUpdates { get; set; }
    }

    public class InstalledVersion
    {
        public InstalledVersion()
        {

        }

        /// <summary>
        /// A name for this installation
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The build id on the update server
        /// </summary>
        public int Build_ID { get; set; }
        /// <summary>
        /// The Version Name that this Instance is using
        /// </summary>
        public string Version_Name { get; set; }

        /// <summary>
        /// Where this instance is installed
        /// </summary>
        public string Installation_Directory { get; set; }

        private bool AutoUpdating { get; set; }
        /// <summary>
        /// Defines if this instance can be automaticly updated
        /// </summary>
        public bool AutoUpdating_Enabled
        {
            get
            {
                return AutoUpdating;
            }
            set
            {
                if (value)
                {
                    if (AutoUpdating == value)
                        return;

                    AutoUpdating = true;
                    AutoUpdating_Config = new AutoUpdateConfig();
                }
                else
                {
                    if (AutoUpdating == value)
                        return;

                    AutoUpdating = false;
                    AutoUpdating_Config = null;
                }
            }
        }
        /// <summary>
        /// The settings for the autoupdater
        /// </summary>
        public AutoUpdateConfig AutoUpdating_Config { get; set; }
    }
    public class OfflineUpdate
    {
        public string Build_ID { get; set; }
        public string Version_Name { get; set; }

        public string UpdateFile_Directory { get; set; }
    }

    public class AutoUpdateConfig
    {
        public AutoUpdateConfig()
        {
            Update_Actions = new List<UpdateAction>();
        }

        public Stream ReleaseStream { get; set; }
        public Mode Update_Mode { get; set; }
        public List<UpdateAction> Update_Actions { get; set; }
    }
}
