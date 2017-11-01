using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class Static_HealthChange
    {
        public Static_HealthChange()
        {
            Value = 0;
            Damage = true;
            Percentage = false;
        }

        public bool Damage { get; set; }
        public bool Percentage { get; set; }
        public int Value { get; set; }
        //public bool 
    }
}
