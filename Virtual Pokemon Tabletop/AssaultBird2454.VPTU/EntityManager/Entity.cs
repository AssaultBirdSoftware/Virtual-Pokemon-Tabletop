using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.EntitiesManager
{
    public interface Entities
    {
        string ID { get; set; }
        string Name { get; set; }
        string Notes { get; set; }

        #region Stats
        #region HP
        int Current_HP { get; set; }
        int Stat_HP_Max { get; }
        #endregion
        #region Attack
        [JsonIgnore]
        int Attack_Adjusted { get; }
        #endregion
        #region Defence
        [JsonIgnore]
        int Defence_Adjusted { get; }
        #endregion
        #region SpAttack
        [JsonIgnore]
        int SpAttack_Adjusted { get; }
        #endregion
        #region SpDefence
        [JsonIgnore]
        int SpDefence_Adjusted { get; }
        #endregion
        #region Speed
        [JsonIgnore]
        int Speed_Adjusted { get; }
        #endregion

        //ToDo: Add a call to check effects that gain a mod to evasion stats
        #region Physical Evasion
        [JsonIgnore]
        int Evasion_Physical { get; }
        #endregion
        #region Special Evasion
        [JsonIgnore]
        int Evasion_Special { get; }
        #endregion
        #region Speed Evasion
        [JsonIgnore]
        int Evasion_Speed { get; }
        #endregion
        #endregion

        /// <summary>
        /// Lists all the status afflictions that this pokemon has
        /// Key: Status Condition
        /// Value: Duration in turns (0 = Ended, -1 = No Limit)
        /// </summary>
        List<KeyValuePair<string, object>> Status { get; set; }

        void AddStatus(string Effect, object Effect_Data = null);
        object GetStatusData(string Effect);
        void SetStatusData(string Effect, object Effect_Data);
        bool HasStatus(string Effect);
        void RemoveStatus(string Effect);
    }
}
