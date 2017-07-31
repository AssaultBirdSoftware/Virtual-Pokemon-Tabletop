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
        /// Lists all the triggers that will trigger this effect
        /// </summary>
        public List<Data.Triggers> Triggers { get; set; }

        /// <summary>
        /// Lists all the variables that this object will have
        /// </summary>
        public List<Data.Variables> Variables { get; set; }

        /// <summary>
        /// All the actions that this effect will execute for this effect
        /// </summary>
        public List<Data.Action> Actions { get; set; }
    }
}
