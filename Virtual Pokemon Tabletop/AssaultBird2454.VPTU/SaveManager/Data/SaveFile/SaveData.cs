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
                Campaign_Settings = new Campaign_Settings(true);
                Server_Settings = new Server_Settings(true);

                Typing_Manager = new BattleManager.Typing.Manager(true);

                Identities = new List<Authentication_Manager.Data.Identity>();
                Permissions = new List<Authentication_Manager.Data.PermissionData>();
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

            if (Identities == null)
                Identities = new List<Authentication_Manager.Data.Identity>();
            if (Permissions == null)
                Permissions = new List<Authentication_Manager.Data.PermissionData>();
            if (Users == null)
                Users = new List<Authentication_Manager.Data.User>();
            if (Groups == null)
                Groups = new List<Authentication_Manager.Data.Group>();

            if (AudioResources == null)
                AudioResources = new List<SoundSystem.SaveData.AudioData>();
            if (ImageResources == null)
                ImageResources = new List<Resource_Data.Resources>();

            if (Folders == null)
                Folders = new List<EntitiesManager.Folder>();
            if (Pokemon == null)
                Pokemon = new List<EntitiesManager.Pokemon.PokemonCharacter>();

            if (PokedexData == null)
                PokedexData = new Pokedex.Save_Data.Pokedex(true);
            PokedexData.InitNullObjects();
            if (Campaign_Data == null)
                Campaign_Data = new Campaign_Data(true);
            Campaign_Data.InitNullObjects();
            if (Campaign_Settings == null)
                Campaign_Settings = new Campaign_Settings(true);
            Campaign_Settings.InitNullObjects();
            if (Server_Settings == null)
                Server_Settings = new Server_Settings(true);
            Server_Settings.InitNullObjects();

            if (Typing_Manager == null)
                Typing_Manager = new BattleManager.Typing.Manager(true);
            Typing_Manager.InitNullObjects();
        }

        #region Data
        public Data.Campaign_Data Campaign_Data;
        public Data.Campaign_Settings Campaign_Settings;
        public Data.Server_Settings Server_Settings;
        #endregion

        #region Auth and Perms
        public List<Authentication_Manager.Data.Identity> Identities;
        public List<Authentication_Manager.Data.PermissionData> Permissions;
        public List<Authentication_Manager.Data.User> Users;
        public List<Authentication_Manager.Data.Group> Groups;

        public string Identity_GetKey(string UserID)
        {
            if (Identities.FindAll(X => X.UserID == UserID).Count <= 0)
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

        #region Battles
        public BattleManager.Typing.Manager Typing_Manager;
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
