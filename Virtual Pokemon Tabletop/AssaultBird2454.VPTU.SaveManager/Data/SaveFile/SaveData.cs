using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveManager.Data.SaveFile
{
    /// <summary>
    /// A Save Data Class designed for backup purposes. It supports checking if a part of the save has been modified outside the Client/Server
    /// </summary>
    public class PTUSaveData_ECC
    {
        public PTUSaveData_ECC() { }
        public PTUSaveData_ECC(string _Hash, PTUSaveData _Data)
        {
            Hash = _Hash;
            Data = _Data;
        }

        public string Hash { get; set; }
        public PTUSaveData Data { get; set; }
    }

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
                Trainers = new List<BattleManager.Entity.Trainer.EntityTrainerData>();
                Pokemon = new List<BattleManager.Entity.Pokemon.EntityPokemonData>();

                EntityGroups = new List<BattleManager.Entity.EntityGroup>();

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

            if (AudioResources == null)
            {
                AudioResources = new List<SoundSystem.SaveData.AudioData>();
            }
            if (ImageResources == null)
            {
                ImageResources = new List<Resource_Data.Resources>();
            }

            PokedexData.InitNullObjects();
        }

        #region Data
        public Pokedex.Save_Data.Pokedex PokedexData;
        #endregion

        #region Entity Data
        public List<BattleManager.Entity.Trainer.EntityTrainerData> Trainers;
        public List<BattleManager.Entity.Pokemon.EntityPokemonData> Pokemon;

        public List<BattleManager.Entity.EntityGroup> EntityGroups;
        #endregion

        #region Tabletop
        //public List<Resources.MapFileData> MapFiles;
        //public List<Resources.MapData> Maps;
        #endregion
        #region Resources
        public List<SoundSystem.SaveData.AudioData> AudioResources;
        public List<Resource_Data.Resources> ImageResources;
        #endregion
    }
}
