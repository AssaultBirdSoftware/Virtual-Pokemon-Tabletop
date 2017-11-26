using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssaultBird2454.VPTU.Networking.Server.TCP;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex;

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
            #region Auth
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Auth_Login");

            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Auth_Create");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Auth_Delete");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Auth_Edit");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Auth_List");
            #endregion

            #region Base
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Save");
            CommandHandeler.GetCommand("Base_SaveData_Save").Command_Executed += Base_SaveData_Save_Executed;

            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Load");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Upload");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Download");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_Settings_Get");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_Settings_Set");
            // CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_Get>("");
            #endregion

            #region Permissions (Not Implemented)

            #endregion

            #region Pokedex
            // All
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Pokedex_Get");
            CommandHandeler.GetCommand("Pokedex_Get").Command_Executed += Pokedex_Get_Executed;

            // Pokemon
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Pokedex_Pokemon_GetList");
            CommandHandeler.GetCommand("Pokedex_Pokemon_GetList").Command_Executed += Pokedex_Pokemon_GetList_Executed;
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Get");
            CommandHandeler.GetCommand("Pokedex_Pokemon_Get").Command_Executed += Pokedex_Pokemon_Get_Executed;

            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Add");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Edit");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Remove");

            // Moves
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Get");
            CommandHandeler.GetCommand("Pokedex_Moves_Get").Command_Executed += Pokedex_Moves_Get_Executed;

            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Add");
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Edit");
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Remove");
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
            CommandHandeler.RegisterCommand<CommandData.Resources.ImageResource>("Resources_Image_Get");// Gets an Image Resource
            CommandHandeler.GetCommand("Resources_Image_Get").Command_Executed += Resources_Image_Get_Executed;

            CommandHandeler.RegisterCommand<CommandData.Resources.ImageResource>("Resources_Image_Add");// Adds an Image Resource
            CommandHandeler.RegisterCommand<CommandData.Resources.ImageResource>("Resources_Image_Edit");// Edits and Image Resource
            CommandHandeler.RegisterCommand<CommandData.Resources.ImageResource>("Resources_Image_Remove");// Removes and Image Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Play");// Play Audio Signal
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Get");// Gets an Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Add");// Adds an Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Edit");// Edits and Audio Resource
            CommandHandeler.RegisterCommand<string>("Resources_Audio_Remove");// Removes an Audio Resource
            // CommandHandeler.RegisterCommand<string>("Resources_");
            #endregion
        }

        #region Callbacks
        #region Base Commands
        private void Base_SaveData_Save_Executed(object Data, Networking.Server.TCP.TCP_ClientNode Client)
        {
            Instance.SaveManager.Save_SaveData();
            Client.Send(new Instances.CommandData.SaveData.Save() { State = CommandData.SaveData.SaveStates.Save_Passed });
        }
        #endregion

        #region Pokedex
        #region All
        private void Pokedex_Get_Executed(object Data, Networking.Server.TCP.TCP_ClientNode Client)
        {
            List<Pokedex.Pokemon.PokemonData> PokedexData = new List<Pokedex.Pokemon.PokemonData>();

            foreach (Pokedex.Pokemon.PokemonData pokemon in Instance.SaveManager.SaveData.PokedexData.Pokemon)
            {
                // Permissions Checks Here
                PokedexData.Add(pokemon);
            }

            Client.Send(new CommandData.Pokedex.Pokedex_Pokemon_GetList()
            {
                Command = "Pokedex_Get",
                Pokemon_Dex = PokedexData
            });
        }
        #endregion

        #region Pokemon
        private void Pokedex_Pokemon_GetList_Executed(object Data, TCP_ClientNode Client)
        {
            List<Pokedex.Pokemon.PokemonData> PokedexData = new List<Pokedex.Pokemon.PokemonData>();

            foreach (Pokedex.Pokemon.PokemonData pokemon in Instance.SaveManager.SaveData.PokedexData.Pokemon)
            {
                // Permissions Checks Here
                PokedexData.Add(pokemon);
            }

            Client.Send(new CommandData.Pokedex.Pokedex_Pokemon_GetList()
            {
                Command = "Pokedex_Pokemon_GetList",
                Pokemon_Dex = PokedexData
            });
        }
        private void Pokedex_Pokemon_Get_Executed(object Data, TCP_ClientNode Client)
        {
            Pokedex.Pokemon.PokemonData PokemonData = Instance.SaveManager.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == ((Pokedex_Pokemon)Data).DexID);

            Client.Send(new Pokedex_Pokemon()
            {
                Command = "Pokedex_Pokemon_Get",
                PokemonData = PokemonData,
                DexID = PokemonData.Species_DexID
            });
        }
        #endregion

        #region Moves
        private void Pokedex_Moves_Get_Executed(object Data, TCP_ClientNode Client)
        {
            List<Pokedex.Moves.MoveData> MoveData = new List<Pokedex.Moves.MoveData>();

            foreach (Pokedex.Moves.MoveData move in Instance.SaveManager.SaveData.PokedexData.Moves)
            {
                // Permissions Checks Here
                MoveData.Add(move);
            }

            Client.Send(new CommandData.Pokedex.Pokedex_Moves_Get()
            {
                Command = "Pokedex_Moves_Get",
                Move_Dex = MoveData
            });
        }
        #endregion
        #endregion

        #region Resources
        private void Resources_Image_Get_Executed(object Data, TCP_ClientNode Client)
        {
            CommandData.Resources.ImageResource IRD = (CommandData.Resources.ImageResource)Data;
            IRD.Image = Instance.SaveManager.LoadImage(IRD.Location);

            Client.Send(IRD);
        }
        #endregion
        #endregion

        public void Unregister_Commands(Networking.Server.Command_Handeler.Server_CommandHandeler CommandHandeler)
        {

        }
    }
}
