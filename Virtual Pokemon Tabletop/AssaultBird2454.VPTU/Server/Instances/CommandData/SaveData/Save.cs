using AssaultBird2454.VPTU.Networking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.SaveData
{
    public class Save : NetworkCommand
    {
        public string Command { get; set; }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public SaveStates State { get; set; }
    }
}
