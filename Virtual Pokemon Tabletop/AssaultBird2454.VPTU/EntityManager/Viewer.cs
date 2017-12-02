
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.EntityManager
{
    public class EntityViewer
    {
        /// <summary>
        /// Creates a new EntryViewer Save Data Manager
        /// </summary>
        /// <param name="InitNewSave">Initialize new data</param>
        public EntityViewer(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Folders = new List<Folder>();// Initilises the Folders
                Entrys = new List<Entry>();// Initilises the Entrys
            }
        }

        public void InitNullObjects()
        {
            if (Folders == null)
            {
                Folders = new List<Folder>();// Initilises the Folders
            }
            if (Entrys == null)
            {
                Entrys = new List<Entry>();// Initilises the Entrys
            }
        }

        public List<Folder> Folders { get; set; }
        public List<Entry> Entrys { get; set; }
    }

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
    public class Entry
    {
        /// <summary>
        /// The ID of the entity that this entry represents
        /// </summary>
        public string EntityID { get; set; }
        /// <summary>
        /// The Name of the entity
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The resource ID for this entry
        /// </summary>
        public string Token_ResourceID { get; set; }

        /// <summary>
        /// The folder that this entry is placed in (null = root)
        /// </summary>
        public string Parent_Folder { get; set; }
    }
}
