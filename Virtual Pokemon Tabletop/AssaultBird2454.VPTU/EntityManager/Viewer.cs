
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.EntityManager
{
    public enum Entity_Type { Trainer = 0, Pokemon = 1 }

    public class Folder
    {
        /// <summary>
        /// The folders ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// The Display Name of the folder
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ID of the parent folder (null = root dir)
        /// </summary>
        public string Parent { get; set; }
    }
    public interface Entry
    {
        [JsonIgnore]
        Entry_Data EntryData { get; }

        /// <summary>
        /// Defines what type of entity this is
        /// </summary>
        Entity_Type Entity_Type { get; }
        /// <summary>
        /// The ID of the entity
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// The Name of the entity
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The resource ID for this entry
        /// </summary>
        string Token_ResourceID { get; set; }
        /// <summary>
        /// List of users with View Permission
        /// </summary>
        List<string> View { get; set; }
        /// <summary>
        /// List of users with Edit Permission
        /// </summary>
        List<string> Edit { get; set; }

        /// <summary>
        /// The folder that this entry is placed in (null = root)
        /// </summary>
        string Parent_Folder { get; set; }
    }
    public class Entry_Data
    {
        /// <summary>
        /// Defines what type of entity this is
        /// </summary>
        public Entity_Type Entity_Type { get; set; }
        /// <summary>
        /// The ID of the entity
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// The Name of the entity
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The resource ID for this entry
        /// </summary>
        public string Token_ResourceID { get; set; }
        /// <summary>
        /// List of users with View Permission
        /// </summary>
        public List<string> View;
        /// <summary>
        /// List of users with Edit Permission
        /// </summary>
        public List<string> Edit;

        /// <summary>
        /// The folder that this entry is placed in (null = root)
        /// </summary>
        public string Parent_Folder { get; set; }
    }
}
