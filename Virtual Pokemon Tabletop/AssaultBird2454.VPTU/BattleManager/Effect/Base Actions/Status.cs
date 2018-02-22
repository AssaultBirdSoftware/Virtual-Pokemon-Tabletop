using AssaultBird2454.VPTU.EntitiesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect.Base_Actions
{
    public static class Status
    {
        public static void AddStatus(object Character, string Condition, object Data)
        {
            if(Character is Entities)
            {
                ((Entities)Character).AddStatus();
            }
        }
    }
}
