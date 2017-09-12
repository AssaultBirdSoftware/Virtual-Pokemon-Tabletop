using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Data
{
    /// <summary>
    /// Defines the natures that a pokemon can have
    /// </summary>
    public enum Nature { Cuddly = 1, Distracted, Proud, Decisive, Patient, Desperate, Lonely, Adamant, Naughty, Brave, Stark, Bold, Impish, Lax, Relaxed, Curious, Modest, Mild, Rash, Quiet, Dreamy, Calm, Gentle, Careful, Sassy, Skittish, Timid, Hasty, Jolly, Naive, Composed, Hardy, Docile, Bashful, Quirky, Serious }
    /// <summary>
    /// Defines Pokemon & Move Types
    /// </summary>
    public enum Type { Normal = 1, Fire, Fighting, Water, Flying, Grass, Poison, Electric, Ground, Psychic, Rock, Ice, Bug, Dragon, Ghost, Dark, Steel, Fairy }
    /// <summary>
    /// Defines all the different status conditions
    /// </summary>
    public enum Status_Afflictions { Burned = 1, Frozen, Paralysis, Poision, Sleep, BadSleep, Suppressed, Confusion, Cursed, Dissabled, Rage, Flinch, Infatuation, Fainted, Slowed, Stuck, Blindness, TotalBlindness, Trapped, Tripped, Vulnerable, Vortex, TemporaryHitPoints }
    /// <summary>
    /// Defines Move Classes
    /// </summary>
    public enum MoveClass { Status, Physical, Special, Static }
    public enum Action_Type { Standard_Action, Shift_Action, Swift_Action, Free_Action, Extended_Action, Full_Action, F_Priority_Action, L_Priority_Action, Interrupt_Action }

    /// <summary>
    /// Defines the possible Ranges that a move can have
    /// </summary>
    public enum Move_Range { Melee, Line, Range, Range_Blast, Cone, Close_Blast, Self, Burst, Field, Cardinally_Adjacent, All_Adjacent_Foes, Any }
    /// <summary>
    /// Defines DamageBases used to identify what Set or Rolled Damage Formula to use
    /// </summary>
    public enum DamageBase { DB0, DB1, DB2, DB3, DB4, DB5, DB6, DB7, DB8, DB9, DB10, DB11, DB12, DB13, DB14, DB15, DB16, DB17, DB18, DB19, DB20, DB21, DB22, DB23, DB24 }
    /// <summary>
    /// Defines the possible frequency that a move can be used at
    /// </summary>
    public enum Move_Frequency { At_Will, EOT, Scene, Daily, Static }
    /// <summary>
    /// Defines 
    /// </summary>
    public enum Move_KeyWords
    {
        Aura = 1, Berry, Blessing, Coat, Dash, DubleStrike, Environ, Execute, Exhaust, Fling, Friendly, Illusion, FiveStrike, Interupt, Pass,
        Groundsource, Hazard, Pledge, Shield, Smite, Social, Powder, Priority, Sonic, SpiritSurge, Trigger, Vortex, Push, Weather, Reaction, Recoil, SetUp, Hail, Rainy,
        SandStorm, Sunny, WeightClass
    }

    /// <summary>
    /// This enum contains an object for all skills in the game.
    /// </summary>
    public enum Skills
    {
        Body_Acrobatics, Body_Athletics, Body_Combat, Body_Intimidate, Body_Stealth, Body_Survival,
        Mind_GeneralEducation, Mind_MedicineEducation, Mind_OccultEducation, Mind_PokémonEducation, Mind_TechnologyEducation, Mind_Guile, Mind_Perception,
        Spirit_Charm, Spirit_Command, Spirit_Focus, Spirit_Intuition, Spirit_Edges
    }

    public enum NatureWalk_Type { Grassland, Forest, Wetlands, Ocean, Tundra, Mountain, Cave, Urban, Desert }
}
