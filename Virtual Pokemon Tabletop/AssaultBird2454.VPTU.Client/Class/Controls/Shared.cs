using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Client.Class.Controls
{
    public delegate void Button_Pressed();

    public enum Pokedex_Entry_Type { Pokemon, Move, Ability, Item, None }
    public delegate void Pokedex_Entry_Selection_Changed(Pokedex_Entry_Type type, object Data);
}
