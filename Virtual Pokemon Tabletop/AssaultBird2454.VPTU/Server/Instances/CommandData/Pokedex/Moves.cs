using AssaultBird2454.VPTU.Networking.Data;
using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex
{
    public class Pokedex_Moves_Get : NetworkCommand
    {
        public string Command { get; set; }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public List<VPTU.Pokedex.Moves.MoveData> Move_Dex { get; set; }
    }
}