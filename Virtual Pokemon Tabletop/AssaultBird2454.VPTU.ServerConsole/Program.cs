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
            string commandstr = args.ToString();
            string[] commands = commandstr.Split('-');

            main = new Server.Main();

            if (commands.Contains("dedicated"))
            {
                main.Start_Dedicated();
            }
            else if (commands.Contains("intergrated"))
            {
                main.Start_Intergrated();
            }
            else if (commands.Contains("management"))
            {
                main.Start_Management();
            }
            else
            {
                Console.Write("Server Type not specified!");
                Console.Read();
            }
        }
    }
}
