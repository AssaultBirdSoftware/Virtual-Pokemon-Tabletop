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
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Cuddly || Nature == BattleManager.Data.Nature.Distracted || Nature == BattleManager.Data.Nature.Proud || Nature == BattleManager.Data.Nature.Decisive || Nature == BattleManager.Data.Nature.Patient)
                {
                    NatureBonus = 1;
                }
                else if (Nature == BattleManager.Data.Nature.Desperate || Nature == BattleManager.Data.Nature.Stark || Nature == BattleManager.Data.Nature.Curious || Nature == BattleManager.Data.Nature.Dreamy || Nature == BattleManager.Data.Nature.Skittish)
                {
                    NatureBonus = -1;
                }

                return HP_SpeciesBase + HP_BaseMod + NatureBonus;
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

        #region Stat Max HP
        public int Stat_HP_Max
        {
            get
            {
                return (Level + (HP_Total * 3) + 10);
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
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Desperate || Nature == BattleManager.Data.Nature.Lonely || Nature == BattleManager.Data.Nature.Adamant || Nature == BattleManager.Data.Nature.Naughty || Nature == BattleManager.Data.Nature.Brave)
                {
                    NatureBonus = 2;
                }
                else if (Nature == BattleManager.Data.Nature.Cuddly || Nature == BattleManager.Data.Nature.Bold || Nature == BattleManager.Data.Nature.Modest || Nature == BattleManager.Data.Nature.Calm || Nature == BattleManager.Data.Nature.Timid)
                {
                    NatureBonus = -2;
                }

                return Attack_SpeciesBase + Attack_BaseMod + NatureBonus;
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
        #region Defence
        #region Species Base
        private int Stat_Defence_SpeciesBase;
        public int Defence_SpeciesBase
        {
            get
            {
                return Stat_Defence_SpeciesBase;
            }
            set
            {
                Stat_Defence_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_Defence_BaseMod;
        public int Defence_BaseMod
        {
            get
            {
                return Stat_Defence_BaseMod;
            }
            set
            {
                Stat_Defence_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int Defence_Base
        {
            get
            {
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Stark || Nature == BattleManager.Data.Nature.Bold || Nature == BattleManager.Data.Nature.Impish || Nature == BattleManager.Data.Nature.Lax || Nature == BattleManager.Data.Nature.Relaxed)
                {
                    NatureBonus = 2;
                }
                else if (Nature == BattleManager.Data.Nature.Distracted || Nature == BattleManager.Data.Nature.Lonely || Nature == BattleManager.Data.Nature.Mild || Nature == BattleManager.Data.Nature.Gentle || Nature == BattleManager.Data.Nature.Hasty)
                {
                    NatureBonus = -2;
                }

                return Defence_SpeciesBase + Defence_BaseMod + NatureBonus;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_Defence_AddStat;
        public int Defence_AddStat
        {
            get
            {
                return Stat_Defence_AddStat;
            }
            set
            {
                Stat_Defence_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int Defence_Total
        {
            get
            {
                return Defence_Base + Defence_AddStat;
            }
        }
        #endregion

        #region Combat Stage
        private int Stat_Defence_CombatStage;
        public int Defence_CombatStage
        {
            get
            {
                return Stat_Defence_CombatStage;
            }
            set
            {
                Stat_Defence_CombatStage = value;
            }
        }
        #endregion

        #region Combat Stage Adjusted
        public int Defence_Adjusted
        {
            get
            {
                switch (Stat_Defence_CombatStage)
                {
                    case -6:
                        return (int)Math.Floor(Defence_Total * 0.4);

                    case -5:
                        return (int)Math.Floor(Defence_Total * 0.5);

                    case -4:
                        return (int)Math.Floor(Defence_Total * 0.6);

                    case -3:
                        return (int)Math.Floor(Defence_Total * 0.7);

                    case -2:
                        return (int)Math.Floor(Defence_Total * 0.8);

                    case -1:
                        return (int)Math.Floor(Defence_Total * 0.9);

                    case 0:
                        return Defence_Total;

                    case 1:
                        return (int)Math.Floor(Defence_Total * 1.2);

                    case 2:
                        return (int)Math.Floor(Defence_Total * 1.4);

                    case 3:
                        return (int)Math.Floor(Defence_Total * 1.6);

                    case 4:
                        return (int)Math.Floor(Defence_Total * 1.8);

                    case 5:
                        return (int)Math.Floor(Defence_Total * 2.0);

                    case 6:
                        return (int)Math.Floor(Defence_Total * 2.2);

                    default:
                        return Defence_Total;
                }
            }
        }
        #endregion
        #endregion
        #region SpAttack
        #region Species Base
        private int Stat_SpAttack_SpeciesBase;
        public int SpAttack_SpeciesBase
        {
            get
            {
                return Stat_SpAttack_SpeciesBase;
            }
            set
            {
                Stat_SpAttack_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_SpAttack_BaseMod;
        public int SpAttack_BaseMod
        {
            get
            {
                return Stat_SpAttack_BaseMod;
            }
            set
            {
                Stat_SpAttack_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int SpAttack_Base
        {
            get
            {
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Curious || Nature == BattleManager.Data.Nature.Modest || Nature == BattleManager.Data.Nature.Mild || Nature == BattleManager.Data.Nature.Rash || Nature == BattleManager.Data.Nature.Quiet)
                {
                    NatureBonus = 2;
                }
                else if (Nature == BattleManager.Data.Nature.Proud || Nature == BattleManager.Data.Nature.Adamant || Nature == BattleManager.Data.Nature.Impish || Nature == BattleManager.Data.Nature.Careful || Nature == BattleManager.Data.Nature.Jolly)
                {
                    NatureBonus = -2;
                }

                return SpAttack_SpeciesBase + SpAttack_BaseMod + NatureBonus;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_SpAttack_AddStat;
        public int SpAttack_AddStat
        {
            get
            {
                return Stat_SpAttack_AddStat;
            }
            set
            {
                Stat_SpAttack_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int SpAttack_Total
        {
            get
            {
                return SpAttack_Base + SpAttack_AddStat;
            }
        }
        #endregion

        #region Combat Stage
        private int Stat_SpAttack_CombatStage;
        public int SpAttack_CombatStage
        {
            get
            {
                return Stat_SpAttack_CombatStage;
            }
            set
            {
                Stat_SpAttack_CombatStage = value;
            }
        }
        #endregion

        #region Combat Stage Adjusted
        public int SpAttack_Adjusted
        {
            get
            {
                switch (Stat_SpAttack_CombatStage)
                {
                    case -6:
                        return (int)Math.Floor(SpAttack_Total * 0.4);

                    case -5:
                        return (int)Math.Floor(SpAttack_Total * 0.5);

                    case -4:
                        return (int)Math.Floor(SpAttack_Total * 0.6);

                    case -3:
                        return (int)Math.Floor(SpAttack_Total * 0.7);

                    case -2:
                        return (int)Math.Floor(SpAttack_Total * 0.8);

                    case -1:
                        return (int)Math.Floor(SpAttack_Total * 0.9);

                    case 0:
                        return SpAttack_Total;

                    case 1:
                        return (int)Math.Floor(SpAttack_Total * 1.2);

                    case 2:
                        return (int)Math.Floor(SpAttack_Total * 1.4);

                    case 3:
                        return (int)Math.Floor(SpAttack_Total * 1.6);

                    case 4:
                        return (int)Math.Floor(SpAttack_Total * 1.8);

                    case 5:
                        return (int)Math.Floor(SpAttack_Total * 2.0);

                    case 6:
                        return (int)Math.Floor(SpAttack_Total * 2.2);

                    default:
                        return SpAttack_Total;
                }
            }
        }
        #endregion
        #endregion
        #region SpDefence
        #region Species Base
        private int Stat_SpDefence_SpeciesBase;
        public int SpDefence_SpeciesBase
        {
            get
            {
                return Stat_SpDefence_SpeciesBase;
            }
            set
            {
                Stat_SpDefence_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_SpDefence_BaseMod;
        public int SpDefence_BaseMod
        {
            get
            {
                return Stat_SpDefence_BaseMod;
            }
            set
            {
                Stat_SpDefence_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int SpDefence_Base
        {
            get
            {
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Dreamy || Nature == BattleManager.Data.Nature.Calm || Nature == BattleManager.Data.Nature.Gentle || Nature == BattleManager.Data.Nature.Careful || Nature == BattleManager.Data.Nature.Sassy)
                {
                    NatureBonus = 2;
                }
                else if (Nature == BattleManager.Data.Nature.Decisive || Nature == BattleManager.Data.Nature.Naughty || Nature == BattleManager.Data.Nature.Lax || Nature == BattleManager.Data.Nature.Rash || Nature == BattleManager.Data.Nature.Naive)
                {
                    NatureBonus = -2;
                }

                return SpDefence_SpeciesBase + SpDefence_BaseMod + NatureBonus;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_SpDefence_AddStat;
        public int SpDefence_AddStat
        {
            get
            {
                return Stat_SpDefence_AddStat;
            }
            set
            {
                Stat_SpDefence_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int SpDefence_Total
        {
            get
            {
                return SpDefence_Base + SpDefence_AddStat;
            }
        }
        #endregion

        #region Combat Stage
        private int Stat_SpDefence_CombatStage;
        public int SpDefence_CombatStage
        {
            get
            {
                return Stat_SpDefence_CombatStage;
            }
            set
            {
                Stat_SpDefence_CombatStage = value;
            }
        }
        #endregion

        #region Combat Stage Adjusted
        public int SpDefence_Adjusted
        {
            get
            {
                switch (Stat_SpDefence_CombatStage)
                {
                    case -6:
                        return (int)Math.Floor(SpDefence_Total * 0.4);

                    case -5:
                        return (int)Math.Floor(SpDefence_Total * 0.5);

                    case -4:
                        return (int)Math.Floor(SpDefence_Total * 0.6);

                    case -3:
                        return (int)Math.Floor(SpDefence_Total * 0.7);

                    case -2:
                        return (int)Math.Floor(SpDefence_Total * 0.8);

                    case -1:
                        return (int)Math.Floor(SpDefence_Total * 0.9);

                    case 0:
                        return SpDefence_Total;

                    case 1:
                        return (int)Math.Floor(SpDefence_Total * 1.2);

                    case 2:
                        return (int)Math.Floor(SpDefence_Total * 1.4);

                    case 3:
                        return (int)Math.Floor(SpDefence_Total * 1.6);

                    case 4:
                        return (int)Math.Floor(SpDefence_Total * 1.8);

                    case 5:
                        return (int)Math.Floor(SpDefence_Total * 2.0);

                    case 6:
                        return (int)Math.Floor(SpDefence_Total * 2.2);

                    default:
                        return SpDefence_Total;
                }
            }
        }
        #endregion
        #endregion
        #region Speed
        #region Species Base
        private int Stat_Speed_SpeciesBase;
        public int Speed_SpeciesBase
        {
            get
            {
                return Stat_Speed_SpeciesBase;
            }
            set
            {
                Stat_Speed_SpeciesBase = value;
            }
        }
        #endregion

        #region Base Mod
        private int Stat_Speed_BaseMod;
        public int Speed_BaseMod
        {
            get
            {
                return Stat_Speed_BaseMod;
            }
            set
            {
                Stat_Speed_BaseMod = value;
            }
        }
        #endregion

        #region Base Stat
        public int Speed_Base
        {
            get
            {
                int NatureBonus = 0;

                if (Nature == BattleManager.Data.Nature.Skittish || Nature == BattleManager.Data.Nature.Timid || Nature == BattleManager.Data.Nature.Hasty || Nature == BattleManager.Data.Nature.Jolly || Nature == BattleManager.Data.Nature.Naive)
                {
                    NatureBonus = 2;
                }
                else if (Nature == BattleManager.Data.Nature.Patient || Nature == BattleManager.Data.Nature.Brave || Nature == BattleManager.Data.Nature.Relaxed || Nature == BattleManager.Data.Nature.Quiet || Nature == BattleManager.Data.Nature.Sassy)
                {
                    NatureBonus = -2;
                }

                return Speed_SpeciesBase + Speed_BaseMod + NatureBonus;
            }
        }
        #endregion

        #region Add Stat
        private int Stat_Speed_AddStat;
        public int Speed_AddStat
        {
            get
            {
                return Stat_Speed_AddStat;
            }
            set
            {
                Stat_Speed_AddStat = value;
            }
        }
        #endregion

        #region Total
        public int Speed_Total
        {
            get
            {
                return Speed_Base + Speed_AddStat;
            }
        }
        #endregion

        #region Combat Stage
        private int Stat_Speed_CombatStage;
        public int Speed_CombatStage
        {
            get
            {
                return Stat_Speed_CombatStage;
            }
            set
            {
                Stat_Speed_CombatStage = value;
            }
        }
        #endregion

        #region Combat Stage Adjusted
        public int Speed_Adjusted
        {
            get
            {
                switch (Stat_Speed_CombatStage)
                {
                    case -6:
                        return (int)Math.Floor(Speed_Total * 0.4);

                    case -5:
                        return (int)Math.Floor(Speed_Total * 0.5);

                    case -4:
                        return (int)Math.Floor(Speed_Total * 0.6);

                    case -3:
                        return (int)Math.Floor(Speed_Total * 0.7);

                    case -2:
                        return (int)Math.Floor(Speed_Total * 0.8);

                    case -1:
                        return (int)Math.Floor(Speed_Total * 0.9);

                    case 0:
                        return Speed_Total;

                    case 1:
                        return (int)Math.Floor(Speed_Total * 1.2);

                    case 2:
                        return (int)Math.Floor(Speed_Total * 1.4);

                    case 3:
                        return (int)Math.Floor(Speed_Total * 1.6);

                    case 4:
                        return (int)Math.Floor(Speed_Total * 1.8);

                    case 5:
                        return (int)Math.Floor(Speed_Total * 2.0);

                    case 6:
                        return (int)Math.Floor(Speed_Total * 2.2);

                    default:
                        return Speed_Total;
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
                /* Level 1 - 20 (EXP 0 - 459) */
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
                else if (Enumerable.Range(30, 39).Contains(EXP))
                {
                    return 4;
                }
                else if (Enumerable.Range(40, 49).Contains(EXP))
                {
                    return 5;
                }
                else if (Enumerable.Range(50, 59).Contains(EXP))
                {
                    return 6;
                }
                else if (Enumerable.Range(60, 69).Contains(EXP))
                {
                    return 7;
                }
                else if (Enumerable.Range(70, 79).Contains(EXP))
                {
                    return 8;
                }
                else if (Enumerable.Range(80, 89).Contains(EXP))
                {
                    return 9;
                }
                else if (Enumerable.Range(90, 109).Contains(EXP))
                {
                    return 10;
                }
                else if (Enumerable.Range(110, 134).Contains(EXP))
                {
                    return 11;
                }
                else if (Enumerable.Range(135, 159).Contains(EXP))
                {
                    return 12;
                }
                else if (Enumerable.Range(160, 189).Contains(EXP))
                {
                    return 13;
                }
                else if (Enumerable.Range(190, 219).Contains(EXP))
                {
                    return 14;
                }
                else if (Enumerable.Range(220, 249).Contains(EXP))
                {
                    return 15;
                }
                else if (Enumerable.Range(250, 284).Contains(EXP))
                {
                    return 16;
                }
                else if (Enumerable.Range(285, 319).Contains(EXP))
                {
                    return 17;
                }
                else if (Enumerable.Range(320, 359).Contains(EXP))
                {
                    return 18;
                }
                else if (Enumerable.Range(360, 399).Contains(EXP))
                {
                    return 19;
                }
                else if (Enumerable.Range(400, 459).Contains(EXP))
                {
                    return 20;
                }
                /* Level 21 - 40 (EXP 460 - 2354) */
                else if (Enumerable.Range(460, 529).Contains(EXP))
                {
                    return 21;
                }
                else if (Enumerable.Range(530, 599).Contains(EXP))
                {
                    return 22;
                }
                else if (Enumerable.Range(600, 669).Contains(EXP))
                {
                    return 23;
                }
                else if (Enumerable.Range(670, 744).Contains(EXP))
                {
                    return 24;
                }
                else if (Enumerable.Range(745, 819).Contains(EXP))
                {
                    return 25;
                }
                else if (Enumerable.Range(820, 899).Contains(EXP))
                {
                    return 26;
                }
                else if (Enumerable.Range(900, 989).Contains(EXP))
                {
                    return 27;
                }
                else if (Enumerable.Range(990, 1074).Contains(EXP))
                {
                    return 28;
                }
                else if (Enumerable.Range(1075, 1164).Contains(EXP))
                {
                    return 29;
                }
                else if (Enumerable.Range(1165, 1259).Contains(EXP))
                {
                    return 30;
                }
                else if (Enumerable.Range(1260, 1354).Contains(EXP))
                {
                    return 31;
                }
                else if (Enumerable.Range(1355, 1454).Contains(EXP))
                {
                    return 32;
                }
                else if (Enumerable.Range(1455, 1554).Contains(EXP))
                {
                    return 33;
                }
                else if (Enumerable.Range(1555, 1659).Contains(EXP))
                {
                    return 34;
                }
                else if (Enumerable.Range(1660, 1769).Contains(EXP))
                {
                    return 35;
                }
                else if (Enumerable.Range(1770, 1879).Contains(EXP))
                {
                    return 36;
                }
                else if (Enumerable.Range(1880, 1994).Contains(EXP))
                {
                    return 37;
                }
                else if (Enumerable.Range(1995, 2109).Contains(EXP))
                {
                    return 38;
                }
                else if (Enumerable.Range(2110, 2229).Contains(EXP))
                {
                    return 39;
                }
                else if (Enumerable.Range(2230, 2354).Contains(EXP))
                {
                    return 40;
                }
                /* Level 41 - 60 (EXP 2355 - 0) */
                else if (Enumerable.Range(2355, 2479).Contains(EXP))
                {
                    return 41;
                }
                else if (Enumerable.Range(2480, 2609).Contains(EXP))
                {
                    return 42;
                }
                else if (Enumerable.Range(2610, 2739).Contains(EXP))
                {
                    return 43;
                }
                else if (Enumerable.Range(2740, 2874).Contains(EXP))
                {
                    return 44;
                }
                else if (Enumerable.Range(2875, 3014).Contains(EXP))
                {
                    return 45;
                }
                else if (Enumerable.Range(3015, 3154).Contains(EXP))
                {
                    return 46;
                }
                else if (Enumerable.Range(3155, 3299).Contains(EXP))
                {
                    return 47;
                }
                else if (Enumerable.Range(3300, 3444).Contains(EXP))
                {
                    return 48;
                }
                else if (Enumerable.Range(3445, 3644).Contains(EXP))
                {
                    return 49;
                }
                else if (Enumerable.Range(3645, 3849).Contains(EXP))
                {
                    return 50;
                }
                else if (Enumerable.Range(3850, 4059).Contains(EXP))
                {
                    return 51;
                }
                else if (Enumerable.Range(4060, 4169).Contains(EXP))
                {
                    return 52;
                }
                else if (Enumerable.Range(4270, 4484).Contains(EXP))
                {
                    return 53;
                }
                else if (Enumerable.Range(4485, 4704).Contains(EXP))
                {
                    return 54;
                }
                else if (Enumerable.Range(4705, 4929).Contains(EXP))
                {
                    return 55;
                }
                else if (Enumerable.Range(4930, 5159).Contains(EXP))
                {
                    return 56;
                }
                else if (Enumerable.Range(5160, 5389).Contains(EXP))
                {
                    return 57;
                }
                else if (Enumerable.Range(5390, 5624).Contains(EXP))
                {
                    return 58;
                }
                else if (Enumerable.Range(5625, 5864).Contains(EXP))
                {
                    return 59;
                }
                else if (Enumerable.Range(5865, 6109).Contains(EXP))
                {
                    return 60;
                }
                /* Level 61 - 80 (EXP 0 - 0) */
                else if (Enumerable.Range(6110, 6359).Contains(EXP))
                {
                    return 61;
                }
                else if (Enumerable.Range(6360, 6609).Contains(EXP))
                {
                    return 62;
                }
                else if (Enumerable.Range(6610, 6864).Contains(EXP))
                {
                    return 63;
                }
                else if (Enumerable.Range(6865, 7124).Contains(EXP))
                {
                    return 64;
                }
                else if (Enumerable.Range(7125, 7389).Contains(EXP))
                {
                    return 65;
                }
                else if (Enumerable.Range(7390, 7659).Contains(EXP))
                {
                    return 66;
                }
                else if (Enumerable.Range(7660, 7924).Contains(EXP))
                {
                    return 67;
                }
                else if (Enumerable.Range(7925, 8204).Contains(EXP))
                {
                    return 68;
                }
                else if (Enumerable.Range(8205, 8484).Contains(EXP))
                {
                    return 69;
                }
                else if (Enumerable.Range(8485, 8769).Contains(EXP))
                {
                    return 70;
                }
                else if (Enumerable.Range(8770, 9059).Contains(EXP))
                {
                    return 71;
                }
                else if (Enumerable.Range(9060, 9344).Contains(EXP))
                {
                    return 72;
                }
                else if (Enumerable.Range(9350, 9644).Contains(EXP))
                {
                    return 73;
                }
                else if (Enumerable.Range(9645, 9944).Contains(EXP))
                {
                    return 74;
                }
                else if (Enumerable.Range(9945, 10249).Contains(EXP))
                {
                    return 75;
                }
                else if (Enumerable.Range(10250, 10559).Contains(EXP))
                {
                    return 76;
                }
                else if (Enumerable.Range(10560, 10869).Contains(EXP))
                {
                    return 77;
                }
                else if (Enumerable.Range(10870, 11184).Contains(EXP))
                {
                    return 78;
                }
                else if (Enumerable.Range(11185, 11504).Contains(EXP))
                {
                    return 79;
                }
                else if (Enumerable.Range(11505, 11909).Contains(EXP))
                {
                    return 80;
                }
                /* Level 81 - 99 (EXP 0 - 0) */
                else if (Enumerable.Range(11910, 12319).Contains(EXP))
                {
                    return 81;
                }
                else if (Enumerable.Range(12320, 12734).Contains(EXP))
                {
                    return 82;
                }
                else if (Enumerable.Range(12735, 13154).Contains(EXP))
                {
                    return 83;
                }
                else if (Enumerable.Range(13155, 13579).Contains(EXP))
                {
                    return 84;
                }
                else if (Enumerable.Range(13580, 14009).Contains(EXP))
                {
                    return 85;
                }
                else if (Enumerable.Range(14010, 14444).Contains(EXP))
                {
                    return 86;
                }
                else if (Enumerable.Range(14445, 14884).Contains(EXP))
                {
                    return 87;
                }
                else if (Enumerable.Range(14885, 15329).Contains(EXP))
                {
                    return 88;
                }
                else if (Enumerable.Range(15330, 15779).Contains(EXP))
                {
                    return 89;
                }
                else if (Enumerable.Range(15780, 16234).Contains(EXP))
                {
                    return 90;
                }
                else if (Enumerable.Range(16235, 16694).Contains(EXP))
                {
                    return 91;
                }
                else if (Enumerable.Range(16695, 17159).Contains(EXP))
                {
                    return 92;
                }
                else if (Enumerable.Range(17160, 17629).Contains(EXP))
                {
                    return 93;
                }
                else if (Enumerable.Range(17630, 18104).Contains(EXP))
                {
                    return 94;
                }
                else if (Enumerable.Range(18105, 18584).Contains(EXP))
                {
                    return 95;
                }
                else if (Enumerable.Range(18585, 19069).Contains(EXP))
                {
                    return 96;
                }
                else if (Enumerable.Range(19070, 19559).Contains(EXP))
                {
                    return 97;
                }
                else if (Enumerable.Range(19560, 20054).Contains(EXP))
                {
                    return 98;
                }
                else if (Enumerable.Range(20055, 20554).Contains(EXP))
                {
                    return 99;
                }

                // EXP over 20555... Pokemon is Level 100
                else if (EXP >= 20555)
                {
                    return 100;
                }

                // less than 0... Default to level 0
                return 0;
            }
        }
        #endregion
    }
}
