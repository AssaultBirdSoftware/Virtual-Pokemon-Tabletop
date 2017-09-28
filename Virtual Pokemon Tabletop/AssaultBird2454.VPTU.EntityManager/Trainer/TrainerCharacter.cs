using System;
using System.Collections.Generic;
using System.Text;

namespace AssaultBird2454.VPTU.EntityManager.Trainer
{
    public class TrainerCharacter
    {
        public string Name { get; set; }

        public string Notes { get; set; }
        public string Background { get; set; }
        public Pokedex.Entity.Gender Gender { get; set; }
        public int Age { get; set; }
        public int Trainer_EXP { get; set; }
        public int Milestone { get; set; }
        public List<string> Features { get; set; }// Max 4 Classes & Unlimited for normal Features
        public Pokedex.Entity.SizeClass SizeClass { get; set; }
        public Pokedex.Entity.WeightClass WeightClass { get; set; }
        public Pokedex.Entity.Capability_Data Capabilities { get; set; }
        public Pokedex.Entity.Skill_Data Skills { get; set; }

        public int Base_HP { get; set; }
        public int Current_HP { get; set; }
        public int Base_Attack { get; set; }
        public int Base_Defence { get; set; }
        public int Base_SpAttack { get; set; }
        public int Base_SpDefence { get; set; }
        public int Base_Speed { get; set; }

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
        public int GetTrainerLevel()
        {
            return (int)Math.Floor((decimal)((Trainer_EXP / 10) + Milestone));// Returns what the characters level is
        }
        #endregion
    }
}
