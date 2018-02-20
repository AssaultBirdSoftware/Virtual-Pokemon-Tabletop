using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect
{
    public class Effect_Function
    {
        public string FunctionName { get; set; }
        public string FunctionDesc { get; set; }
        public string EffectScript_ID { get; set; }

        public void Attack_AoE_Invoked(Pokedex.Moves.MoveData MoveData, object User, List<object> Targets)
        {

        }

        public void StatusAfflictions_Add(object Target, object Effect)
        {
            System.Windows.MessageBox.Show("Add");

            if(Target is EntitiesManager.Pokemon.PokemonCharacter)
            {
                Data.Status_Afflictions status = (Data.Status_Afflictions)Enum.Parse(typeof(Data.Status_Afflictions), (string)Effect);

                ((EntitiesManager.Pokemon.PokemonCharacter)Target).AddStatus(status, true);
            }
            else if((Target is EntitiesManager.Trainer.TrainerCharacter))
            {
                Data.Status_Afflictions status = (Data.Status_Afflictions)Enum.Parse(typeof(Data.Status_Afflictions), (string)Effect);

                ((EntitiesManager.Pokemon.PokemonCharacter)Target).AddStatus(status, true);
            }
        }
        public void StatusAfflictions_Remove(object Target, object Effect)
        {
            System.Windows.MessageBox.Show("Remove");

            if (Target is EntitiesManager.Pokemon.PokemonCharacter)
            {
                Data.Status_Afflictions status = (Data.Status_Afflictions)Enum.Parse(typeof(Data.Status_Afflictions), (string)Effect);

                ((EntitiesManager.Pokemon.PokemonCharacter)Target).RemoveStatus(status);
            }
            else if ((Target is EntitiesManager.Trainer.TrainerCharacter))
            {
                Data.Status_Afflictions status = (Data.Status_Afflictions)Enum.Parse(typeof(Data.Status_Afflictions), (string)Effect);

                ((EntitiesManager.Pokemon.PokemonCharacter)Target).RemoveStatus(status);
            }
        }

        public void Attack_Range_Invoked(object User, List<object> Targets)
        {

        }
    }
}
