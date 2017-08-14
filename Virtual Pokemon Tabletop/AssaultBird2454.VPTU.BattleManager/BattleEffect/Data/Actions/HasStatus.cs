using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class HasStatus
    {
        public HasStatus()
        {
            CheckedStatus = new List<VPTU.BattleManager.Data.Status_Afflictions>();
            HasAll = false;
        }

        public List<VPTU.BattleManager.Data.Status_Afflictions> CheckedStatus { get; set; }
        public bool HasAll { get; set; }

        /// <summary>
        /// The name of the function to invoke if the condition passes
        /// </summary>
        public string FunctionName { get; set; }
    }
}
