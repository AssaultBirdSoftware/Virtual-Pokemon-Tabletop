using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Typing
{
    public class Manager
    {
        public Manager()
        {
            if (Types == null)
            {
                Types = new List<Type.Typing_Data>();
            }
        }

        public List<Type.Typing_Data> Types { get; set; }
    }

}
