using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssaultBird2454.VPTU.EntitiesManager.Trainer
{
    public class TrainerCharacter : Entities, Entry
    {
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
                    Entities_Type = Entities_Type.Trainer,
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
        public string Background { get; set; }
        public Pokedex.Entities.Gender Gender { get; set; }
        public int Age { get; set; }
        public int Trainer_EXP { get; set; }
        public int Milestone { get; set; }
        public List<string> Features { get; set; }// Max 4 Classes & Unlimited for normal Features
        public Pokedex.Entities.SizeClass SizeClass { get; set; }
        public Pokedex.Entities.WeightClass WeightClass { get; set; }
        public Pokedex.Entities.Capability_Data Capabilities { get; set; }
        public Pokedex.Entities.Skill_Data Skills { get; set; }
        public int Current_HP { get; set; }

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
                return Defence_SpeciesBase + Defence_BaseMod;
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
                return SpAttack_SpeciesBase + SpAttack_BaseMod;
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
                return SpDefence_SpeciesBase + SpDefence_BaseMod;
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
                return Speed_SpeciesBase + Speed_BaseMod;
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

        public List<Pokemon.PokemonCharacter> PartyPokemon { get; set; }

        #region Methods
        /// <summary>
        /// Gets the characters level based on a formula
        /// </summary>
        /// <returns>Level</returns>
        public int Level
        {
            get
            {
                return (int)Math.Floor((decimal)((Trainer_EXP / 10) + Milestone));// Returns what the characters level is
            }
        }
        #endregion
    }
}
