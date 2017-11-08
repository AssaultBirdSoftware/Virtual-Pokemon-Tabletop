using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.EntityManager
{
    public interface Entity
    {
        string ID { get; set; }
        string Name { get; set; }
        string Notes { get; set; }

        int Current_HP { get; set; }
        int Stat_HP_Max { get; set; }

        /// <summary>
        /// Lists all the status afflictions that this pokemon has
        /// Key: Status Condition
        /// Value: Duration in turns (0 = Ended, -1 = No Limit)
        /// </summary>
        List<KeyValuePair<BattleManager.Data.Status_Afflictions, object>> Status { get; set; }

        void AddStatus(BattleManager.Data.Status_Afflictions Effect, object Effect_Data = null);
        object GetStatusData(BattleManager.Data.Status_Afflictions Effect);
        void SetStatusData(BattleManager.Data.Status_Afflictions Effect, object Effect_Data);
        bool HasStatus(BattleManager.Data.Status_Afflictions Effect);
        void RemoveStatus(BattleManager.Data.Status_Afflictions Effect);
    }
}
