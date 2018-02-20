using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect
{
    public class Effect_Function
    {
        public string FunctionName { get; set; }
        public string FunctionDesc { get; set; }
        public string EffectScript_ID { get; set; }

        public void Attack_AoE_Invoked(Pokedex.Moves.MoveData MoveData, object User, List<object> Targets)
        {
            Lua lua = new Lua();
            lua.RegisterFunction("InflictStatus", this, GetType().GetMethod("InflictStatus"));
            lua.DoFile(@"C:\Users\Tasman Leach\Desktop\EFfect LUA Files\j7KCS9Bd8DBlWgDL.lua");
            LuaFunction myFunction = lua["Attack_Invoked"] as LuaFunction;

            foreach (object obj in Targets)
                myFunction.Call(MoveData, obj, User);
        }

        public void InflictStatus(Pokedex.Moves.MoveData Target, string Effect)
        {
            System.Windows.MessageBox.Show(Target.Name + " is now " + Effect);
        }

        public void Attack_Range_Invoked(object User, List<object> Targets)
        {

        }
    }
}
