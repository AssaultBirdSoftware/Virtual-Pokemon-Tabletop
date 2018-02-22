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

        public Effect_Handler(string Script)
        {
            lua.DoString(Script);

            #region Register Base Actions
            
            #endregion
        }

        public void Register_Action(string Function, object Target, System.Reflection.MethodBase Method)
        {
            lua.RegisterFunction(Function, Target, Method);
        }
        public NLua.LuaFunction Get_Trigger(string TriggerName)
        {
            return lua.GetFunction(TriggerName);
        }

        public void Set_GlobalVariable(string Name, object Value)
        {
            lua[Name] = Value;
        }
        public object Get_GlobalVariable(string Name)
        {
            return lua[Name];
        }
    }
}