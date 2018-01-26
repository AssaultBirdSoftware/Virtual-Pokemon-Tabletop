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

        /// <summary>
        /// Creates a new instance of a Base_Commands class for handeling command calls over the network
        /// </summary>
        public Base_Commands(ClientInstance _Instance)
        {
            Instance = _Instance;
        }

        /// <summary>
        /// Registers all base commands
        /// </summary>
        /// <param name="CommandHandeler">Command Handeler that the commands are being Registered to</param>
        public void Register_Commands(Networking.Client.Command_Handeler.Client_CommandHandeler CommandHandeler)
        {
            CommandHandeler.RegisterCommand<CommandData.Connection.Connect>("ConnectionState");

            #region Auth
            CommandHandeler.RegisterCommand<CommandData.Auth.Login>("Auth_Login");
            CommandHandeler.RegisterCommand<CommandData.Auth.Logout>("Auth_Logout");
            CommandHandeler.RegisterCommand<object>("Auth_Create");
            CommandHandeler.RegisterCommand<object>("Auth_Delete");
            CommandHandeler.RegisterCommand<object>("Auth_Edit");
            CommandHandeler.RegisterCommand<object>("Auth_List");
            #endregion

            #region Base
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Save");
            CommandHandeler.GetCommand("Base_SaveData_Save").Command_Executed += Base_Commands_Command_Executed;
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Load");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Upload");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_SaveData_Download");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_Settings_Get");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Base_Settings_Set");
            // CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("");
            #endregion

            #region Pokedex
            // All
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Pokedex_Get");

            // Pokemon
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon_GetList>("Pokedex_Pokemon_GetList");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Get");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Add");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Edit");
            CommandHandeler.RegisterCommand<CommandData.Pokedex.Pokedex_Pokemon>("Pokedex_Pokemon_Remove");

            // Moves
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Get");
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Add");
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Edit");
            CommandHandeler.RegisterCommand<string>("Pokedex_Moves_Remove");
            #endregion

            #region Entities
            CommandHandeler.RegisterCommand<CommandData.Entities.Entities_All_GetList>("Entities_All_GetList");

            CommandHandeler.RegisterCommand<string>("Entities_Pokemon_GetList");
            CommandHandeler.RegisterCommand<CommandData.Entities.Entities_Pokemon_Get>("Entities_Pokemon_Get");
            CommandHandeler.RegisterCommand<string>("Entities_Pokemon_Create");
            CommandHandeler.RegisterCommand<string>("Entities_Pokemon_Edit");
            CommandHandeler.RegisterCommand<string>("Entities_Pokemon_Delete");

            CommandHandeler.RegisterCommand<string>("Entities_Trainer_GetList");
            CommandHandeler.RegisterCommand<string>("Entities_Trainer_Get");
            CommandHandeler.RegisterCommand<string>("Entities_Trainer_Create");
            CommandHandeler.RegisterCommand<string>("Entities_Trainer_Edit");
            CommandHandeler.RegisterCommand<string>("Entities_Trainer_Delete");
            // CommandHandeler.RegisterCommand<string>("Entities_");
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



        private void Base_Commands_Command_Executed(object Data)
        {

        }

        public void Unregister_Commands(Networking.Client.Command_Handeler.Client_CommandHandeler CommandHandeler)
        {

        }
    }
}
