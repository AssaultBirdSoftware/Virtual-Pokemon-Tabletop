using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.Updater
{
    static class Program
    {
        public static bool Installed = false;

        public static bool Installing = false;
        public static bool Uninstalling = false;

        public static bool Hidden = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            #region Command Switches
            if (args.Contains("+Installed"))
            {
                Installed = true;
            }
            else if (args.Contains("+Standalone"))
            {
                Installed = false;
            }

            if (args.Contains("-Hidden"))
            {
                Hidden = true;
            }
            #endregion

            if (Hidden == true)
            {

            }
            else if (Hidden == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }
    }
}
