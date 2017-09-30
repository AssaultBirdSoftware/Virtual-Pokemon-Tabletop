using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.Client
{
    public class Base_Commands
    {
        ClientInstance Instance;
        #region Command Classes
        Commands.Pokedex Pokedex;// Pokedex
        #endregion

        /// <summary>
        /// Creates a new instance of a Base_Commands class for handeling command calls over the network
        /// </summary>
        public Base_Commands(ClientInstance _Instance)
        {
            Instance = _Instance;

            Pokedex = new Commands.Pokedex(Instance);
        }

        /// <summary>
        /// Registers all base commands
        /// </summary>
        /// <param name="CommandHandeler">Command Handeler that the commands are being Registered to</param>
        public void Register_Commands(Networking.Client.Command_Handeler.Client_CommandHandeler CommandHandeler)
        {
            #region Pokedex
            // Pokemon
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Get_Pokedex_Pokemon>("Get_Pokedex_Pokemon", Pokedex.Get_Pokedex_Pokemon);
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Add_Pokedex_Pokemon", Pokedex.Add_Pokedex_Pokemon);
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Edit_Pokedex_Pokemon", Pokedex.Edit_Pokedex_Pokemon);
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Remove_Pokedex_Pokemon", Pokedex.Remove_Pokedex_Pokemon);

            // Moves
            CommandHandeler.RegisterCommand<string>("Get_Pokedex_Moves", Pokedex.Get_Pokedex_Moves);
            CommandHandeler.RegisterCommand<string>("Add_Pokedex_Moves", Pokedex.Add_Pokedex_Moves);
            CommandHandeler.RegisterCommand<string>("Edit_Pokedex_Moves", Pokedex.Edit_Pokedex_Moves);
            CommandHandeler.RegisterCommand<string>("Remove_Pokedex_Moves", Pokedex.Remove_Pokedex_Moves);
            #endregion
        }
        public void Unregister_Commands(Networking.Client.Command_Handeler.Client_CommandHandeler CommandHandeler)
        {

        }
    }
}
