using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class.ConsoleCommands
{
    public static class Command_Listener
    {
        public static string Console_Listen()
        {
            return Console.ReadLine();
        }
        public static ConsoleKeyInfo Console_Listen_Key()
        {
            return Console.ReadKey();
        }
    }
}
