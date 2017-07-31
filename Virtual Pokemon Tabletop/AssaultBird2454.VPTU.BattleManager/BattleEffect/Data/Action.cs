using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public interface Action
    {
        string Action_Name { get; set; }
    }

    public class AddStatusEffect : Action
    {
        public string Action_Name { get; set; }

    }
}
