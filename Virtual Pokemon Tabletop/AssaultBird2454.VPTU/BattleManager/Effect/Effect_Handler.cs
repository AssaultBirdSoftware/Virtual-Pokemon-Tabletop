using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect
{
    public class Effect_Handler
    {
        private NLua.Lua lua = new NLua.Lua();

        public Effect_Handler(string Script, bool File = false)
        {
            if (!File)
                lua.DoString(Script);
            else
                lua.DoFile(Script);

            #region Register Base Actions
            Register_Action("Generate_Number", null, typeof(Base_Actions.Random).GetMethod("GenerateNumber"));// Exposes an interface to get a random number
            Register_Action("Roll", null, typeof(Base_Actions.Random).GetMethod("Roll"));// Exposes an interface to roll dice

            Register_Action("StatusEffect_Add", null, typeof(Base_Actions.Status).GetMethod("AddStatus"));// Exposes an interface to add status conditions to entities
            Register_Action("StatusEffect_Remove", null, typeof(Base_Actions.Status).GetMethod("RemoveStatus"));// Exposes an interface to remove status conditions to entities
            Register_Action("StatusEffect_Has", null, typeof(Base_Actions.Status).GetMethod("HasStatus"));// Exposes and interface to check if an entitie has a status condition
            Register_Action("StatusEffect_Get", null, typeof(Base_Actions.Status).GetMethod("GetStatus"));// Exposes and interface to get data associated with a status condition

            Register_Action("Stat_Attack_CS_Set", null, typeof(Base_Actions.Stats).GetMethod("Attack_CS_Set"));// Exposes and interface to Set Attack CS
            Register_Action("Stat_Attack_CS_Get", null, typeof(Base_Actions.Stats).GetMethod("Attack_CS_Get"));// Exposes and interface to Get Attack CS
            Register_Action("Stat_Defence_CS_Set", null, typeof(Base_Actions.Stats).GetMethod("Defence_CS_Set"));// Exposes and interface to Set Defence CS
            Register_Action("Stat_Defence_CS_Get", null, typeof(Base_Actions.Stats).GetMethod("Defence_CS_Get"));// Exposes and interface to Get Defence CS
            Register_Action("Stat_SpAttack_CS_Set", null, typeof(Base_Actions.Stats).GetMethod("SpAttack_CS_Set"));// Exposes and interface to Set SpAttack CS
            Register_Action("Stat_SpAttack_CS_Get", null, typeof(Base_Actions.Stats).GetMethod("SpAttack_CS_Get"));// Exposes and interface to Get SpAttack CS
            Register_Action("Stat_SpDefence_CS_Set", null, typeof(Base_Actions.Stats).GetMethod("SpDefence_CS_Set"));// Exposes and interface to Set SpDefence CS
            Register_Action("Stat_SpDefence_CS_Get", null, typeof(Base_Actions.Stats).GetMethod("SpDefence_CS_Get"));// Exposes and interface to Get SpDefence CS
            Register_Action("Stat_Speed_CS_Set", null, typeof(Base_Actions.Stats).GetMethod("Speed_CS_Set"));// Exposes and interface to Set Speed CS
            Register_Action("Stat_Speed_CS_Get", null, typeof(Base_Actions.Stats).GetMethod("Speed_CS_Get"));// Exposes and interface to Get Speed CS
            Register_Action("Current_HP_Get", null, typeof(Base_Actions.Stats).GetMethod("GetHealth"));// Exposes and interface to Get the entities health
            Register_Action("Current_HP_Set", null, typeof(Base_Actions.Stats).GetMethod("SetHealth"));// Exposes and interface to Set the entities health
            #endregion
            // Register_Action("", null, typeof().GetMethod(""));
        }

        /// <summary>
        /// Registers an action that can be called from the lua script
        /// </summary>
        /// <param name="Function">Name of the command in the script</param>
        /// <param name="Target">The target object to invoke the method on</param>
        /// <param name="Method">The method to invoke</param>
        public void Register_Action(string Function, object Target, System.Reflection.MethodBase Method)
        {
            lua.RegisterFunction(Function, Target, Method);
        }
        /// <summary>
        /// Gets a function in the lua script that can be called
        /// </summary>
        /// <param name="TriggerName">Name of the function in the script</param>
        /// <returns>An object that wit ha call function</returns>
        private NLua.LuaFunction Get_Trigger(string TriggerName)
        {
            return lua.GetFunction(TriggerName);
        }
        public void Call_Function(string TriggerName, params object[] objects)
        {
            try
            {
                lua.GetFunction(TriggerName).Call(objects);
            }
            catch (NullReferenceException nre) { }
        }

        /// <summary>
        /// Sets a global variable accessible in the lua script
        /// </summary>
        /// <param name="Name">Name of the vaiable</param>
        /// <param name="Value">Value for the variable</param>
        public void Set_GlobalVariable(string Name, object Value)
        {
            lua[Name] = Value;
        }
        /// <summary>
        /// Gets a global variable that is set on the lua script
        /// </summary>
        /// <param name="Name">The Name of the variable</param>
        /// <returns>The Value</returns>
        public object Get_GlobalVariable(string Name)
        {
            object obj = lua[Name];
            Type type = obj.GetType();
            return obj;
        }
    }
}