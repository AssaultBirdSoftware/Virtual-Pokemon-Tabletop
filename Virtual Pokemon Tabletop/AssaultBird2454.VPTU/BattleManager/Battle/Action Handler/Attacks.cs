using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssaultBird2454.VPTU.BattleManager.Battle.Action_Handler
{
    public static class Attack
    {
        public static void Invoke_AoE_Attack(Pokedex.Moves.MoveData Move, List<EntitiesManager.Entities> Targets, EntitiesManager.Entities User)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(@"C:\Users\Assau\Desktop\Effect LUA Files\Moves\Attack_Order.lua", true);

            int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
            int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

            foreach (EntitiesManager.Entities e in Targets)
            {
                handle.Set_GlobalVariable("Cancel", false);// Creates a global variable to cancel the attack
                handle.Set_GlobalVariable("Hit", false);// Set Hit
                handle.Set_GlobalVariable("Damage", null);// Damage to be delt
                handle.Set_GlobalVariable("Crit", false);// Set not critical hit
                handle.Set_GlobalVariable("AC", AC);// Set AC
                handle.Set_GlobalVariable("DB", DB);// Set DB

                if (Move.Move_Class == Data.MoveClass.Physical)
                {
                    if (e.Evasion_Physical >= e.Evasion_Speed)
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Physical);// DC for move with physical evasion
                    }
                    else
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                    }
                }
                else if (Move.Move_Class == Data.MoveClass.Special)
                {
                    if (e.Evasion_Special >= e.Evasion_Speed)
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Special);// DC for move with Special evasion
                    }
                    else
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                    }
                }
                else if (Move.Move_Class == Data.MoveClass.Status)
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                }
                else
                {
                    // What to do if move is static?
                }

                if (AC == 20)// If Critical hit
                    handle.Set_GlobalVariable("Crit", true);// Set Critical Hit

                handle.Get_Trigger("Pre_Attack_Invoked").Call(null, e, User);// Invoke effects to be executed before damage is delt

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

                    MessageBox.Show("");
                    handle.Get_Trigger("Post_Attack_Invoked").Call(null, e, User);// Invoke effects to be executed before damage is delt
                }// Check if the attack is not canceled
            }
        }

        public static void Invoke_Range_Attack(Pokedex.Moves.MoveData Move, List<EntitiesManager.Entities> Targets, EntitiesManager.Entities User)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(@"C:\Users\Assau\Desktop\Effect LUA Files\Moves\Attack_Order.lua", true);

            foreach (EntitiesManager.Entities e in Targets)// for each target
            {
                int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
                int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

                handle.Set_GlobalVariable("Cancel", false);// Creates a global variable to cancel the attack
                handle.Set_GlobalVariable("Hit", false);// Set Hit
                handle.Set_GlobalVariable("Damage", null);// Damage to be delt
                handle.Set_GlobalVariable("Crit", false);// Set not critical hit
                handle.Set_GlobalVariable("AC", AC);// Set AC
                handle.Set_GlobalVariable("DB", DB);// Set DB

                if (Move.Move_Class == Data.MoveClass.Physical)
                {
                    if (e.Evasion_Physical >= e.Evasion_Speed)
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Physical);// DC for move with physical evasion
                    }
                    else
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                    }
                }
                else if (Move.Move_Class == Data.MoveClass.Special)
                {
                    if (e.Evasion_Special >= e.Evasion_Speed)
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Special);// DC for move with Special evasion
                    }
                    else
                    {
                        handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                    }
                }
                else if (Move.Move_Class == Data.MoveClass.Status)
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + e.Evasion_Speed);// DC for move with speed evasion
                }
                else
                {
                    // What to do if move is static?
                }

                if (AC == 20)// If Critical hit
                    handle.Set_GlobalVariable("Crit", true);// Set Critical Hit

                handle.Get_Trigger("Pre_Attack_Invoked").Call(null, e, User);// Invoke effects to be executed before damage is delt

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
                    handle.Get_Trigger("Post_Attack_Invoked").Call(null, e, User);// Invoke effects to be executed before damage is delt
                }// Check if the attack is not canceled
            }
        }
    }
}
