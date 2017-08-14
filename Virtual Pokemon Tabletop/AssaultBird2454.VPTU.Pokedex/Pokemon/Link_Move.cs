using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Pokedex.Pokemon
{
    public class Link_Moves
    {
        public string MoveName { get; set; }
        public bool LevelUp_Move { get; set; }
        public bool Tutor_Move { get; set; }
        public bool Egg_Move { get; set; }
        public bool TMHM_Move { get; set; }

        public int LevelUp_Level { get; set; }
    }
}
