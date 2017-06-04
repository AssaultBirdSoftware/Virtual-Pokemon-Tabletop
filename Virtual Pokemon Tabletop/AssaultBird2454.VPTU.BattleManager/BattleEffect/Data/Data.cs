using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public enum Triggers { Static = 1, Attack_Hit = 2, Attack_Missed = 3, Critical_Hit = 4 }
    public enum Conditions { Dice_Pass = 1, Dice_Fail = 2, Variable_Roll_Pass = 3, Variable_Roll_Fail = 4 }
    public enum Actions { Status = 1, CombatStage = 2, DamageBase = 3 }
    public enum Variables { Dice = 1 }
}
