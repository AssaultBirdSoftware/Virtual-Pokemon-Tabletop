using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Entity
{
    public class Entity_All_GetList : Networking.Data.NetworkCommand
    {
        public string Command
        {
            get
            {
                return "Entity_All_GetList";
            }
        }

        public List<EntityManager.Folder> Folders { get; set; }
        public List<EntityManager.Entry_Data> Entrys { get; set; }
    }
}
