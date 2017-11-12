using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaultBird2454.VPTU.Client
{
    public static class BaseApplication
    {
        public static MainWindow MainWindow { get; set; }
        public static NotifyIcon Client_NotifyIcon { get; set; }
        public static Server.Instances.ClientInstance Client_Instance { get; set; }
        public static Server.Instances.ServerInstance Session_LanServer { get; set; }
    }
}
