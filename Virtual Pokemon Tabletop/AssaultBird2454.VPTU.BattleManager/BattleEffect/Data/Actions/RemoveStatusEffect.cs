using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions
{
    public class RemoveStatusEffect
    {
        public RemoveStatusEffect()
        {
            StatusEffect = Status_Afflictions.Burned;
        }

        public Status_Afflictions StatusEffect { get; set; }
    }
}