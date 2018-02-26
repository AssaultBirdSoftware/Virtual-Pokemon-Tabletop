using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect.Base_Actions
{
    public static class Random
    {
        public static int GenerateNumber(object Sides)
        {
            try
            {
                return RNG.Generators.RNG.GenerateNumber(Convert.ToByte(Sides));
            }
            catch { return 0; }
        }

        public static int Roll(object DiceFormula)
        {
            try
            {
                return RNG.Generators.RNG.RollDice((string)DiceFormula);
            }
            catch { return 0; }
        }
    }
}
