using System.Collections.Generic;
using AssaultBird2454.VPTU.BattleManager.Entity;
using AssaultBird2454.VPTU.BattleManager.Entity.Pokemon;
using AssaultBird2454.VPTU.BattleManager.Entity.Trainer;
using AssaultBird2454.VPTU.SaveManager.Resource_Data;
using AssaultBird2454.VPTU.SoundSystem.SaveData;

namespace AssaultBird2454.VPTU.SaveManager.Data.SaveFile
{
    /// <summary>
    ///     A Save Data Class designed for backup purposes. It supports checking if a part of the save has been modified
    ///     outside the Client/Server
    /// </summary>
    public class PTUSaveData_ECC
    {
        public PTUSaveData_ECC()
        {
        }

        public PTUSaveData_ECC(string _Hash, PTUSaveData _Data)
        {
            Hash = _Hash;
            Data = _Data;
        }

        public string Hash { get; set; }
        public PTUSaveData Data { get; set; }
    }

    /// <summary>
    ///     A Save Data Class designed to handle the save data
    /// </summary>
    public class PTUSaveData
    {
        #region Data

        public Pokedex.Save_Data.Pokedex PokedexData;

        #endregion

        /// <summary>
        ///     Creates a new PTUSaveData class
        /// </summary>
        /// <param name="InitNewSave">Initilises all objects</param>
        public PTUSaveData(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Trainers = new List<EntityTrainerData>();
                Pokemon = new List<EntityPokemonData>();

                EntityGroups = new List<EntityGroup>();

                //MapFiles = new List<Resources.MapFileData>();
                //Maps = new List<Resources.MapData>();

                AudioResources = new List<AudioData>();
                ImageResources = new List<Resources>();

                PokedexData = new Pokedex.Save_Data.Pokedex(true);
            }
        }

        /// <summary>
        ///     Creates a new instance of all objects that are null
        /// </summary>
        public void InitNullObjects()
        {
            //MapFiles = new List<Resources.MapFileData>();
            //Maps = new List<Resources.MapData>();

            if (AudioResources == null)
                AudioResources = new List<AudioData>();
            if (ImageResources == null)
                ImageResources = new List<Resources>();

            PokedexData.InitNullObjects();
        }

        #region Entity Data

        public List<EntityTrainerData> Trainers;
        public List<EntityPokemonData> Pokemon;

        public List<EntityGroup> EntityGroups;

        #endregion

        #region Tabletop

        //public List<Resources.MapFileData> MapFiles;
        //public List<Resources.MapData> Maps;

        #endregion

        #region Resources

        public List<AudioData> AudioResources;
        public List<Resources> ImageResources;

        #endregion
    }
}