using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    public enum Commands { }

    public class NetworkData_Data
    {
        public Commands Command { get; set; }
        public object CommandObject { get; set; }
    }
}
