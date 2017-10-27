using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    public interface EffectAction_Designer
    {
        void Load(object Data);
        void Save(object Data);
    }
}
