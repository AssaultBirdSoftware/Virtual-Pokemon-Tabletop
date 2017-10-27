using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class AddStatusEffect
    {
        public AddStatusEffect()
        {
            StatusEffect = VPTU.BattleManager.Data.Status_Afflictions.Burned;
            //StatusExpiry_Method = null;
            StatusExpiry_Time = 5;
            StatusEffect_Force = false;
            StatusEffect_Persistant = false;
        }

        public VPTU.BattleManager.Data.Status_Afflictions StatusEffect { get; set; }
        //public object StatusExpiry_Method { get; set; }
        public int StatusExpiry_Time { get; set; }
        public bool StatusEffect_Force { get; set; }
        public bool StatusEffect_Persistant { get; set; }
    }
}
