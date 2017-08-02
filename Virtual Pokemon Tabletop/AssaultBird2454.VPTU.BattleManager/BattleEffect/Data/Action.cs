using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public interface Action
    {
        /* Action Info */
        /// <summary>
        /// The name of the action
        /// </summary>
        string Action_Name { get; set; }
        /// <summary>
        /// The Command for executing the action
        /// </summary>
        string Action_Command { get; set; }
        /// <summary>
        /// A comment to describe the action
        /// </summary>
        string Comment { get; set; }
    }

    public class AddStatusEffect : Action
    {
        /* Action Info */
        public string Action_Name { get; set; }
        public string Action_Command { get; set; }
        public string Comment { get; set; }

        /* Effect Info */
        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
    }

    public class RemoveStatusEffect : Action
    {
        /* Action Info */
        public string Action_Name { get; set; }
        public string Action_Command { get; set; }
        public string Comment { get; set; }

        /* Effect Info */
        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
    }
}
