using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveManager.Data.SaveFile
{
    /// <summary>
    /// A Save Data Class designed to handle the save data
    /// </summary>
    public class PTUSaveData
    {
        /// <summary>
        /// Creates a new PTUSaveData class
        /// </summary>
        /// <param name="InitNewSave">Initilises all objects</param>
        public PTUSaveData(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Campaign_Data = new Campaign_Data(true);

                Identities = new List<Authentication_Manager.Data.Identity>();
                Users = new List<Authentication_Manager.Data.User>();
                Groups = new List<Authentication_Manager.Data.Group>();

                Folders = new List<EntitiesManager.Folder>();
                Trainers = new List<EntitiesManager.Trainer.TrainerCharacter>();
                Pokemon = new List<EntitiesManager.Pokemon.PokemonCharacter>();

                //MapFiles = new List<Resources.MapFileData>();
                //Maps = new List<Resources.MapData>();

                AudioResources = new List<SoundSystem.SaveData.AudioData>();
                ImageResources = new List<Resource_Data.Resources>();

                PokedexData = new Pokedex.Save_Data.Pokedex(true);
            }
        }

        /// <summary>
        /// Creates a new instance of all objects that are null
        /// </summary>
        public void InitNullObjects()
        {
            //MapFiles = new List<Resources.MapFileData>();
            //Maps = new List<Resources.MapData>();

            if(Identities == null)
            {
                Identities = new List<Authentication_Manager.Data.Identity>();
            }
            if (Users == null)
            {
                Users = new List<Authentication_Manager.Data.User>();
            }
            if (Groups == null)
            {
                Groups = new List<Authentication_Manager.Data.Group>();
            }

            if (AudioResources == null)
            {
                AudioResources = new List<SoundSystem.SaveData.AudioData>();
            }
            if (ImageResources == null)
            {
                ImageResources = new List<Resource_Data.Resources>();
            }

            if (Folders == null)
            {
                Folders = new List<EntitiesManager.Folder>();
            }
            if (Pokemon == null)
            {
                Pokemon = new List<EntitiesManager.Pokemon.PokemonCharacter>();
            }

            PokedexData.InitNullObjects();
            Campaign_Data.InitNullObjects();
        }

        #region Data
        public Data.Campaign_Data Campaign_Data;
        #endregion

        #region Auth and Perms
        public List<Authentication_Manager.Data.Identity> Identities;
        public List<Authentication_Manager.Data.User> Users;
        public List<Authentication_Manager.Data.Group> Groups;

        public string Identity_GetKey(string UserID)
        {
            if(Identities.FindAll(X => X.UserID == UserID).Count <= 0)
            {
                Identities.Add(new Authentication_Manager.Data.Identity()
                {
                    UserID = UserID
                });
            }

            Authentication_Manager.Data.Identity ID = Identities.Find(x => x.UserID == UserID);
            if (ID == null)
                return "";

            return ID.Key;
        }
        #endregion

        public Pokedex.Save_Data.Pokedex PokedexData;

        #region Entities Data
        public List<EntitiesManager.Folder> Folders;
        public List<EntitiesManager.Trainer.TrainerCharacter> Trainers;
        public List<EntitiesManager.Pokemon.PokemonCharacter> Pokemon;

        /// <summary>
        /// Helper Function, This function will return the tree of folders to get to the child folder specified
        /// </summary>
        /// <param name="Child">The ID of the folder that is trying to be retrieved</param>
        /// <returns>List of folders to the desired child folder</returns>
        public List<EntitiesManager.Folder> Folders_GetTreeFrom(string Child)
        {
            List<EntitiesManager.Folder> list;
            EntitiesManager.Folder folder = Folders.Find(x => x.ID == Child);

            if (folder.Parent == null)
            {
                list = new List<EntitiesManager.Folder>();
            }
            else
            {
                list = Folders_GetTreeFrom(folder.Parent);
            }

            list.Add(folder);

            return list;
        }
        #endregion

        #region Tabletop
        //public List<Resources.MapFileData> MapFiles;
        //public List<Resources.MapData> Maps;
        #endregion
        #region Resources
        public List<SoundSystem.SaveData.AudioData> AudioResources;
        public List<Resource_Data.Resources> ImageResources;
        #endregion

        #region Settings

        #endregion
    }
}
