using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.RNG.Generators
{
    public static class RNG
    {
        public static int GenerateNumber(byte Sides)
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                // Create a byte array to hold the random value.
                byte[] randomNumber = new byte[1];
                do
                {
                    // Fill the array with a random value.
                    rng.GetBytes(randomNumber);
                }
                while (!IsFairRoll(randomNumber[0], Sides));
                // Return the random number mod the number
                // of sides.  The possible values are zero-
                // based, so we add one.
                return (byte)((randomNumber[0] % Sides) + 1);
            }
        }

        public static int RollDice(string DiceForumla)
        {
            int dice = Convert.ToInt32(DiceForumla.Split('d')[0]);
            int sides = Convert.ToInt32(DiceForumla.Split('d')[1].Split('+', '-')[0]);
            int mod = Convert.ToInt32(DiceForumla.Split('+', '-')[1]);
            char mod_opp = DiceForumla.ToCharArray().First(x => x == '+' || x == '-');

            int val = 0;

            for (int i = 0; i >= dice; i++)
            {
                try { val *= 10; } catch { }
                val += GenerateNumber(Convert.ToByte(sides));
            }

            if (mod_opp == '+')
            {
                return val + mod;
            }
            else if (mod_opp == '-')
            {
                return val - mod;
            }
            else
            {
                return val + mod;
            }
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
    }
}
