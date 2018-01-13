using System.Collections.Generic;
using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class HasStatus
    {
        public HasStatus()
        {
            CheckedStatus = new List<Status_Afflictions>();
            HasAll = false;
        }

        public List<Status_Afflictions> CheckedStatus { get; set; }
        public bool HasAll { get; set; }

        /// <summary>
        ///     The name of the function to invoke if the condition passes
        /// </summary>
        public string FunctionName { get; set; }
    }
}