using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public class Program
    {
        public static Server.Instances.ServerInstance Instance;

        static void Main(string[] args)
        {
            Instance = new Server.Instances.ServerInstance(@"D:\PTU\Saves\PokemonTabletop2.2 - Updated.ptu", new Server.Class.Logging.Console_Logger(true));
            Instance.StartServerInstance();
            Console.Read();
        }
    }
}
