using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class RemoveStatusEffect
    {
        public RemoveStatusEffect()
        {
            StatusEffect = VPTU.BattleManager.Data.Status_Afflictions.Burned;
        }

        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
    }
}
