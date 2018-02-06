using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect
{
    public interface Effect_Action
    {
        string Action_Name { get; set; }
        string Action_Description { get; set; }
        string Action_Command { get; }
    }
}
