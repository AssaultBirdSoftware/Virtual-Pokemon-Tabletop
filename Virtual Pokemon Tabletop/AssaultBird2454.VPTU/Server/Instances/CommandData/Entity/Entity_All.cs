using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Entities
{
    public class Entities_All_GetList : Networking.Data.NetworkCommand
    {
        public string Command
        {
            get
            {
                return "Entities_All_GetList";
            }
        }

        public List<EntitiesManager.Folder> Folders { get; set; }
        public List<EntitiesManager.Entry_Data> Entrys { get; set; }
        public List<Authentication_Manager.Data.User> UserList { get; set; }
    }
}
