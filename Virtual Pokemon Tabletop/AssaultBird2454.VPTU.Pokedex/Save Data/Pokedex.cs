using System.Collections.Generic;
using AssaultBird2454.VPTU.Pokedex.Abilitys;
using AssaultBird2454.VPTU.Pokedex.Items;
using AssaultBird2454.VPTU.Pokedex.Moves;
using AssaultBird2454.VPTU.Pokedex.Pokemon;

namespace AssaultBird2454.VPTU.Pokedex.Save_Data
{
    /// <summary>
    ///     A Save Data Class designed for pokedex related data
    /// </summary>
    public class Pokedex
    {
        /// <summary>
        ///     All the Abilitys in the save file
        /// </summary>
        public List<AbilityData> Abilitys;

        /// <summary>
        ///     All the Items in the save file
        /// </summary>
        public List<ItemData> Items;

        /// <summary>
        ///     All the Moves in the save file
        /// </summary>
        public List<MoveData> Moves;

        /// <summary>
        ///     All the Pokemon in the save file
        /// </summary>
        public List<PokemonData> Pokemon;

        /// <summary>
        ///     Creates a new Pokedex Save Data Manager
        /// </summary>
        /// <param name="InitNewSave">Initialize new data</param>
        public Pokedex(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Moves = new List<MoveData>(); // Initilises the MoveData
                Pokemon = new List<PokemonData>(); // Initilises the PokemonData
                Abilitys = new List<AbilityData>(); // Initilises the AbilityData
                Items = new List<ItemData>(); // Initilises the ItemData
            }
        }

        public void InitNullObjects()
        {
            if (Moves == null)
                Moves = new List<MoveData>(); // Initilises the MoveData
            if (Pokemon == null)
                Pokemon = new List<PokemonData>(); // Initilises the PokemonData
            if (Abilitys == null)
                Abilitys = new List<AbilityData>(); // Initilises the AbilityData
            if (Items == null)
                Items = new List<ItemData>(); // Initilises the ItemData
        }
    }
}