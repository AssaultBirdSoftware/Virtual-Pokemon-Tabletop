using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.RNG.Generators
{
    public static class RSG
    {
        public static string GenerateString(int Length)
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[Length];
                rng.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
