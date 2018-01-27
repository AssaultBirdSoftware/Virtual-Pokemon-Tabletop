using CommandLine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace AssaultBird2454.VPTU.Installer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string Path_InstallDir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Virtual PTU\");
        public static string Path_UpdaterDir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Virtual PTU\Updater\");
        public static string Path_VersionsDir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Virtual PTU\Versions\");
        public static string Path_OfflineUpdatesDir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Virtual PTU\OfflineUpdates\");

        public App()
        {
            Parser.Default.ParseArguments<Options>(Environment.GetCommandLineArgs()).WithParsed<Options>(new Action<Options>((options) =>
            {
                if (AssemblyDirectory != Path_InstallDir || AssemblyDirectory != Path_UpdaterDir || AssemblyDirectory != Path_VersionsDir || AssemblyDirectory != Path_OfflineUpdatesDir)
                {
                    Install.Setup Setup = new Install.Setup();

                    Setup.ShowDialog();
                }
                else
                {
                    // Updater / Launcher
                }
            }));
        }
    }

    class Options
    {
        [Option("install", Required = false, HelpText = "Installs the software.")]
        public bool Install { get; set; }

        [Option("uninstall", Required = false, HelpText = "Uninstalls the software.")]
        public bool Uninstall { get; set; }

        [Option("update", Required = false, HelpText = "Updates the software.")]
        public bool Update { get; set; }

        /*[Option('a', "add_version", Required = false, HelpText = "When set, the application will automaticly download and install the specified version.")]
        public string Add_Versions { get; set; }
        [Option('r', "remove_version", Required = false, HelpText = "When set, the application will automaticly uninstall and delete the specified version.")]
        public string Remove_Versions { get; set; }*/
    }
}
