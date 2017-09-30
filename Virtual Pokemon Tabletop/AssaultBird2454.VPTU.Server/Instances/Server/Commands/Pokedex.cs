using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.Server.Commands
{
    public class Pokedex
    {
        ServerInstance Instance;
        public Pokedex(ServerInstance _Instance)
        {
            Instance = _Instance;
        }

        #region Pokemon
        internal void Get_Pokedex_Pokemon(object obj, TCP_ClientNode Client)
        {
            Client.Send(new CommandData.Pokedex.Get_Pokedex_Pokemon() { Command = "Get_Pokedex_Pokemon", Pokemon_Dex = Instance.SaveManager.SaveData.PokedexData.Pokemon });
        }

        internal void Add_Pokedex_Pokemon(object obj, TCP_ClientNode Client)
        {

        }

        internal void Edit_Pokedex_Pokemon(object obj, TCP_ClientNode Client)
        {

        }

        internal void Remove_Pokedex_Pokemon(object obj, TCP_ClientNode Client)
        {

        }
        #endregion

        #region Moves
        internal void Get_Pokedex_Moves(object obj, TCP_ClientNode Client)
        {

        }

        internal void Add_Pokedex_Moves(object obj, TCP_ClientNode Client)
        {

        }

        internal void Edit_Pokedex_Moves(object obj, TCP_ClientNode Client)
        {

        }

        internal void Remove_Pokedex_Moves(object obj, TCP_ClientNode Client)
        {

        }
        #endregion
    }
}
