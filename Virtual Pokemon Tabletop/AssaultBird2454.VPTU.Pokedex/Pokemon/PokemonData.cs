using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Pokedex.Pokemon
{
    public class PokemonData
    {
        public PokemonData()
        {
            Species_Capability_Data = new Entity.Capability_Data();
            Species_Skill_Data = new Entity.Skill_Data();
            Moves = new List<Link_Moves>();
            Abilitys = new List<Link_Ability>();
            Evolutions = new List<Pokemon.Link_Evolutions>();
            Species_SpecialCapability = new List<KeyValuePair<Pokemon_Capabilities, object>>();
        }

        /// <summary>
        /// The Pokedex Number for the Pokemon
        /// </summary>
        public decimal Species_DexID { get; set; }
        /// <summary>
        /// The Species name for the Pokemon
        /// </summary>
        public string Species_Name { get; set; }
        /// <summary>
        /// The Species Pokedex Description
        /// </summary>
        public string Species_Desc { get; set; }

        /// <summary>
        /// Data Class for tracking Skill Data
        /// </summary>
        public Entity.Skill_Data Species_Skill_Data { get; set; }
        /// <summary>
        /// Data Class for tracking Capability's
        /// </summary>
        public Entity.Capability_Data Species_Capability_Data { get; set; }
        /// <summary>
        /// List for tracking Special Capabilities and their values (If no value is kept with it then the value = null)
        /// </summary>
        public List<KeyValuePair<Pokemon_Capabilities, object>> Species_SpecialCapability { get; set; }

        public List<BattleManager.Data.Type> Species_Types { get; set; }
        public Entity.SizeClass Species_SizeClass { get; set; }
        public Entity.WeightClass Species_WeightClass { get; set; }

        #region Breeding and Gender
        public PokemonGender Species_Genders { get; set; }
        public decimal Species_Gender_Chance_Male { get; set; }
        public decimal Species_Gender_Chance_Female { get; set; }
        #endregion

        #region Base Stats
        public int Species_BaseStats_HP { get; set; }
        public int Species_BaseStats_Attack { get; set; }
        public int Species_BaseStats_Defence { get; set; }
        public int Species_BaseStats_SpAttack { get; set; }
        public int Species_BaseStats_SpDefence { get; set; }
        public int Species_BaseStats_Speed { get; set; }
        #endregion

        #region Texture and Resource
        public string Sprite_Normal { get; set; }
        public string Sprite_Shiny { get; set; }
        public string Sprite_Egg { get; set; }
        public string Sound_Cry { get; set; }
        #endregion

        public List<Link_Moves> Moves { get; set; }
        public List<Link_Ability> Abilitys { get; set; }
        public List<Link_Evolutions> Evolutions { get; set; }

        public void Dispose()
        {
            Moves = null;
            Abilitys = null;
            Evolutions = null;
            Species_Capability_Data = null;
            Species_SpecialCapability = null;
            Species_Skill_Data = null;
        }
    }
}