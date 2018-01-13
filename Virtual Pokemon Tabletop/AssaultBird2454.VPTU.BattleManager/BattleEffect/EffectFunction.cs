using System.Collections.Generic;
using AssaultBird2454.VPTU.BattleManager.BattleEffect.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect
{
    public class EffectFunction
    {
        /// <summary>
        ///     Functions Name
        /// </summary>
        public string Function_Name { get; set; }

        /// <summary>
        ///     A Description for what this function does
        /// </summary>
        public string Function_Comment { get; set; }

        /// <summary>
        ///     Lists all the triggers that will trigger this function
        /// </summary>
        public List<Triggers> Triggers { get; set; }

        /// <summary>
        ///     List of Actions to take to perform this function
        /// </summary>
        public List<Action> Actions { get; set; }
    }
}