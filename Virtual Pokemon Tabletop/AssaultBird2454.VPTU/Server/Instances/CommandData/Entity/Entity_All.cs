using AssaultBird2454.VPTU.Networking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Entities
{
    public class Entities_All_GetList : NetworkCommand
    {
        public string Command { get { return "Entities_All_GetList"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public List<EntitiesManager.Folder> Folders { get; set; }
        public List<EntitiesManager.Entry_Data> Entrys { get; set; }
        public List<Authentication_Manager.Data.User> UserList { get; set; }
    }
}
