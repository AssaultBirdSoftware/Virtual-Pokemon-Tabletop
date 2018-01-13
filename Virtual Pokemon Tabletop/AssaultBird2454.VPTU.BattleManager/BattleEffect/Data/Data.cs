namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public enum Triggers
    {
        Static = 1,
        Attack_Hit = 2,
        Attack_Missed = 3,
        Critical_Hit = 4
    }

    public enum Conditions
    {
        Dice_Pass = 1,
        Dice_Fail = 2,
        Variable_Roll_Pass = 3,
        Variable_Roll_Fail = 4
    }

    public enum Variables
    {
        Dice = 1
    }

    /// <summary>
    ///     Defines what targets the effect is executed on
    /// </summary>
    public enum Target
    {
        Target,
        Area
    }
}