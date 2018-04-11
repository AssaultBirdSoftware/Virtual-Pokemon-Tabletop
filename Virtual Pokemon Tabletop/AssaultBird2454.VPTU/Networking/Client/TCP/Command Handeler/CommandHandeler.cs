﻿using AssaultBird2454.VPTU.Networking.Client.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.Command_Handeler
{
    #region Exceptions
    /// <summary>
    /// Thrown when an attempt to change a property when the server is running
    /// </summary>
    public class CommandNameTakenException : Exception
    {
        public CommandNameTakenException(string Name) : base("The Command Name \"" + Name + "\" is taken... Use another name for that command") { }
    }
    #endregion
    #region Delegates
    public delegate void CommandEvent(string Command);
    #endregion

    public class Client_CommandHandeler
    {
        /// <summary>
        /// An event that is fired when a command is registered
        /// </summary>
        public event CommandEvent CommandRegistered;
        /// <summary>
        /// An event that is fired when a command is unregistered
        /// </summary>
        public event CommandEvent CommandUnRegistered;

        private Dictionary<string, Command> Commands;

        public Client_CommandHandeler()
        {
            Commands = new Dictionary<string, Command>();
        }

        /// <summary>
        /// Register a command
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="CommandName">The Name of the command</param>
        /// <exception cref="CommandNameTakenException"/>
        public void RegisterCommand<T>(string CommandName)
        {
            if (HasCommandName(CommandName))
            {
                throw new CommandNameTakenException(CommandName);// Command with that name exists, Throw Exception
            }

            Commands.Add(CommandName, new Command(CommandName, typeof(T)));// Add the command to the command list
            CommandRegistered?.Invoke(CommandName);// Fire Event
        }

        /// <summary>
        /// Checks if the handeler has the command
        /// </summary>
        /// <param name="Name">The name of the command being checked</param>
        /// <returns>if the command exists</returns>
        public bool HasCommandName(string Name)
        {
            return Commands.ContainsKey(Name);// Gets the command by searching
        }

        public void DeleteCommand(string Name)
        {
            try
            {
                Commands.Remove(Name);// Removes the command
                CommandUnRegistered?.Invoke(Name);
            }
            catch { /* Does not exist, dont not matter */ }
        }

        public Command GetCommand(string Name)
        {
            return Commands.First(x => x.Key.ToLower() == Name.ToLower()).Value;
        }

        internal void InvokeCommand(TCP_Client Client, string Data)
        {
            try
            {
                var DataForm = new { Command = "" };

                var CommandName = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(Data, DataForm);// Deserializes an interface for command pharsing

                Command cmd = Commands.First(x => x.Key == CommandName.Command).Value;// Gets the command by searching
                var CommandData = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(Data, cmd.DataType);// Deserializes an interface for command pharsing

                if (((Data.NetworkCommand)CommandData).Response == Networking.Data.ResponseCode.Nothing)
                {
                    cmd.Invoke(CommandData, Client);
                }
                else
                {
                    Client.Awaiting_Callbacks.Find(x => x.Key == ((Data.NetworkCommand)CommandData).Waiting_Code).Value.Invoke(cmd);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
