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

        public void Attack_Range_Invoked(object User, List<object> Targets)
        {

        }
    }
}
