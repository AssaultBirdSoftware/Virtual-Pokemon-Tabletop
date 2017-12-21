using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveManager.Identity
{
    public class Identity_Data
    {
        public Identity_Data()
        {

        }

        public string UserID { get; set; }
        public string Key { get; set; }
        public string Server_Address { get; set; }
        public int Server_Port { get; set; }
        public string ICN { get; set; }
        public string Campaign_Name { get; set; }
        public System.Windows.Media.Color UserColor { get; set; }
    }
}
