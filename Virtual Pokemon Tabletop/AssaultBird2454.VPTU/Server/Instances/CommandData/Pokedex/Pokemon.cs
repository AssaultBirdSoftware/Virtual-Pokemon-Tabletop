using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Pokemon_GetList : Networking.Data.NetworkCommand
    {
        public Pokedex_Pokemon_GetList()
        {
            Command = "Pokedex_Pokemon_GetList";
        }

        public string Command { get; set; }

        public List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon_Dex { get; set; }
    }

    public class Pokedex_Pokemon : Networking.Data.NetworkCommand
    {
        public string Command { get; set; }

        public decimal DexID { get; set; }
        public VPTU.Pokedex.Pokemon.PokemonData PokemonData { get; set; }
    }
}
