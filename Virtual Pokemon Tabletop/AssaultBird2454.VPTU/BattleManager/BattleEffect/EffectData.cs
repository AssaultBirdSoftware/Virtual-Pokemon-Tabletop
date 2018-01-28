using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect
{
    public class EffectData
    {
        public List<TriggerData> Triggers { get; set; }

        /// <summary>
        /// All the actions that this effect will execute for this effect
        /// </summary>
        public List<EffectFunction> Functions { get; set; }
    }

    public class TriggerData
    {
        public string Trigger { get; set; }
        public List<string> Functions { get; set; }
    }
}
