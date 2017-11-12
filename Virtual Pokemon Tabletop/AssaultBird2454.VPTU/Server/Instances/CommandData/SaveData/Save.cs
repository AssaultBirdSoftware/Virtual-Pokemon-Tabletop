using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.SaveData
{
    public class Save : Networking.Data.NetworkCommand
    {
        public string Command
        {
            get
            {
                return "Base_SaveData_Save";
            }
        }
        public SaveStates State { get; set; }
    }
}
