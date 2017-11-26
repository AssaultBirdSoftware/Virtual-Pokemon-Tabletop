using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Authentication_Manager.Data
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string IC_Name { get; set; }

        public List<string> Groups { get; set; }
        public List<Identity> Identitys { get; set; }
        public List<Permissions_Manager.Data.Permission_Node> Permissions { get; set; }
    }

    public class Group
    {
        public string GroupID { get; set; }
        public string Name { get; set; }

        public List<Permissions_Manager.Data.Permission_Node> Permissions { get; set; }
    }

    public class Identity
    {
        /// <summary>
        /// A 32char long random string that is unique to a player
        /// </summary>
        public string Identity_String { get; set; }
    }
}
