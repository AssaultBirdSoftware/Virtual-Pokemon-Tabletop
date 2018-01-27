using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.EntitiesManager.Pokemon
{
    public class PokemonCharacter : Entities, Entry
    {
        public PokemonCharacter(string _ID)
        {
            ID = _ID;

            View = new List<string>();
            Edit = new List<string>();
            PokemonType = new List<BattleManager.Data.Type>();
            Abilitys = new List<string>();
            Moves = new List<string>();
            Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();
        }
        public PokemonCharacter()
        {

        }

        [JsonIgnore]
        public Entry_Data EntryData
        {
            get
            {
                return new Entry_Data()
                {
                    ID = ID,
                    Name = Name,
                    Parent_Folder = Parent_Folder,
                    Token_ResourceID = Token_ResourceID,
                    Entities_Type = Entities_Type.Pokemon,
                    View = View,
                    Edit = Edit
                };
            }
        }

        /// <summary>
        /// List of users with View Permission
        /// </summary>
        public List<string> View { get; set; }
        /// <summary>
        /// List of users with Edit Permission
        /// </summary>
        public List<string> Edit { get; set; }

        /// <summary>
        /// The resource ID for this entry
        /// </summary>
        public string Token_ResourceID { get; set; }
        /// <summary>
        /// The folder that this entry is placed in (null = root)
        /// </summary>
        public string Parent_Folder { get; set; }

        [JsonIgnore]
        public Entities_Type Entities_Type { get { return Entities_Type.Pokemon; } }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public decimal Species_DexID { get; set; }
        public List<VPTU.BattleManager.Data.Type> PokemonType { get; set; }
        [JsonIgnore]
        public string TypeString
        {
            get
            {
                int i = 0;
                string s = "";
                foreach (BattleManager.Data.Type type in PokemonType)
                {
                    if (i != 0)
                        s = s + ", ";
                    s = s + type.ToString();
                    i++;
                }

                return s;
            }
        }
        public int EXP { get; set; }
        [JsonIgnore]
        public int Next_EXP_Requirement
        {
            get
            {
                return (EXP_Markers(Level + 1));
            }
        }
        [JsonIgnore]
        public int Prev_EXP_Requirement
        {
            get
            {
                return EXP_Markers(Level);
            }
        }
        public string Held_Item { get; set; }
        public int Loyalty { get; set; }
        public Pokedex.Entities.Gender Gender { get; set; }
        public BattleManager.Data.Nature Nature { get; set; }
        public List<string> Abilitys { get; set; }
        public Pokedex.Entities.SizeClass SizeClass { get; set; }
        public Pokedex.Entities.WeightClass WeightClass { get; set; }
        public Pokedex.Entities.Capability_Data Capabilities { get; set; }
        public Pokedex.Entities.Skill_Data Skills { get; set; }

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
        [JsonIgnore]
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

        #region Stat Max HP
        [JsonIgnore]
        public int Stat_HP_Max
        {
            get
            {
                return (Level + (HP_Base + HP_AddStat * 3) + 10);
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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

        //ToDo: Add a call to check effects that gain a mod to evasion stats
        #region Physical Evasion
        private int Stat_Physical_Evasion_Mod;
        public int Evasion_Physical_Mod
        {
            get
            {
                return Stat_Physical_Evasion_Mod;
            }
            set
            {
                Stat_Physical_Evasion_Mod = value;
            }
        }

        [JsonIgnore]
        public int Evasion_Physical
        {
            get
            {
                int stat = (int)Math.Floor((decimal)Defence_Total / 5);

                if (stat >= 6)
                {
                    return 6 + Evasion_Physical_Mod;
                }
                else
                {

                    return stat + Evasion_Physical_Mod;
                }
            }
        }
        #endregion
        #region Special Evasion
        private int Stat_Special_Evasion_Mod;
        public int Evasion_Special_Mod
        {
            get
            {
                return Stat_Special_Evasion_Mod;
            }
            set
            {
                Stat_Special_Evasion_Mod = value;
            }
        }

        [JsonIgnore]
        public int Evasion_Special
        {
            get
            {
                int stat = (int)Math.Floor((decimal)SpDefence_Total / 5);

                if (stat >= 6)
                {
                    return 6 + Evasion_Special_Mod;
                }
                else
                {

                    return stat + Evasion_Special_Mod;
                }
            }
        }
        #endregion
        #region Speed Evasion
        private int Stat_Speed_Evasion_Mod;
        public int Evasion_Speed_Mod
        {
            get
            {
                return Stat_Speed_Evasion_Mod;
            }
            set
            {
                Stat_Speed_Evasion_Mod = value;
            }
        }

        [JsonIgnore]
        public int Evasion_Speed
        {
            get
            {
                int stat = (int)Math.Floor((decimal)Speed_Total / 5);

                if (stat >= 6)
                {
                    return 6 + Evasion_Speed_Mod;
                }
                else
                {

                    return stat + Evasion_Speed_Mod;
                }
            }
        }
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
        /// Lists all the status afflictions that this pokemon has
        /// Key: Status Condition
        /// Value: Duration in turns (0 = Ended, -1 = No Limit)
        /// </summary>
        public List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>> Status { get; set; }

        public void AddStatus(BattleManager.Data.Status_Afflictions Effect, object Effect_Data = null)
        {
            if (Status == null)
                Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();

            KeyValuePair<BattleManager.Data.Status_Afflictions, object> Data = new KeyValuePair<BattleManager.Data.Status_Afflictions, object>(Effect, Effect_Data);
            if (HasStatus(Effect))
            {
                RemoveStatus(Effect);
                Status.Add(Data);
            }
            else
            {
                Status.Add(Data);
            }
        }
        public object GetStatusData(BattleManager.Data.Status_Afflictions Effect)
        {
            if (Status == null)
                Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();

            return Status.Find(x => x.Key == Effect).Value;
        }
        public void SetStatusData(BattleManager.Data.Status_Afflictions Effect, object Effect_Data)
        {
            if (Status == null)
                Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();

            if (HasStatus(Effect))
            {
                RemoveStatus(Effect);
                AddStatus(Effect, Effect_Data);
            }
        }
        public bool HasStatus(BattleManager.Data.Status_Afflictions Effect)
        {
            if (Status == null)
                Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();

            if (Status.FindAll(x => x.Key == Effect).Count >= 1) { return true; } else { return false; }
        }
        public void RemoveStatus(BattleManager.Data.Status_Afflictions Effect)
        {
            if (Status == null)
                Status = new List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>>();

            Status.RemoveAll(x => x.Key == Effect);
        }

        #region Functions
        [JsonIgnore]
        public int Level
        {
            get
            {
                /* Level 1 - 20 (EXP 0 - 459) */
                if (Between(0, 9, EXP))
                {
                    return 1;
                }
                else if (Between(10, 19, EXP))
                {
                    return 2;
                }
                else if (Between(20, 29, EXP))
                {
                    return 3;
                }
                else if (Between(30, 39, EXP))
                {
                    return 4;
                }
                else if (Between(40, 49, EXP))
                {
                    return 5;
                }
                else if (Between(50, 59, EXP))
                {
                    return 6;
                }
                else if (Between(60, 69, EXP))
                {
                    return 7;
                }
                else if (Between(70, 79, EXP))
                {
                    return 8;
                }
                else if (Between(80, 89, EXP))
                {
                    return 9;
                }
                else if (Between(90, 109, EXP))
                {
                    return 10;
                }
                else if (Between(110, 134, EXP))
                {
                    return 11;
                }
                else if (Between(135, 159, EXP))
                {
                    return 12;
                }
                else if (Between(160, 189, EXP))
                {
                    return 13;
                }
                else if (Between(190, 219, EXP))
                {
                    return 14;
                }
                else if (Between(220, 249, EXP))
                {
                    return 15;
                }
                else if (Between(250, 284, EXP))
                {
                    return 16;
                }
                else if (Between(285, 319, EXP))
                {
                    return 17;
                }
                else if (Between(320, 359, EXP))
                {
                    return 18;
                }
                else if (Between(360, 399, EXP))
                {
                    return 19;
                }
                else if (Between(400, 459, EXP))
                {
                    return 20;
                }
                /* Level 21 - 40 (EXP 460 - 2354) */
                else if (Between(460, 529, EXP))
                {
                    return 21;
                }
                else if (Between(530, 599, EXP))
                {
                    return 22;
                }
                else if (Between(600, 669, EXP))
                {
                    return 23;
                }
                else if (Between(670, 744, EXP))
                {
                    return 24;
                }
                else if (Between(745, 819, EXP))
                {
                    return 25;
                }
                else if (Between(820, 899, EXP))
                {
                    return 26;
                }
                else if (Between(900, 989, EXP))
                {
                    return 27;
                }
                else if (Between(990, 1074, EXP))
                {
                    return 28;
                }
                else if (Between(1075, 1164, EXP))
                {
                    return 29;
                }
                else if (Between(1165, 1259, EXP))
                {
                    return 30;
                }
                else if (Between(1260, 1354, EXP))
                {
                    return 31;
                }
                else if (Between(1355, 1454, EXP))
                {
                    return 32;
                }
                else if (Between(1455, 1554, EXP))
                {
                    return 33;
                }
                else if (Between(1555, 1659, EXP))
                {
                    return 34;
                }
                else if (Between(1660, 1769, EXP))
                {
                    return 35;
                }
                else if (Between(1770, 1879, EXP))
                {
                    return 36;
                }
                else if (Between(1880, 1994, EXP))
                {
                    return 37;
                }
                else if (Between(1995, 2109, EXP))
                {
                    return 38;
                }
                else if (Between(2110, 2229, EXP))
                {
                    return 39;
                }
                else if (Between(2230, 2354, EXP))
                {
                    return 40;
                }
                /* Level 41 - 60 (EXP 2355 - 0) */
                else if (Between(2355, 2479, EXP))
                {
                    return 41;
                }
                else if (Between(2480, 2609, EXP))
                {
                    return 42;
                }
                else if (Between(2610, 2739, EXP))
                {
                    return 43;
                }
                else if (Between(2740, 2874, EXP))
                {
                    return 44;
                }
                else if (Between(2875, 3014, EXP))
                {
                    return 45;
                }
                else if (Between(3015, 3154, EXP))
                {
                    return 46;
                }
                else if (Between(3155, 3299, EXP))
                {
                    return 47;
                }
                else if (Between(3300, 3444, EXP))
                {
                    return 48;
                }
                else if (Between(3445, 3644, EXP))
                {
                    return 49;
                }
                else if (Between(3645, 3849, EXP))
                {
                    return 50;
                }
                else if (Between(3850, 4059, EXP))
                {
                    return 51;
                }
                else if (Between(4060, 4169, EXP))
                {
                    return 52;
                }
                else if (Between(4270, 4484, EXP))
                {
                    return 53;
                }
                else if (Between(4485, 4704, EXP))
                {
                    return 54;
                }
                else if (Between(4705, 4929, EXP))
                {
                    return 55;
                }
                else if (Between(4930, 5159, EXP))
                {
                    return 56;
                }
                else if (Between(5160, 5389, EXP))
                {
                    return 57;
                }
                else if (Between(5390, 5624, EXP))
                {
                    return 58;
                }
                else if (Between(5625, 5864, EXP))
                {
                    return 59;
                }
                else if (Between(5865, 6109, EXP))
                {
                    return 60;
                }
                /* Level 61 - 80 (EXP 0 - 0) */
                else if (Between(6110, 6359, EXP))
                {
                    return 61;
                }
                else if (Between(6360, 6609, EXP))
                {
                    return 62;
                }
                else if (Between(6610, 6864, EXP))
                {
                    return 63;
                }
                else if (Between(6865, 7124, EXP))
                {
                    return 64;
                }
                else if (Between(7125, 7389, EXP))
                {
                    return 65;
                }
                else if (Between(7390, 7659, EXP))
                {
                    return 66;
                }
                else if (Between(7660, 7924, EXP))
                {
                    return 67;
                }
                else if (Between(7925, 8204, EXP))
                {
                    return 68;
                }
                else if (Between(8205, 8484, EXP))
                {
                    return 69;
                }
                else if (Between(8485, 8769, EXP))
                {
                    return 70;
                }
                else if (Between(8770, 9059, EXP))
                {
                    return 71;
                }
                else if (Between(9060, 9344, EXP))
                {
                    return 72;
                }
                else if (Between(9350, 9644, EXP))
                {
                    return 73;
                }
                else if (Between(9645, 9944, EXP))
                {
                    return 74;
                }
                else if (Between(9945, 10249, EXP))
                {
                    return 75;
                }
                else if (Between(10250, 10559, EXP))
                {
                    return 76;
                }
                else if (Between(10560, 10869, EXP))
                {
                    return 77;
                }
                else if (Between(10870, 11184, EXP))
                {
                    return 78;
                }
                else if (Between(11185, 11504, EXP))
                {
                    return 79;
                }
                else if (Between(11505, 11909, EXP))
                {
                    return 80;
                }
                /* Level 81 - 99 (EXP 0 - 0) */
                else if (Between(11910, 12319, EXP))
                {
                    return 81;
                }
                else if (Between(12320, 12734, EXP))
                {
                    return 82;
                }
                else if (Between(12735, 13154, EXP))
                {
                    return 83;
                }
                else if (Between(13155, 13579, EXP))
                {
                    return 84;
                }
                else if (Between(13580, 14009, EXP))
                {
                    return 85;
                }
                else if (Between(14010, 14444, EXP))
                {
                    return 86;
                }
                else if (Between(14445, 14884, EXP))
                {
                    return 87;
                }
                else if (Between(14885, 15329, EXP))
                {
                    return 88;
                }
                else if (Between(15330, 15779, EXP))
                {
                    return 89;
                }
                else if (Between(15780, 16234, EXP))
                {
                    return 90;
                }
                else if (Between(16235, 16694, EXP))
                {
                    return 91;
                }
                else if (Between(16695, 17159, EXP))
                {
                    return 92;
                }
                else if (Between(17160, 17629, EXP))
                {
                    return 93;
                }
                else if (Between(17630, 18104, EXP))
                {
                    return 94;
                }
                else if (Between(18105, 18584, EXP))
                {
                    return 95;
                }
                else if (Between(18585, 19069, EXP))
                {
                    return 96;
                }
                else if (Between(19070, 19559, EXP))
                {
                    return 97;
                }
                else if (Between(19560, 20054, EXP))
                {
                    return 98;
                }
                else if (Between(20055, 20554, EXP))
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

        public static int EXP_Markers(int _Level)
        {
            if (_Level == 1)
            {
                return 0;
            }
            else if (_Level == 2)
            {
                return 10;
            }
            else if (_Level == 3)
            {
                return 20;
            }
            else if (_Level == 4)
            {
                return 30;
            }
            else if (_Level == 5)
            {
                return 40;
            }
            else if (_Level == 6)
            {
                return 50;
            }
            else if (_Level == 7)
            {
                return 60;
            }
            else if (_Level == 8)
            {
                return 70;
            }
            else if (_Level == 9)
            {
                return 80;
            }
            else if (_Level == 10)
            {
                return 90;
            }
            else if (_Level == 11)
            {
                return 110;
            }
            else if (_Level == 12)
            {
                return 135;
            }
            else if (_Level == 13)
            {
                return 160;
            }
            else if (_Level == 14)
            {
                return 190;
            }
            else if (_Level == 15)
            {
                return 220;
            }
            else if (_Level == 16)
            {
                return 250;
            }
            else if (_Level == 17)
            {
                return 285;
            }
            else if (_Level == 18)
            {
                return 320;
            }
            else if (_Level == 19)
            {
                return 360;
            }
            else if (_Level == 20)
            {
                return 400;
            }
            else if (_Level == 21)
            {
                return 460;
            }
            else if (_Level == 22)
            {
                return 530;
            }
            else if (_Level == 23)
            {
                return 600;
            }
            else if (_Level == 24)
            {
                return 670;
            }
            else if (_Level == 25)
            {
                return 745;
            }
            else if (_Level == 26)
            {
                return 820;
            }
            else if (_Level == 27)
            {
                return 900;
            }
            else if (_Level == 28)
            {
                return 990;
            }
            else if (_Level == 29)
            {
                return 1075;
            }
            else if (_Level == 30)
            {
                return 1165;
            }
            else if (_Level == 31)
            {
                return 1260;
            }
            else if (_Level == 32)
            {
                return 1355;
            }
            else if (_Level == 33)
            {
                return 1455;
            }
            else if (_Level == 34)
            {
                return 1555;
            }
            else if (_Level == 35)
            {
                return 1660;
            }
            else if (_Level == 36)
            {
                return 1770;
            }
            else if (_Level == 37)
            {
                return 1880;
            }
            else if (_Level == 38)
            {
                return 1995;
            }
            else if (_Level == 39)
            {
                return 2110;
            }
            else if (_Level == 40)
            {
                return 2230;
            }
            else if (_Level == 41)
            {
                return 2355;
            }
            else if (_Level == 42)
            {
                return 2480;
            }
            else if (_Level == 43)
            {
                return 2610;
            }
            else if (_Level == 44)
            {
                return 2740;
            }
            else if (_Level == 45)
            {
                return 2875;
            }
            else if (_Level == 46)
            {
                return 3015;
            }
            else if (_Level == 47)
            {
                return 3155;
            }
            else if (_Level == 48)
            {
                return 3300;
            }
            else if (_Level == 49)
            {
                return 3445;
            }
            else if (_Level == 50)
            {
                return 3645;
            }
            else if (_Level == 51)
            {
                return 3850;
            }
            else if (_Level == 52)
            {
                return 4060;
            }
            else if (_Level == 53)
            {
                return 4270;
            }
            else if (_Level == 54)
            {
                return 4485;
            }
            else if (_Level == 55)
            {
                return 4705;
            }
            else if (_Level == 56)
            {
                return 4930;
            }
            else if (_Level == 57)
            {
                return 5160;
            }
            else if (_Level == 58)
            {
                return 5390;
            }
            else if (_Level == 59)
            {
                return 5625;
            }
            else if (_Level == 60)
            {
                return 5865;
            }
            else if (_Level == 61)
            {
                return 6110;
            }
            else if (_Level == 62)
            {
                return 6360;
            }
            else if (_Level == 63)
            {
                return 6610;
            }
            else if (_Level == 64)
            {
                return 6865;
            }
            else if (_Level == 65)
            {
                return 7125;
            }
            else if (_Level == 66)
            {
                return 7390;
            }
            else if (_Level == 67)
            {
                return 7660;
            }
            else if (_Level == 68)
            {
                return 7925;
            }
            else if (_Level == 69)
            {
                return 8205;
            }
            else if (_Level == 70)
            {
                return 8485;
            }
            else if (_Level == 71)
            {
                return 8770;
            }
            else if (_Level == 72)
            {
                return 9060;
            }
            else if (_Level == 73)
            {
                return 9350;
            }
            else if (_Level == 74)
            {
                return 9645;
            }
            else if (_Level == 75)
            {
                return 9945;
            }
            else if (_Level == 76)
            {
                return 10250;
            }
            else if (_Level == 77)
            {
                return 10560;
            }
            else if (_Level == 78)
            {
                return 10870;
            }
            else if (_Level == 79)
            {
                return 11185;
            }
            else if (_Level == 80)
            {
                return 11505;
            }
            else if (_Level == 81)
            {
                return 11910;
            }
            else if (_Level == 82)
            {
                return 12320;
            }
            else if (_Level == 83)
            {
                return 12735;
            }
            else if (_Level == 84)
            {
                return 13155;
            }
            else if (_Level == 85)
            {
                return 13580;
            }
            else if (_Level == 86)
            {
                return 14010;
            }
            else if (_Level == 87)
            {
                return 14445;
            }
            else if (_Level == 88)
            {
                return 14885;
            }
            else if (_Level == 89)
            {
                return 15330;
            }
            else if (_Level == 90)
            {
                return 15780;
            }
            else if (_Level == 91)
            {
                return 16235;
            }
            else if (_Level == 92)
            {
                return 16695;
            }
            else if (_Level == 93)
            {
                return 17160;
            }
            else if (_Level == 94)
            {
                return 17630;
            }
            else if (_Level == 95)
            {
                return 18105;
            }
            else if (_Level == 96)
            {
                return 18585;
            }
            else if (_Level == 97)
            {
                return 19070;
            }
            else if (_Level == 98)
            {
                return 19560;
            }
            else if (_Level == 99)
            {
                return 20055;
            }
            else if (_Level == 100)
            {
                return 20555;
            }
            else
            {
                return 0;
            }
        }

        private static bool Between(int Low, int High, int Val)
        {
            if (Val >= Low && Val <= High)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
