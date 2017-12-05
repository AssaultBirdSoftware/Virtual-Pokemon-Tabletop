using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Battle
{
    public class Battle_Instance_List : Networking.Data.NetworkCommand
    {
        public Battle_Instance_List()
        {
            Command = "Battle_Instance_List";
        }

        public string Command { get; set; }
        public IEnumerable<BattleManager.Battle_Instance.Instance> Instances { get; set; }
    }
    public class Battle_Instance : Networking.Data.NetworkCommand
    {
        public Battle_Instance()
        {

        }

        public string Command { get; set; }
        
    }
}
