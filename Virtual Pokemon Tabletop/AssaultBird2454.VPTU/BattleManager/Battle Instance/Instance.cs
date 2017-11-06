using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Battle_Instance
{
    public delegate void Participant_Changed(EntityManager.Entity Entity);

    public class Instance
    {
        public event Participant_Changed Participant_Added_Event;
        //public event Participant_Changed Participant_Changed_Event;
        public event Participant_Changed Participant_Removed_Event;

        private List<EntityManager.Entity> Partisipants { get; set; }

        public Instance()
        {
            // Set List
            Partisipants = new List<EntityManager.Entity>();
        }
        public Instance(List<EntityManager.Entity> _Partisipants)
        {
            // Set List
            Partisipants = _Partisipants;

            // Reset Entitys

            // Link Effect Triggers
        }

        public void AddPartisipant(EntityManager.Entity Entity)
        {
            Partisipants.Add(Entity);
            Participant_Added_Event?.Invoke(Entity);
        }

        //public void ChangeParticipant(EntityManager.Entity Entity)
        //{
        //    Participant_Changed_Event?.Invoke(Entity);
        //}

        public void RemoveParticipant(EntityManager.Entity Entity)
        {
            Partisipants.Remove(Entity);
            Participant_Removed_Event?.Invoke(Entity);
        }

        public void Reset_Entity_BattleStats(EntityManager.Entity Entity)
        {
            if (Entity is EntityManager.Pokemon.PokemonCharacter)
            {
                ((EntityManager.Pokemon.PokemonCharacter)Entity).Attack_CombatStage = 0;
                ((EntityManager.Pokemon.PokemonCharacter)Entity).Defence_CombatStage = 0;
                ((EntityManager.Pokemon.PokemonCharacter)Entity).SpAttack_CombatStage = 0;
                ((EntityManager.Pokemon.PokemonCharacter)Entity).SpDefence_CombatStage = 0;
                ((EntityManager.Pokemon.PokemonCharacter)Entity).Speed_CombatStage = 0;
            }
            else if (Entity is EntityManager.Trainer.TrainerCharacter)
            {
                
            }
            else
            {

            }
        }

        public void Begin()
        {

        }

        public void End()
        {

        }
    }
}