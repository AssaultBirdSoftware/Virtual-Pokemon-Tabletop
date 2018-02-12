using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.SaveData
{
    public class Save : Networking.Data.NetworkCommand
    {
        public SaveStates State { get; set; }
    }
}
