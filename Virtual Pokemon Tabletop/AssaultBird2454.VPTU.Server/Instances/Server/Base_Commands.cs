using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.Server
{
    public class Base_Commands
    {
        ServerInstance Instance;

        /// <summary>
        /// Creates a new instance of a Base_Commands class for handeling command calls over the network
        /// </summary>
        public Base_Commands(ServerInstance _Instance)
        {
            Instance = _Instance;
        }

        /// <summary>
        /// Registers all base commands
        /// </summary>
        /// <param name="CommandHandeler">Command Handeler that the commands are being Registered to</param>
        /// 
        public void Register_Commands(Networking.Server.Command_Handeler.Server_CommandHandeler CommandHandeler)
        {
            #region Pokedex
            // Pokemon
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Get_Pokedex_Pokemon>("Get_Pokedex_Pokemon");
            CommandHandeler.GetCommand("Get_Pokedex_Pokemon").Command_Executed += new Networking.Server.Command_Handeler.Command_Callback(
                (object Data, Networking.Server.TCP.TCP_ClientNode Client) => Client.Send(new CommandData.Pokedex.Get_Pokedex_Pokemon()
                {
                    Command = "Get_Pokedex_Pokemon",
                    Pokemon_Dex = Instance.SaveManager.SaveData.PokedexData.Pokemon
                }));

            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Add_Pokedex_Pokemon");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Edit_Pokedex_Pokemon");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Remove_Pokedex_Pokemon");

            // Moves
            CommandHandeler.RegisterCommand<string>("Get_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Add_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Edit_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Remove_Pokedex_Moves");
            #endregion
        }
        public void Unregister_Commands(Networking.Server.Command_Handeler.Server_CommandHandeler CommandHandeler)
        {

        }
    }
}
