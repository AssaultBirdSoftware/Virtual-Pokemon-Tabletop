using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect
{
    public class EffectFunction
    {
        /// <summary>
        /// Functions Name
        /// </summary>
        public string Function_Name { get; set; }
        /// <summary>
        /// A Description for what this function does
        /// </summary>
        public string Function_Comment { get; set; }

        /// <summary>
        /// List of Actions to take to perform this function
        /// </summary>
        public List<Data.Action> Actions { get; set; }
    }
}
