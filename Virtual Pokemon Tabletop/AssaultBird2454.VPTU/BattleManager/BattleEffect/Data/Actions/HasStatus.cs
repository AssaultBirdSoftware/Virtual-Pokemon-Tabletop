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
            Status = VPTU.BattleManager.Data.Status_Afflictions.Burned;
            Effected_State = true;
        }

        public VPTU.BattleManager.Data.Status_Afflictions Status { get; set; }
        public bool Effected_State { get; set; }
    }
}
