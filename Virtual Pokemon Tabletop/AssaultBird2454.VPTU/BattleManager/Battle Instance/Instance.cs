using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Battle_Instance
{
    public class Instance
    {
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
