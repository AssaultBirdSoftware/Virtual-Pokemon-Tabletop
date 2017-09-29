using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public class Program
    {
        public static Server.Server.ServerInstance Instance;

        static void Main(string[] args)
        {
            Instance = new Server.Server.ServerInstance(@"D:\PTU\Saves\PokemonTabletop2.2 - Updated.ptu", new Server.Class.Logging.Console_Logger(true));
            Console.Read();
        }
    }
}
