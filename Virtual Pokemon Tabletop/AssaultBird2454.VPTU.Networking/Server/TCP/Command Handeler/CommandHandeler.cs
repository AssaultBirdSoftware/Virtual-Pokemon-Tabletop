using System;
using System.Collections.Generic;
using System.Linq;
using AssaultBird2454.VPTU.Networking.Server.TCP;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.Networking.Server.Command_Handeler
{
    #region Exceptions

    /// <summary>
    ///     Thrown when an attempt to change a property when the server is running
    /// </summary>
    public class CommandNameTakenException : Exception
    {
        public CommandNameTakenException(string Name) : base("The Command Name \"" + Name +
                                                             "\" is taken... Use another name for that command")
        {
        }
    }

    #endregion

    public class Server_CommandHandeler
    {
        private readonly Dictionary<string, object> Commands;

        public Server_CommandHandeler()
        {
            Commands = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Register a command
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="CommandName">The Name of the command</param>
        /// <param name="Callback">A an action or method to execute when this command is executed</param>
        /// <exception cref="CommandNameTakenException" />
        public void RegisterCommand<T>(string CommandName, Action<object, TcpClientNode> Callback)
        {
            if (HasCommandName(CommandName))
                throw new CommandNameTakenException(CommandName); // Command with that name exists, Throw Exception

            Commands.Add(CommandName,
                new Command(CommandName, typeof(T), Callback)); // Add the command to the command list
        }

        /// <summary>
        ///     Checks if the handeler has the command
        /// </summary>
        /// <param name="Name">The name of the command being checked</param>
        /// <returns>if the command exists</returns>
        public bool HasCommandName(string Name)
        {
            return Commands.ContainsKey(Name); // Gets the command by searching
        }

        public void DeleteCommand(string Name)
        {
            try
            {
                Commands.Remove(Name); // Removes the command
            }
            catch
            {
                /* Does not exist, dont not matter */
            }
        }

        internal void InvokeCommand(string Data, TcpClientNode node)
        {
            var DataForm = new {Command = ""};

            var CommandData =
                JsonConvert.DeserializeAnonymousType(Data, DataForm); // Deserializes an interface for command pharsing

            var cmd = (Command) Commands.First(x => x.Key == CommandData.Command)
                .Value; // Gets the command by searching

            cmd.Invoke(JsonConvert.DeserializeObject(Data, cmd.DataType), node);
        }
    }
}