using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Pokedex.Moves
{
    public class Move_RangeData
    {
        public Move_RangeData(BattleManager.Data.Move_Range _Range, bool _Enabled, int _Size, int _Distance)
        {
            Range = _Range;
            Enabled = _Enabled;
            Distance = _Distance;
            Size = _Size;
        }

        public BattleManager.Data.Move_Range Range { get; set; }
        public bool Enabled { get; set; }
        public int Distance { get; set; }
        public int Size { get; set; }
    }
}
