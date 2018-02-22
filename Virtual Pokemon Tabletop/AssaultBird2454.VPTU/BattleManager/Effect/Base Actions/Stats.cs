using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect.Base_Actions
{
    public static class Stats
    {
        #region Attack
        public static void Attack_CS_Set(object Entity, object Mod)
        {
            ((EntitiesManager.Entities)Entity).Attack_CombatStage += (int)Mod;
        }
        public static int Attack_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Attack_CombatStage;
        }
        #endregion
        #region Defence
        public static void Defence_CS_Set(object Entity, object Mod)
        {
            ((EntitiesManager.Entities)Entity).Defence_CombatStage += (int)Mod;
        }
        public static int Defence_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Defence_CombatStage;
        }
        #endregion
        #region SpAttack
        public static void SpAttack_CS_Set(object Entity, object Mod)
        {
            ((EntitiesManager.Entities)Entity).SpAttack_CombatStage += (int)Mod;
        }
        public static int SpAttack_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).SpAttack_CombatStage;
        }
        #endregion
        #region SpDefence
        public static void SpDefence_CS_Set(object Entity, object Mod)
        {
            ((EntitiesManager.Entities)Entity).SpDefence_CombatStage += (int)Mod;
        }
        public static int SpDefence_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).SpDefence_CombatStage;
        }
        #endregion
        #region Speed
        public static void Speed_CS_Set(object Entity, object Mod)
        {
            ((EntitiesManager.Entities)Entity).Speed_CombatStage += (int)Mod;
        }
        public static int Speed_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Speed_CombatStage;
        }
        #endregion
    }
}
