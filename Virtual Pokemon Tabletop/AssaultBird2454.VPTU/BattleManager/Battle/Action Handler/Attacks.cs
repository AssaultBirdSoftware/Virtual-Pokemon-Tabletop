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
        public static void Invoke_AoE_Attack(SaveManager.SaveManager mgr, Pokedex.Moves.MoveData Move, List<EntitiesManager.Entities> Targets, EntitiesManager.Entities User)
        {
            int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
            int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

            foreach (EntitiesManager.Entities e in Targets)
            {
                Invoke(mgr, Move, e, User, AC, DB);
            }
        }

        public static void Invoke_Range_Attack(SaveManager.SaveManager mgr, Pokedex.Moves.MoveData Move, List<EntitiesManager.Entities> Targets, EntitiesManager.Entities User)
        {
            foreach (EntitiesManager.Entities e in Targets)// for each target
            {
                int AC = RNG.Generators.RNG.GenerateNumber(20);// Rolls the AC
                int DB = RNG.Generators.RNG.GenerateNumber(20);// Rolls the DB

                bool Hit = Invoke(mgr, Move, e, User, AC, DB);// Invoke Attack
                if ((bool)(Move.KeyWords.Find(x => x.Key == Data.Move_KeyWords.DubleStrike).Value) == true && Hit)
                {
                    Invoke(mgr, Move, e, User, AC, DB);// Invoke Attack Again
                }// If DubleStrike & Hit first hit
            }
        }

        private static bool Invoke(SaveManager.SaveManager mgr, Pokedex.Moves.MoveData Move, EntitiesManager.Entities Target, EntitiesManager.Entities User, int AC, int DB)
        {
            VPTU.BattleManager.Effect.Effect_Handler handle = new BattleManager.Effect.Effect_Handler(mgr.LoadEffect_LuaScript(Move.Move_EffectsScript_ID), false);

            handle.Set_GlobalVariable("Cancel", false);// Creates a global variable to cancel the attack
            handle.Set_GlobalVariable("Hit", false);// Set Hit
            handle.Set_GlobalVariable("Damage", 0);// Damage to be delt
            handle.Set_GlobalVariable("Crit", false);// Set not critical hit
            handle.Set_GlobalVariable("AC", AC);// Set AC
            handle.Set_GlobalVariable("DB", DB);// Set DB
            handle.Set_GlobalVariable("DB_Mod", 0);// Set DB Mod
            handle.Set_GlobalVariable("Strikes", 1);// Set Strikes
            handle.Set_GlobalVariable("DC", 0);// Set DC

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

            if ((bool)(Move.KeyWords.Find(x => x.Key == Data.Move_KeyWords.FiveStrike).Value) == true)
            {
                int _strike = RNG.Generators.RNG.GenerateNumber(8);
                switch (_strike)
                {
                    case 1:
                        handle.Set_GlobalVariable("Strikes", 1);// Set Strikes
                        break;
                    case 2:
                        handle.Set_GlobalVariable("Strikes", 2);// Set Strikes
                        break;
                    case 3:
                        handle.Set_GlobalVariable("Strikes", 2);// Set Strikes
                        break;
                    case 4:
                        handle.Set_GlobalVariable("Strikes", 3);// Set Strikes
                        break;
                    case 5:
                        handle.Set_GlobalVariable("Strikes", 3);// Set Strikes
                        break;
                    case 6:
                        handle.Set_GlobalVariable("Strikes", 4);// Set Strikes
                        break;
                    case 7:
                        handle.Set_GlobalVariable("Strikes", 4);// Set Strikes
                        break;
                    case 8:
                        handle.Set_GlobalVariable("Strikes", 5);// Set Strikes
                        break;
                    default:
                        handle.Set_GlobalVariable("Strikes", 5);// Set Strikes
                        break;
                }
            }

            if (AC == 20)// If Critical hit
                handle.Set_GlobalVariable("Crit", true);// Set Critical Hit
            if (AC >= ((double)handle.Get_GlobalVariable("DC_AC")))
                handle.Set_GlobalVariable("Hit", true);// Set Hit

            handle.Call_Function("Pre_Attack_Invoked", Move, Target, User);// Invoke effects to be executed before damage is delt

            if (!(bool)handle.Get_GlobalVariable("Cancel") && (bool)handle.Get_GlobalVariable("Hit"))
            {
                #region Calculate DMG
                int Damage = 0;
                // Initial DMG
                // Apply "Five Strike" / "Double Strike"

                // Add DB Mods (STAB), Modify DB Roll for crit & Roll dmg
                if (User is EntitiesManager.Pokemon.PokemonCharacter)
                {
                    if (((EntitiesManager.Pokemon.PokemonCharacter)User).PokemonType.Find(x => x.ToLower() == Move.Move_Type.ToLower()) != null)
                    {
                        // Stab
                        Damage = Roll_DB(((int)Move.Move_DamageBase) + 2 + Convert.ToInt32((double)handle.Get_GlobalVariable("DB_Mod")), (bool)handle.Get_GlobalVariable("Crit"));
                    }
                    else
                    {
                        // Not Stab
                        Damage = Roll_DB(((int)Move.Move_DamageBase) + Convert.ToInt32((double)handle.Get_GlobalVariable("DB_Mod")), (bool)handle.Get_GlobalVariable("Crit"));
                    }
                }
                else
                {
                    // Human
                    Damage = Roll_DB(((int)Move.Move_DamageBase) + Convert.ToInt32((double)handle.Get_GlobalVariable("DB_Mod")), (bool)handle.Get_GlobalVariable("Crit"));
                }

                // Add Attack & Other Bonuses
                if (Move.Move_Class == Data.MoveClass.Physical)
                    Damage += User.Attack_Adjusted;
                else if (Move.Move_Class == Data.MoveClass.Special)
                    Damage += User.SpAttack_Adjusted;

                // Subtract Defence & Other dmg reduction
                if (Move.Move_Class == Data.MoveClass.Physical)
                    Damage += Target.Defence_Adjusted;
                else if (Move.Move_Class == Data.MoveClass.Special)
                    Damage += Target.SpDefence_Adjusted;

                // Apply weekness and resistance
                // Subtract dmg from targets HP
                #endregion

                handle.Call_Function("Post_Attack_Invoked", Move, Target, User);// Invoke effects to be executed before damage is delt
                return true;
            }// Check if the attack is not canceled

            return false;
        }
        private static int Roll_DB(Data.DamageBase DB, bool Crit)
        {
            if (Crit)
                switch (DB)
                {
                    case Data.DamageBase.DB1:
                        return RNG.Generators.RNG.RollDice("2d6+2");
                    case Data.DamageBase.DB2:
                        return RNG.Generators.RNG.RollDice("2d6+6");
                    case Data.DamageBase.DB3:
                        return RNG.Generators.RNG.RollDice("2d6+10");
                    case Data.DamageBase.DB4:
                        return RNG.Generators.RNG.RollDice("2d8+12");
                    case Data.DamageBase.DB5:
                        return RNG.Generators.RNG.RollDice("2d8+16");
                    case Data.DamageBase.DB6:
                        return RNG.Generators.RNG.RollDice("4d6+16");
                    case Data.DamageBase.DB7:
                        return RNG.Generators.RNG.RollDice("4d6+20");
                    case Data.DamageBase.DB8:
                        return RNG.Generators.RNG.RollDice("4d8+20");
                    case Data.DamageBase.DB9:
                        return RNG.Generators.RNG.RollDice("4d10+20");
                    case Data.DamageBase.DB10:
                        return RNG.Generators.RNG.RollDice("6d8+20");
                    case Data.DamageBase.DB11:
                        return RNG.Generators.RNG.RollDice("6d10+20");
                    case Data.DamageBase.DB12:
                        return RNG.Generators.RNG.RollDice("6d12+20");
                    case Data.DamageBase.DB13:
                        return RNG.Generators.RNG.RollDice("8d10+20");
                    case Data.DamageBase.DB14:
                        return RNG.Generators.RNG.RollDice("8d10+30");
                    case Data.DamageBase.DB15:
                        return RNG.Generators.RNG.RollDice("8d10+40");
                    case Data.DamageBase.DB16:
                        return RNG.Generators.RNG.RollDice("10d10+40");
                    case Data.DamageBase.DB17:
                        return RNG.Generators.RNG.RollDice("10d12+50");
                    case Data.DamageBase.DB18:
                        return RNG.Generators.RNG.RollDice("12d12+50");
                    case Data.DamageBase.DB19:
                        return RNG.Generators.RNG.RollDice("12d12+60");
                    case Data.DamageBase.DB20:
                        return RNG.Generators.RNG.RollDice("12d12+70");
                    case Data.DamageBase.DB21:
                        return RNG.Generators.RNG.RollDice("12d12+80");
                    case Data.DamageBase.DB22:
                        return RNG.Generators.RNG.RollDice("12d12+90");
                    case Data.DamageBase.DB23:
                        return RNG.Generators.RNG.RollDice("12d12+100");
                    case Data.DamageBase.DB24:
                        return RNG.Generators.RNG.RollDice("12d12+110");
                    case Data.DamageBase.DB25:
                        return RNG.Generators.RNG.RollDice("12d12+120");
                    case Data.DamageBase.DB26:
                        return RNG.Generators.RNG.RollDice("14d12+130");
                    case Data.DamageBase.DB27:
                        return RNG.Generators.RNG.RollDice("16d12+140");
                    case Data.DamageBase.DB28:
                        return RNG.Generators.RNG.RollDice("16d12+160");
                    default:
                        return 2;
                }// Rolls Crit DB's
            else
                switch (DB)
                {
                    case Data.DamageBase.DB1:
                        return RNG.Generators.RNG.RollDice("1d6+1");
                    case Data.DamageBase.DB2:
                        return RNG.Generators.RNG.RollDice("1d6+3");
                    case Data.DamageBase.DB3:
                        return RNG.Generators.RNG.RollDice("1d6+5");
                    case Data.DamageBase.DB4:
                        return RNG.Generators.RNG.RollDice("1d8+6");
                    case Data.DamageBase.DB5:
                        return RNG.Generators.RNG.RollDice("1d8+8");
                    case Data.DamageBase.DB6:
                        return RNG.Generators.RNG.RollDice("2d6+8");
                    case Data.DamageBase.DB7:
                        return RNG.Generators.RNG.RollDice("2d6+10");
                    case Data.DamageBase.DB8:
                        return RNG.Generators.RNG.RollDice("2d8+10");
                    case Data.DamageBase.DB9:
                        return RNG.Generators.RNG.RollDice("2d10+10");
                    case Data.DamageBase.DB10:
                        return RNG.Generators.RNG.RollDice("3d8+10");
                    case Data.DamageBase.DB11:
                        return RNG.Generators.RNG.RollDice("3d10+10");
                    case Data.DamageBase.DB12:
                        return RNG.Generators.RNG.RollDice("3d12+10");
                    case Data.DamageBase.DB13:
                        return RNG.Generators.RNG.RollDice("4d10+10");
                    case Data.DamageBase.DB14:
                        return RNG.Generators.RNG.RollDice("4d10+15");
                    case Data.DamageBase.DB15:
                        return RNG.Generators.RNG.RollDice("4d10+20");
                    case Data.DamageBase.DB16:
                        return RNG.Generators.RNG.RollDice("5d10+20");
                    case Data.DamageBase.DB17:
                        return RNG.Generators.RNG.RollDice("5d12+25");
                    case Data.DamageBase.DB18:
                        return RNG.Generators.RNG.RollDice("6d12+25");
                    case Data.DamageBase.DB19:
                        return RNG.Generators.RNG.RollDice("6d12+30");
                    case Data.DamageBase.DB20:
                        return RNG.Generators.RNG.RollDice("6d12+35");
                    case Data.DamageBase.DB21:
                        return RNG.Generators.RNG.RollDice("6d12+40");
                    case Data.DamageBase.DB22:
                        return RNG.Generators.RNG.RollDice("6d12+45");
                    case Data.DamageBase.DB23:
                        return RNG.Generators.RNG.RollDice("6d12+50");
                    case Data.DamageBase.DB24:
                        return RNG.Generators.RNG.RollDice("6d12+55");
                    case Data.DamageBase.DB25:
                        return RNG.Generators.RNG.RollDice("6d12+60");
                    case Data.DamageBase.DB26:
                        return RNG.Generators.RNG.RollDice("7d12+65");
                    case Data.DamageBase.DB27:
                        return RNG.Generators.RNG.RollDice("8d12+70");
                    case Data.DamageBase.DB28:
                        return RNG.Generators.RNG.RollDice("8d12+80");
                    default:
                        return 1;
                }// Rolls Nromal DB's
        }
        private static int Roll_DB(int DB, bool Crit)
        {
            Data.DamageBase _DB = (Data.DamageBase)DB;
            return Roll_DB(_DB, Crit);
        }
    }
}
