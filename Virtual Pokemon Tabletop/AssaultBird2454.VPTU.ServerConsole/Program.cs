using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.ServerConsole
{
    public class Program
    {
        private static Server.Main main;

        static void Main(string[] args)
        {
            main = new Server.Main();
            main.Start_Dedicated();
        }
    }
}
