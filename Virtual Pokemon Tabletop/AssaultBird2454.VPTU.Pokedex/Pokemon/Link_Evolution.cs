namespace AssaultBird2454.VPTU.Pokedex.Pokemon
{
    public enum Evolution_Type
    {
        Normal = 0,
        Mega,
        Primal,
        Other
    }

    public class Link_Evolutions
    {
        public decimal Pokemon_Evo { get; set; }

        public Evolution_Type Evo_Type { get; set; }
        //public int Evolution_LevelUp_Level { get; set; }

        //public bool Evolution_LevelUP { get; set; }

        //[Obsolete("Not Implemented", true)]
        //public List<string> Evolution_Items { get; set; }

        //[Obsolete("Not Implemented", true)]
        //public List<string> Evolution_Conditions { get; set; }
    }
}