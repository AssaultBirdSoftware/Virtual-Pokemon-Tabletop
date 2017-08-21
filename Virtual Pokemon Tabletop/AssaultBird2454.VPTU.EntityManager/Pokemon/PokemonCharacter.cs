using System;
using System.Collections.Generic;
using System.Text;

namespace AssaultBird2454.VPTU.EntityManager.Pokemon
{
    public class PokemonCharacter
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public decimal Species_DexID { get; set; }
        public List<AssaultBird2454.VPTU.BattleManager.Data.Type> PokemonType { get; set; }
        public int EXP { get; set; }
        public string Held_Item { get; set; }
        public int Loyalty { get; set; }
        public AssaultBird2454.VPTU.Pokedex.Entity.Gender Gender { get; set; }
        public AssaultBird2454.VPTU.BattleManager.Data.Nature Nature { get; set; }
        public List<string> Abilitys { get; set; }
        public AssaultBird2454.VPTU.Pokedex.Entity.SizeClass SizeClass { get; set; }
        public AssaultBird2454.VPTU.Pokedex.Entity.WeightClass WeightClass { get; set; }
        public AssaultBird2454.VPTU.Pokedex.Entity.Capability_Data Capabilities { get; set; }
        public AssaultBird2454.VPTU.Pokedex.Entity.Skill_Data Skills { get; set; }

        public Data.Stats Stat_HP { get; set; }
        public Data.Stats Stat_Attack { get; set; }
        public Data.Stats Stat_Defence { get; set; }
        public Data.Stats Stat_SpAttack { get; set; }
        public Data.Stats Stat_SpDefence { get; set; }
        public Data.Stats Stat_Speed { get; set; }

        public int Contest_Beauty { get; set; }
        public int Contest_Cool { get; set; }
        public int Contest_Cute { get; set; }
        public int Contest_Smart { get; set; }
        public int Contest_Tough { get; set; }

        public int Physical_Evasion { get; set; }
        public int Special_Evasion { get; set; }
        public int Speed_Evasion { get; set; }
        public int Injuries { get; set; }
        public int Current_HP { get; set; }

        public List<string> Moves { get; set; }

        /// <summary>
        /// Data containing information about battle effects like Status Conditions
        /// </summary>
        public BattleManager.Entity.Effects BattleEffects { get; set; }
    }
}
