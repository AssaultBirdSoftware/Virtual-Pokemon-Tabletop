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
        int HP_SpeciesBase { get; set; }
        int HP_BaseMod { get; set; }
        int HP_Base { get; }
        int HP_AddStat { get; set; }
        int Stat_HP_Max { get; }
        #endregion
        #region Attack
        int Attack_SpeciesBase { get; set; }
        int Attack_BaseMod { get; set; }
        int Attack_Base { get; }
        int Attack_AddStat { get; set; }
        int Attack_Total { get; }
        int Attack_CombatStage { get; set; }
        int Attack_Adjusted { get; }
        #endregion
        #region Defence
        int Defence_SpeciesBase { get; set; }
        int Defence_BaseMod { get; set; }
        int Defence_Base { get; }
        int Defence_AddStat { get; set; }
        int Defence_Total { get; }
        int Defence_CombatStage { get; set; }
        int Defence_Adjusted { get; }
        #endregion
        #region SpAttack
        int SpAttack_SpeciesBase { get; set; }
        int SpAttack_BaseMod { get; set; }
        int SpAttack_Base { get; }
        int SpAttack_AddStat { get; set; }
        int SpAttack_Total { get; }
        int SpAttack_CombatStage { get; set; }
        int SpAttack_Adjusted { get; }
        #endregion
        #region SpDefence
        int SpDefence_SpeciesBase { get; set; }
        int SpDefence_BaseMod { get; set; }
        int SpDefence_Base { get; }
        int SpDefence_AddStat { get; set; }
        int SpDefence_Total { get; }
        int SpDefence_CombatStage { get; set; }
        int SpDefence_Adjusted { get; set; }
        #endregion
        #region Speed
        int Speed_SpeciesBase { get; set; }
        int Speed_BaseMod { get; set; }
        int Speed_Base { get; }
        int Speed_AddStat { get; set; }
        int Speed_Total { get; }
        int Speed_CombatStage { get; set; }
        int Speed_Adjusted { get; }
        #endregion

        //ToDo: Add a call to check effects that gain a mod to evasion stats
        #region Physical Evasion
        int Evasion_Physical_Mod { get; set; }
        int Evasion_Physical { get; }
        #endregion
        #region Special Evasion
        int Evasion_Special_Mod { get; set; }
        int Evasion_Special { get; }
        #endregion
        #region Speed Evasion
        int Evasion_Speed_Mod { get; set; }
        int Evasion_Speed { get; }
        #endregion
        #endregion

        #region Status Conditions
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
        #endregion
    }
}
