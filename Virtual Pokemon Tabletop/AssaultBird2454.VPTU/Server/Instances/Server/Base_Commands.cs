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
                (object Data, Networking.Server.TCP.TCP_ClientNode Client) =>
                {
                    List<Pokedex.Pokemon.PokemonData> PokedexData = new List<Pokedex.Pokemon.PokemonData>();

                    foreach (Pokedex.Pokemon.PokemonData pokemon in Instance.SaveManager.SaveData.PokedexData.Pokemon)
                    {
                        // Permissions Checks Here
                        PokedexData.Add(pokemon);
                    }

                    Client.Send(new CommandData.Pokedex.Get_Pokedex_Pokemon()
                    {
                        Command = "Get_Pokedex_Pokemon",
                        Pokemon_Dex = PokedexData
                    });
                });

            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Add_Pokedex_Pokemon");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Edit_Pokedex_Pokemon");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Remove_Pokedex_Pokemon");

            // Moves
            CommandHandeler.RegisterCommand<string>("Get_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Add_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Edit_Pokedex_Moves");
            CommandHandeler.RegisterCommand<string>("Remove_Pokedex_Moves");
            #endregion

            #region Entity
            CommandHandeler.RegisterCommand<string>("Entity_Pokemon_Get");
            CommandHandeler.RegisterCommand<string>("Entity_Pokemon_Create");
            CommandHandeler.RegisterCommand<string>("Entity_Pokemon_Edit");
            CommandHandeler.RegisterCommand<string>("Entity_Pokemon_Delete");
            CommandHandeler.RegisterCommand<string>("Entity_Trainer_Get");
            CommandHandeler.RegisterCommand<string>("Entity_Trainer_Create");
            CommandHandeler.RegisterCommand<string>("Entity_Trainer_Edit");
            CommandHandeler.RegisterCommand<string>("Entity_Trainer_Delete");
            // CommandHandeler.RegisterCommand<string>("Entity_");
            #endregion

            #region Battles
            // Battles
            CommandHandeler.RegisterCommand<string>("Battle_Participants_Add");// Adds Participants to the Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_Participants_Edit");// Edits a participant in the Battle Instnace
            CommandHandeler.RegisterCommand<string>("Battle_Participants_Remove");// Removes a participant from the Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_Participants_Get");// Gets the participants in the Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_TurnOrder_Get");// Gets the Turn Order of this Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_TurnOrder_Current");// Gets the current turn in the Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_TurnOrder_Next");// Next Turn in the Battle Instance Turn Order
            CommandHandeler.RegisterCommand<string>("Battle_TurnOrder_Prev");// Prev Turn in the Battle Instance Turn Order
            CommandHandeler.RegisterCommand<string>("Battle_Action_Execute");// Executes a Battle Action
            CommandHandeler.RegisterCommand<string>("Battle_Action_Interupt");// Executes a Battle Interupt
            CommandHandeler.RegisterCommand<string>("Battle_Instance_Start");// Starts the Battle Instance
            CommandHandeler.RegisterCommand<string>("Battle_Instance_End");// Ends the Battle Instance
            // CommandHandeler.RegisterCommand<string>("Battle_");
            #endregion

            #region Resources
            CommandHandeler.RegisterCommand<string>("Resources_Image_Get");// Gets an Image Resource
            CommandHandeler.RegisterCommand<string>("Resources_Image_Add");// Adds an Image Resource
            CommandHandeler.RegisterCommand<string>("Resources_Image_Edit");// Edits and Image Resource
            CommandHandeler.RegisterCommand<string>("Resources_Image_Remove");// Removes and Image Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Play");// Play Audio Signal
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Get");// Gets an Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Add");// Adds an Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Edit");// Edits and Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Remove");// Removes an Audio Resource
            // CommandHandeler.RegisterCommand<string>("Resources_");
            #endregion
        }
        public void Unregister_Commands(Networking.Server.Command_Handeler.Server_CommandHandeler CommandHandeler)
        {

        }
    }
}
