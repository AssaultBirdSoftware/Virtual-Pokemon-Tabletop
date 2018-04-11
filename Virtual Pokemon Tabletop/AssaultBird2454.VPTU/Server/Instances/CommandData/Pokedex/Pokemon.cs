using AssaultBird2454.VPTU.Networking.Data;
using System.Collections.Generic;
using System.Drawing;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Pokemon_GetList : NetworkCommand
    {
        public string Command { get { return "Pokedex_Pokemon_GetList"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon_Dex { get; set; }
    }

    public class Pokedex_Pokemon : NetworkCommand
    {
        public string Command { get { return "Pokedex_Pokemon_Get"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public decimal DexID { get; set; }
        public VPTU.Pokedex.Pokemon.PokemonData PokemonData { get; set; }
    }
}
