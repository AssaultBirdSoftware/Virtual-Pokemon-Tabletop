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
            if (Convert.ToInt32(Mod) > 0 || Convert.ToInt32(Mod) < 0)
            {
                ((EntitiesManager.Entities)Entity).Attack_CombatStage += Convert.ToInt32(Mod);
            }
            else
            {
                ((EntitiesManager.Entities)Entity).Attack_CombatStage = 0;
            }
        }
        public static int Attack_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Attack_CombatStage;
        }
        #endregion
        #region Defence
        public static void Defence_CS_Set(object Entity, object Mod)
        {
            if (Convert.ToInt32(Mod) > 0 || Convert.ToInt32(Mod) < 0)
            {
                ((EntitiesManager.Entities)Entity).Defence_CombatStage += Convert.ToInt32(Mod);
            }
            else
            {
                ((EntitiesManager.Entities)Entity).Defence_CombatStage = 0;
            }
        }
        public static int Defence_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Defence_CombatStage;
        }
        #endregion
        #region SpAttack
        public static void SpAttack_CS_Set(object Entity, object Mod)
        {
            if (Convert.ToInt32(Mod) > 0 || Convert.ToInt32(Mod) < 0)
            {
                ((EntitiesManager.Entities)Entity).SpAttack_CombatStage += Convert.ToInt32(Mod);
            }
            else
            {
                ((EntitiesManager.Entities)Entity).SpAttack_CombatStage = 0;
            }
        }
        public static int SpAttack_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).SpAttack_CombatStage;
        }
        #endregion
        #region SpDefence
        public static void SpDefence_CS_Set(object Entity, object Mod)
        {
            if (Convert.ToInt32(Mod) > 0 || Convert.ToInt32(Mod) < 0)
            {
                ((EntitiesManager.Entities)Entity).SpDefence_CombatStage += Convert.ToInt32(Mod);
            }
            else
            {
                ((EntitiesManager.Entities)Entity).SpDefence_CombatStage = 0;
            }
        }
        public static int SpDefence_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).SpDefence_CombatStage;
        }
        #endregion
        #region Speed
        public static void Speed_CS_Set(object Entity, object Mod)
        {
            if (Convert.ToInt32(Mod) > 0 || Convert.ToInt32(Mod) < 0)
            {
                ((EntitiesManager.Entities)Entity).Speed_CombatStage += Convert.ToInt32(Mod);
            }
            else
            {
                ((EntitiesManager.Entities)Entity).Speed_CombatStage = 0;
            }
        }
        public static int Speed_CS_Get(object Entity)
        {
            return ((EntitiesManager.Entities)Entity).Speed_CombatStage;
        }
        #endregion

        public static int GetHealth(object Entitie)
        {
            return ((Entities)Entitie).Current_HP;
        }
        public static void SetHealth(object Entitie, object Value)
        {
            ((Entities)Entitie).Current_HP = (int)Value;
        }
    }
}
