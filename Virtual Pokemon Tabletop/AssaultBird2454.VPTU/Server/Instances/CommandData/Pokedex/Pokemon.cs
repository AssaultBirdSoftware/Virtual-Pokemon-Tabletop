using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Pokemon_Get : Networking.Data.NetworkCommand
    {
        public Pokedex_Pokemon_Get()
        {
            Command = "Pokedex_Pokemon_Get";
        }

        public string Command { get; set; }

        public List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon_Dex { get; set; }
    }

    public class Pokedex_Pokemon : Networking.Data.NetworkCommand
    {
        public string Command { get; set; }

        public Opperation Opperation { get; set; }
    }
}
