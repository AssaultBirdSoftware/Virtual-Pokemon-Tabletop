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
            int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
            int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

            foreach (EntitiesManager.Entities e in Targets)
            {
                Invoke(Move, e, User, AC, DB);
            }
        }

        public static void Invoke_Range_Attack(Pokedex.Moves.MoveData Move, List<EntitiesManager.Entities> Targets, EntitiesManager.Entities User)
        {
            foreach (EntitiesManager.Entities e in Targets)// for each target
            {
                int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
                int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

                Invoke(Move, e, User, AC, DB);
            }
        }

        private static void Invoke(Pokedex.Moves.MoveData Move, EntitiesManager.Entities Target, EntitiesManager.Entities User, int AC, int DB)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(@"C:\Users\Assau\Desktop\Effect LUA Files\Moves\Attack_Order.lua", true);

            handle.Set_GlobalVariable("Cancel", false);// Creates a global variable to cancel the attack
            handle.Set_GlobalVariable("Hit", false);// Set Hit
            handle.Set_GlobalVariable("Damage", null);// Damage to be delt
            handle.Set_GlobalVariable("Crit", false);// Set not critical hit
            handle.Set_GlobalVariable("AC", AC);// Set AC
            handle.Set_GlobalVariable("DB", DB);// Set DB

            if (Move.Move_Class == Data.MoveClass.Physical)
            {
                if (Target.Evasion_Physical >= Target.Evasion_Speed)
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + Target.Evasion_Physical);// DC for move with physical evasion
                }
                else
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + Target.Evasion_Speed);// DC for move with speed evasion
                }
            }// Phiycical Move
            else if (Move.Move_Class == Data.MoveClass.Special)
            {
                if (Target.Evasion_Special >= Target.Evasion_Speed)
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + Target.Evasion_Special);// DC for move with Special evasion
                }
                else
                {
                    handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + Target.Evasion_Speed);// DC for move with speed evasion
                }
            }// Special Move
            else if (Move.Move_Class == Data.MoveClass.Status)
            {
                handle.Set_GlobalVariable("DC_AC", Move.Move_Accuracy + Target.Evasion_Speed);// DC for move with speed evasion
            }// Status Move
            else
            {
                // What to do if move is static?
            }// Static

            if (AC == 20)// If Critical hit
                handle.Set_GlobalVariable("Crit", true);// Set Critical Hit

            handle.Get_Trigger("Pre_Attack_Invoked").Call(null, Target, User);// Invoke effects to be executed before damage is delt

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
                handle.Get_Trigger("Post_Attack_Invoked").Call(null, Target, User);// Invoke effects to be executed before damage is delt
            }// Check if the attack is not canceled

            MessageBox.Show("Attack Stats\nAC: " + AC + "\nDB: " + DB + "\nCrit: " + ((bool)handle.Get_GlobalVariable("Crit")).ToString() + "\nHit: " +
                ((bool)handle.Get_GlobalVariable("Hit")).ToString() + "\nDamage: " + handle.Get_GlobalVariable("Damage").ToString() + "\nMove: " + Move.Name);
        }
    }
}
