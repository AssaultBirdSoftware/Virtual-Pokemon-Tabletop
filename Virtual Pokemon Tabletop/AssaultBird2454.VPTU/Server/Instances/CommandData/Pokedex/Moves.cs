using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Moves_Get : Networking.Data.NetworkCommand
    {
        public Pokedex_Moves_Get()
        {
            Command = "Pokedex_Moves_Get";
        }

        public string Command { get; set; }

        public List<VPTU.Pokedex.Moves.MoveData> Move_Dex { get; set; }
    }
}