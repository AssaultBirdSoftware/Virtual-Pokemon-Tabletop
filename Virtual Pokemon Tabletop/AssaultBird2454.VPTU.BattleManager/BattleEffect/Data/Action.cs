using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public class Action
    {
        public Action(string Name, string Command, string Comment, dynamic Data = null)
        {
            Action_Name = Name;
            Action_Command = Command;
            Action_Comment = Comment;

            if(Data != null)
            {
                Action_Data = Data;
            }
        }

        /* Action Info */
        /// <summary>
        /// The name of the action
        /// </summary>
        public string Action_Name { get; set; }
        /// <summary>
        /// The Command for executing the action
        /// </summary>
        public string Action_Command { get; }
        /// <summary>
        /// A comment to describe the action
        /// </summary>
        public string Action_Comment { get; set; }

        public dynamic Action_Data { get; set; }
    }

    public class AddStatusEffect
    {
        public AddStatusEffect()
        {
            StatusEffect = VPTU.BattleManager.Data.Status_Afflictions.Burned;
        }

        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
    }

    public class RemoveStatusEffect
    {
        public RemoveStatusEffect()
        {
            StatusEffect = VPTU.BattleManager.Data.Status_Afflictions.Burned;
        }

        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
    }
}
