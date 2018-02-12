using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Moves_Get : Networking.Data.NetworkCommand
    {
        public List<VPTU.Pokedex.Moves.MoveData> Move_Dex { get; set; }
    }
}