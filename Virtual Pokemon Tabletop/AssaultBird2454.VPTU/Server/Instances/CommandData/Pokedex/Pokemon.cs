using System.Collections.Generic;
using System.Drawing;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Pokemon_GetList
    {
        public List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon_Dex { get; set; }
    }

    public class Pokedex_Pokemon
    {
        public decimal DexID { get; set; }
        public VPTU.Pokedex.Pokemon.PokemonData PokemonData { get; set; }
    }
}
