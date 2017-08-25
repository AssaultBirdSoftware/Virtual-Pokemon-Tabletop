using System;
using System.Collections.Generic;
using System.Linq;
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

        #region Stats
        #region HP
        #region Species Base
        private int Stat_HP_SpeciesBase;
        public int HP_SpeciesBase
        {
            get
            {
                return Stat_HP_SpeciesBase;
            }
            set
            {
                Stat_HP_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_HP_BaseMod;
        public int HP_BaseMod
        {
            get
            {
                return Stat_HP_BaseMod;
            }
            set
            {
                Stat_HP_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int HP_Base
        {
            get
            {
                return HP_SpeciesBase + HP_BaseMod;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_HP_AddStat;
        public int HP_AddStat
        {
            get
            {
                return Stat_HP_AddStat;
            }
            set
            {
                Stat_HP_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int HP_Total
        {
            get
            {
                return HP_Base + HP_AddStat;
            }
        }
        #endregion

        #region Value
        public int Stat_HP_Max
        {
            get
            {
                //HP = Pokemon Level + (HP x3) + 10
                return 0;
            }
        }
        #endregion
        #endregion
        #region Attack
        #region Species Base
        private int Stat_Attack_SpeciesBase;
        public int Attack_SpeciesBase
        {
            get
            {
                return Stat_Attack_SpeciesBase;
            }
            set
            {
                Stat_Attack_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_Attack_BaseMod;
        public int Attack_BaseMod
        {
            get
            {
                return Stat_Attack_BaseMod;
            }
            set
            {
                Stat_Attack_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int Attack_Base
        {
            get
            {
                return Attack_SpeciesBase + Attack_BaseMod;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_Attack_AddStat;
        public int Attack_AddStat
        {
            get
            {
                return Stat_Attack_AddStat;
            }
            set
            {
                Stat_Attack_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int Attack_Total
        {
            get
            {
                return Attack_Base + Attack_AddStat;
            }
        }
        #endregion

        #region Combat Stage
        private int Stat_Attack_CombatStage;
        public int Attack_CombatStage
        {
            get
            {
                return Stat_Attack_CombatStage;
            }
            set
            {
                Stat_Attack_CombatStage = value;
            }
        }
        #endregion

        #region Combat Stage Adjusted
        public int Attack_Adjusted
        {
            get
            {
                switch (Stat_Attack_CombatStage)
                {
                    case -6:
                        return (int)Math.Floor(Attack_Total * 0.4);

                    case -5:
                        return (int)Math.Floor(Attack_Total * 0.5);

                    case -4:
                        return (int)Math.Floor(Attack_Total * 0.6);

                    case -3:
                        return (int)Math.Floor(Attack_Total * 0.7);

                    case -2:
                        return (int)Math.Floor(Attack_Total * 0.8);

                    case -1:
                        return (int)Math.Floor(Attack_Total * 0.9);

                    case 0:
                        return Attack_Total;

                    case 1:
                        return (int)Math.Floor(Attack_Total * 1.2);

                    case 2:
                        return (int)Math.Floor(Attack_Total * 1.4);

                    case 3:
                        return (int)Math.Floor(Attack_Total * 1.6);

                    case 4:
                        return (int)Math.Floor(Attack_Total * 1.8);

                    case 5:
                        return (int)Math.Floor(Attack_Total * 2.0);

                    case 6:
                        return (int)Math.Floor(Attack_Total * 2.2);

                    default:
                        return Attack_Total;
                }
            }
        }
        #endregion
        #endregion
        #endregion

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

        #region Functions
        public int Level
        {
            get
            {
                if (Enumerable.Range(0, 9).Contains(EXP))
                {
                    return 1;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 2;
                }
                else if (Enumerable.Range(20, 29).Contains(EXP))
                {
                    return 3;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 4;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 5;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 6;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 7;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 8;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 9;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 10;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 11;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 12;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 13;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 14;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 15;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 16;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 17;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 18;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 19;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 20;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 21;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 22;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 23;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 24;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 25;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 26;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 27;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 28;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 29;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 30;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 31;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 32;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 33;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 34;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 35;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 36;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 37;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 38;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 39;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 40;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 41;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 42;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 43;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 44;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 45;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 46;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 47;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 48;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 49;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 50;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 51;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 52;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 53;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 54;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 55;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 56;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 57;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 58;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 59;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 60;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 61;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 62;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 63;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 64;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 65;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 66;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 67;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 68;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 69;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 70;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 71;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 72;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 73;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 74;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 75;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 76;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 77;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 78;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 79;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 80;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 81;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 82;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 83;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 84;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 85;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 86;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 87;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 88;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 89;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 90;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 91;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 92;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 93;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 94;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 95;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 96;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 97;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 98;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 99;
                }
                else if (Enumerable.Range(10, 19).Contains(EXP))
                {
                    return 100;
                }
                return 0;
            }
        }
        #endregion
    }
}
