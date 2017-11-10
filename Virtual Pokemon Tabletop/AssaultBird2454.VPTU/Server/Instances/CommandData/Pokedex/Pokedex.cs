using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public enum Opperation { Add, Edit, Remove, Force_Update_All, Force_Update_Single }
    public class Pokedex_Pokemon : Networking.Data.NetworkCommand
    {
        public string Command { get; set; }

        public Opperation Opperation { get; set; }
    }
}
