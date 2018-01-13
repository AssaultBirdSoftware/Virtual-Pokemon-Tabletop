using System.Collections.Generic;
using AssaultBird2454.VPTU.BattleManager.BattleEffect;
using AssaultBird2454.VPTU.BattleManager.Data;
using AssaultBird2454.VPTU.ContestManager.Data;

namespace AssaultBird2454.VPTU.Pokedex.Moves
{
    public class MoveData
    {
        /// <summary>
        ///     The Name of the move
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Description of move
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Defines what type of move it is
        /// </summary>
        public Type Move_Type { get; set; }

        /// <summary>
        ///     Defines what class of move it is
        /// </summary>
        public MoveClass Move_Class { get; set; }

        /// <summary>
        ///     Defines at what frequency the move can be used
        /// </summary>
        public Move_Frequency Move_Frequency { get; set; }

        /// <summary>
        ///     Defines how the move is used in terms of actions
        /// </summary>
        public Action_Type Move_ActionType { get; set; }

        /// <summary>
        ///     Defines the limit to the moves frequency
        /// </summary>
        public int Move_Frequency_Limit { get; set; }

        /// <summary>
        ///     Defines how strong the move is
        /// </summary>
        public DamageBase Move_DamageBase { get; set; }

        /// <summary>
        ///     Defines how hard or easy it is for the move to hit
        /// </summary>
        public int Move_Accuracy { get; set; }

        /// <summary>
        ///     Defines if the move affects 1 target or all valid targets
        /// </summary>
        public bool Move_TargetLimit_Unlimited { get; set; }

        /// <summary>
        ///     Defines the Max amount of targets that can be targeted by this move
        /// </summary>
        public int Move_TargetLimit { get; set; }

        /// <summary>
        ///     Defines the effect that the move has in a contest
        /// </summary>
        public Contest_Effects Contest_Effect { get; set; }

        /// <summary>
        ///     Defines the type in a contest
        /// </summary>
        public Contest_Type Contest_Type { get; set; }

        /// <summary>
        ///     Move Range Data
        /// </summary>
        public List<Move_RangeData> Range_Data { get; set; }

        /// <summary>
        ///     Move Keyword's
        /// </summary>
        public List<KeyValuePair<Move_KeyWords, object>> KeyWords { get; set; }

        /// <summary>
        ///     Defines when effect are used
        /// </summary>
        public EffectData Move_Effects { get; set; }

        public void Dispose()
        {
            Range_Data = null;
            KeyWords = null;
        }
    }
}