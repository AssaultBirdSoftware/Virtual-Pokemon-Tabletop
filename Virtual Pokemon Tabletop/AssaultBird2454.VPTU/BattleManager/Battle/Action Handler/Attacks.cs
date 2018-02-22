using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Battle.Action_Handler
{
    public static class Attack
    {
        public static void Invoke_AoE_Attack(Pokedex.Moves.MoveData Move, List<EntitiesManager.Entry> Targets, EntitiesManager.Entry User)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(@"C:\Users\Assau\Desktop\Effect LUA Files\Moves\Attack_Order.lua", true);

            int AC = RNG.Generators.RNG.GenerateNumber(20);
            int DB = RNG.Generators.RNG.GenerateNumber(20);

            foreach (EntitiesManager.Entry e in Targets)
            {
                handle.Set_GlobalVariable("Cancel", false);

                if (AC == 20)
                    handle.Set_GlobalVariable("Crit", true);
                else
                    handle.Set_GlobalVariable("Crit", false);

                handle.Set_GlobalVariable("AC", AC);
                handle.Set_GlobalVariable("DB", DB);
                handle.Get_Trigger("Attack_Invoked").Call(null, e, User);

                if ((bool)handle.Get_GlobalVariable("Crit"))
                {
                    // Crit
                }
                else
                {
                    // Not a Crit
                }

                if (!(bool)handle.Get_GlobalVariable("Cancel"))
                {
                    // Continue
                }
            }
        }

        public static void Invoke_Range_Attack(Pokedex.Moves.MoveData Move, List<EntitiesManager.Entry> Targets, EntitiesManager.Entry User)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(@"C:\Users\Assau\Desktop\Effect LUA Files\Moves\Attack_Order.lua", true);

            foreach (EntitiesManager.Entry e in Targets)
            {
                handle.Set_GlobalVariable("Cancel", false);

                int AC = RNG.Generators.RNG.GenerateNumber(20);
                int DB = RNG.Generators.RNG.GenerateNumber(20);

                if (AC == 20)
                    handle.Set_GlobalVariable("Crit", true);
                else
                    handle.Set_GlobalVariable("Crit", false);

                handle.Set_GlobalVariable("AC", AC);
                handle.Set_GlobalVariable("DB", DB);
                handle.Get_Trigger("Attack_Invoked").Call(null, e, User);

                if ((bool)handle.Get_GlobalVariable("Crit"))
                {
                    // Crit
                }
                else
                {
                    // Not a Crit
                }

                if (!(bool)handle.Get_GlobalVariable("Cancel"))
                {
                    // Continue
                }
            }
        }
    }
}
