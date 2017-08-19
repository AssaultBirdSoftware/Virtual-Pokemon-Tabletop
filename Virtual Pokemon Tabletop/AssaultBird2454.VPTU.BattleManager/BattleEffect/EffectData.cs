using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect
{
    public class EffectData
    {
        /// <summary>
        /// Lists all the variables that this object will have
        /// </summary>
        public List<Data.Variable> Variables { get; set; }

        /// <summary>
        /// All the actions that this effect will execute for this effect
        /// </summary>
        public List<EffectFunction> Functions { get; set; }
    }
}
