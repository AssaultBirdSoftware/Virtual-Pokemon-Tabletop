using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.Entity
{
    /// <summary>
    ///     Defines the gender
    /// </summary>
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Genderless = 0
    }

    /// <summary>
    ///     Defines Size Classes
    /// </summary>
    public enum SizeClass
    {
        Small = 0,
        Medium = 1,
        Large = 2,
        Huge = 3,
        Gigantic = 4
    }

    /// <summary>
    ///     Defines Weight Class
    ///     WC1(0-11KG) ->
    ///     WC2(11-25KG) ->
    ///     WC3(25-50KG) ->
    ///     WC4(50-100KG)->
    ///     WC5(100-200KG) ->
    ///     WC6(200KG +) ->
    ///     WC7(200KG + With "Heavy Metal Ability")
    /// </summary>
    public enum WeightClass
    {
        WC1 = 1,
        WC2 = 2,
        WC3 = 3,
        WC4 = 4,
        WC5 = 5,
        WC6 = 6,
        WC7 = 7
    }

    /// <summary>
    ///     Defines Skill Ranks
    /// </summary>
    public enum SkillRank
    {
        Pathetic = 1,
        Untrained = 2,
        Novice = 3,
        Adept = 4,
        Expert = 5,
        Master = 6
    }


    public class Capability_Data
    {
        public int Power { get; set; }
        public int ThrowingRange { get; set; }
        public int LongJump { get; set; }
        public int HighJump { get; set; }

        public int Burrow { get; set; }
        public int Overland { get; set; }
        public int Sky { get; set; }
        public int Swim { get; set; }
        public int Levitate { get; set; }
        public int Teleport { get; set; }

        public NatureWalk_Type NatureWalk_1 { get; set; }
        public NatureWalk_Type NatureWalk_2 { get; set; }
    }

    public class Skill_Data
    {
        #region Skills

        #region Body

        /// <summary>
        /// </summary>
        public int Acrobatics_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Acrobatics_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Athletics_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Athletics_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Combat_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Combat_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Intimidate_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Intimidate_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Stealth_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Stealth_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Survival_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Survival_Rank { get; set; }

        #endregion

        #region Mind

        /// <summary>
        ///     General Education Mod
        /// </summary>
        public int General_Mod { get; set; }

        /// <summary>
        ///     General Education Rank
        /// </summary>
        public SkillRank General_Rank { get; set; }

        /// <summary>
        ///     Medicine Education Mod
        /// </summary>
        public int Medicine_Mod { get; set; }

        /// <summary>
        ///     Medicine Education Rank
        /// </summary>
        public SkillRank Medicine_Rank { get; set; }

        /// <summary>
        ///     Occult Education Mod
        /// </summary>
        public int Occult_Mod { get; set; }

        /// <summary>
        ///     Occult Education Rank
        /// </summary>
        public SkillRank Occult_Rank { get; set; }

        /// <summary>
        ///     Pokemon Education Mod
        /// </summary>
        public int Pokemon_Mod { get; set; }

        /// <summary>
        ///     Pokemon Education Rank
        /// </summary>
        public SkillRank Pokemon_Rank { get; set; }

        /// <summary>
        ///     Technology Education Mod
        /// </summary>
        public int Technology_Mod { get; set; }

        /// <summary>
        ///     Technology Education Rank
        /// </summary>
        public SkillRank Technology_Rank { get; set; }

        /// <summary>
        ///     Guile Mod
        /// </summary>
        public int Guile_Mod { get; set; }

        /// <summary>
        ///     Guile Rank
        /// </summary>
        public SkillRank Guile_Rank { get; set; }

        /// <summary>
        ///     Perception Mod
        /// </summary>
        public int Perception_Mod { get; set; }

        /// <summary>
        ///     Perception Rank
        /// </summary>
        public SkillRank Perception_Rank { get; set; }

        #endregion

        #region Spirit

        /// <summary>
        /// </summary>
        public int Charm_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Charm_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Command_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Command_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Focus_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Focus_Rank { get; set; }

        /// <summary>
        /// </summary>
        public int Intuition_Mod { get; set; }

        /// <summary>
        /// </summary>
        public SkillRank Intuition_Rank { get; set; }

        #endregion

        #endregion
    }
}