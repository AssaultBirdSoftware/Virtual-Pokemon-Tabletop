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

                Trainers = new List<EntityManager.Trainer.TrainerCharacter>();
                Pokemon = new List<EntityManager.Pokemon.PokemonCharacter>();

                EntityGroups = new List<Pokedex.Entity.EntityGroup>();

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

            if (Pokemon == null)
            {
                Pokemon = new List<EntityManager.Pokemon.PokemonCharacter>();
            }

            PokedexData.InitNullObjects();
            Campaign_Data.InitNullObjects();
        }

        #region Data
        public Data.Campaign_Data Campaign_Data;
        #endregion

        public Pokedex.Save_Data.Pokedex PokedexData;

        #region Entity Data
        public List<EntityManager.Trainer.TrainerCharacter> Trainers;
        public List<EntityManager.Pokemon.PokemonCharacter> Pokemon;

        public List<Pokedex.Entity.EntityGroup> EntityGroups;
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
