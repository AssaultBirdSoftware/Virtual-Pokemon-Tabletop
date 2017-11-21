using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.Client
{
    public static class Program
    {
        public static MainWindow MainWindow { get; internal set; }
        public static Server.Instances.ClientInstance ClientInstance { get; set; }
        public static Server.Instances.ServerInstance ServerInstance { get; set; }
        public static SaveManager.Data.SaveFile.PTUSaveData DataCache { get; set; }

        public static NotifyIcon NotifyIcon { get; internal set; }
        public static object ClientLogger
        {
            get
            {
                return ClientInstance.Client_Logger;
            }
            set
            {
                if (ClientInstance != null)
                {
                    if (value is VPTU.Server.Class.Logging.I_Logger)
                    {
                        ClientInstance.Client_Logger = value;
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
