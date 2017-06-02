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
        /// All the conditions that need to pass to execute the actions
        /// </summary>
        public List<Data.Conditions> Conditions { get; set; }
        /// <summary>
        /// Defines if all the Conditions need to pass or a mininum of one to execute the actions
        /// </summary>
        public bool Conditions_All { get; set; }

        /// <summary>
        /// All the actions that this effect will execute for this effect
        /// </summary>
        public List<Data.Actions> Actions { get; set; }
    }
}
