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
        public static void AddStatus(object Character, object Condition, object Data = null)
        {
            if (Character is Entities)
            {
                ((Entities)Character).AddStatus((string)Condition, Data);
            }
        }
        public static void RemoveStatus(object Character, object Condition)
        {
            if (Character is Entities)
            {
                ((Entities)Character).RemoveStatus((string)Condition);
            }
        }
        public static bool HasStatus(object Character, object Condition)
        {
            try
            {
                return ((Entities)Character).HasStatus((string)Condition);
            }
            catch { return false; }
        }
        public static object GetStatus(object Character, object Condition)
        {
            try
            {
                return ((Entities)Character).GetStatusData((string)Condition);
            }
            catch { return null; }
        }
    }
}
