using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Shared
{
    public static class Helper
    {
        public static string GetUntilOrEmpty(this StringBuilder sb, string stopAt = "-")
        {
            string text = sb.ToString();

            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
                sb.Remove(0, (charLocation + stopAt.Length));

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
    }
}
