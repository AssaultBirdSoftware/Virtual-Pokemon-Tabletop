using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Pokedex.Save_Data
{
    /// <summary>
    /// A Save Data Class designed for pokedex related data
    /// </summary>
    public class Pokedex
    {
        /// <summary>
        /// Creates a new Pokedex Save Data Manager
        /// </summary>
        /// <param name="InitNewSave">Initialize new data</param>
        public Pokedex(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Moves = new List<Moves.MoveData>();// Initilises the MoveData
                Pokemon = new List<Pokemon.PokemonData>();// Initilises the PokemonData
                Abilitys = new List<Abilitys.AbilityData>();// Initilises the AbilityData
                Items = new List<Items.ItemData>();// Initilises the ItemData
            }
        }

        public void InitNullObjects()
        {
            if (Moves == null)
            {
                Moves = new List<Moves.MoveData>();// Initilises the MoveData
            }
            if (Pokemon == null)
            {
                Pokemon = new List<Pokemon.PokemonData>();// Initilises the PokemonData
            }
            if (Abilitys == null)
            {
                Abilitys = new List<Abilitys.AbilityData>();// Initilises the AbilityData
            }
            if (Items == null)
            {
                Items = new List<Items.ItemData>();// Initilises the ItemData
            }
        }

        /// <summary>
        /// All the Moves in the save file
        /// </summary>
        public List<Moves.MoveData> Moves;
        /// <summary>
        /// All the Pokemon in the save file
        /// </summary>
        public List<Pokemon.PokemonData> Pokemon;
        /// <summary>
        /// All the Abilitys in the save file
        /// </summary>
        public List<Abilitys.AbilityData> Abilitys;
        /// <summary>
        /// All the Items in the save file
        /// </summary>
        public List<Items.ItemData> Items;
    }
}
