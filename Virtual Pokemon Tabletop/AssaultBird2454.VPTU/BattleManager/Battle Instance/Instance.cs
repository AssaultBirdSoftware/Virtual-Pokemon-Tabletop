using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Battle_Instance
{
    public delegate void Participant_Changed(EntitiesManager.Entities Entities);

    public class Instance
    {
        #region Constructors
        public Instance()
        {
            // Set List
            Participants = new List<EntitiesManager.Entities>();
            ID = VPTU.RNG.Generators.RSG.GenerateString(16);
        }
        public Instance(List<EntitiesManager.Entities> _Partisipants)
        {
            // Set List
            Participants = _Partisipants;

            // Reset Entities

            // Link Effect Triggers

            ID = VPTU.RNG.Generators.RSG.GenerateString(16);
        }
        #endregion

        #region Events
        public event Participant_Changed Participant_Added_Event;
        //public event Participant_Changed Participant_Changed_Event;
        public event Participant_Changed Participant_Removed_Event;
        #endregion

        public string ID { get; private set; }

        private List<EntitiesManager.Entities> Participants { get; set; }

        public IEnumerable<EntitiesManager.Entities> GetParticipants
        {
            get
            {
                foreach (var child in Participants) yield return child;
            }
        }

        public void AddPartisipant(EntitiesManager.Entities Entities)
        {
            Participants.Add(Entities);
            Participant_Added_Event?.Invoke(Entities);
        }

        //public void ChangeParticipant(EntitiesManager.Entities Entities)
        //{
        //    Participant_Changed_Event?.Invoke(Entities);
        //}

        public void RemoveParticipant(EntitiesManager.Entities Entities)
        {
            Participants.Remove(Entities);
            Participant_Removed_Event?.Invoke(Entities);
        }

        public void Reset_Entities_BattleStats(EntitiesManager.Entities Entities)
        {
            if (Entities is EntitiesManager.Pokemon.PokemonCharacter)
            {
                ((EntitiesManager.Pokemon.PokemonCharacter)Entities).Attack_CombatStage = 0;
                ((EntitiesManager.Pokemon.PokemonCharacter)Entities).Defence_CombatStage = 0;
                ((EntitiesManager.Pokemon.PokemonCharacter)Entities).SpAttack_CombatStage = 0;
                ((EntitiesManager.Pokemon.PokemonCharacter)Entities).SpDefence_CombatStage = 0;
                ((EntitiesManager.Pokemon.PokemonCharacter)Entities).Speed_CombatStage = 0;
            }
            else if (Entities is EntitiesManager.Trainer.TrainerCharacter)
            {

            }
            else
            {

            }
        }

        public void TurnOrder_Next()
        {

        }

        public void TurnOrder_Prev()
        {

        }

        public void Begin()
        {

        }

        public void End()
        {

        }
    }
}