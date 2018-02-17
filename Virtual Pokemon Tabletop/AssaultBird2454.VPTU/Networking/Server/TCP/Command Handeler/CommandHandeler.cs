using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Server.Command_Handeler
{
    #region Exceptions
    /// <summary>
    /// Thrown when an attempt to change a property when the server is running
    /// </summary>
    public class CommandNameTakenException : Exception
    {
        public CommandNameTakenException(string Name) : base("The Command Name \"" + Name + "\" is taken... Use another name for that command") { }
    }
    /// <summary>
    /// Thrown when a user attempts to execute a command while they are rate limited for that command or network instance
    /// </summary>
    public class CommandRateLimitReachedException : Exception
    {
        public CommandRateLimitReachedException(string Name, TCP_ClientNode Client) : base("The Command \"" + Name + "\" has reached it's rate limit for client \"" + Client.ID) { }
    }
    #endregion
    #region Delegates
    public delegate void CommandEvent(string Command);
    public delegate void RateLimited(string Name, TCP_ClientNode Client);
    #endregion

    public class Server_CommandHandeler
    {
        /// <summary>
        /// An event that is fired when a command is registered
        /// </summary>
        public event CommandEvent CommandRegistered;

        /// <summary>
        /// An event that is fired when a command is unregistered
        /// </summary>
        public event CommandEvent CommandUnRegistered;

        public event RateLimiting RateLimitChanged_Event;

        public event RateLimited RateLimitHit_Event;

        private Dictionary<string, Command> Commands;
        private List<RateTracker> RateTracking;

        public Server_CommandHandeler()
        {
            Commands = new Dictionary<string, Command>();
            RateTracking = new List<RateTracker>();
        }

        /// <summary>
        /// Register a command
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="CommandName">The Name of the command</param>
        /// <param name="Callback">A an action or method to execute when this command is executed</param>
        /// <exception cref="CommandNameTakenException"/>
        public void RegisterCommand<T>(string CommandName)
        {
            if (HasCommandName(CommandName))
            {
                throw new CommandNameTakenException(CommandName);// Command with that name exists, Throw Exception
            }

            Command cmd = new Command(CommandName, typeof(T));
            cmd.RateLimitChanged_Event += new RateLimiting((Command, Enabled, Limit) =>
            {
                RateLimitChanged_Event?.Invoke(Command, Enabled, Limit);
            });
            Commands.Add(CommandName, cmd);// Add the command to the command list

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

        internal void InvokeCommand(dynamic CommandData, TCP_ClientNode node)
        {
            try
            {
                Command cmd = Commands.First(x => x.Key == CommandData.Command).Value;// Gets the command by searching
                RateTracker RateTracker = RateTracking.Find(x => x.ClientID == node.ID);

                if (RateTracker == null)
                {
                    RateTracker = new RateTracker() { ClientID = node.ID };
                    RateTracking.Add(RateTracker);
                }

                if (RateTracker.CommandExecutions(CommandData.Command) <= cmd.Rate_Limit && cmd.Rate_Enabled == true || cmd.Rate_Enabled == false)
                {
                    RateTracker.CommandExecuted(CommandData.Command);
                    cmd.Invoke(CommandData, node);
                }
                else
                {
                    RateLimitHit_Event?.Invoke(CommandData.Command, node);

                    if (CommandData.Waiting)
                    {
                        CommandData.Response = Networking.Data.ResponseCode.RateLimitHit;
                        node.Send(CommandData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class RateTracker
    {
        public static readonly DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0);

        public RateTracker()
        {
            Commands = new List<KeyValuePair<string, int>>();
        }

        public string ClientID;
        public List<KeyValuePair<string, int>> Commands;

        public int CommandExecutions(string Command)
        {
            TimeSpan span = DateTime.UtcNow - epochTime;
            if (Math.Floor(span.TotalMinutes / 5) != BlockID)
            {
                Commands.Clear();
                Commands.Add(new KeyValuePair<string, int>(Command, 0));
                BlockID = (int)Math.Floor(span.TotalMinutes / 5);
            }

            if (Commands.Find(x => x.Key == Command) == null)
                Commands.Add(new KeyValuePair<string, int>(Command, 0));

            return Commands.Find(x => x.Key == Command).Value;
        }
        public void CommandExecuted(string Command)
        {
            TimeSpan span = DateTime.UtcNow - epochTime;
            if (Math.Floor(span.TotalMinutes / 5) != BlockID)
            {
                Commands.Clear();
                Commands.Add(new KeyValuePair<string, int>(Command, 1));
                BlockID = (int)Math.Floor(span.TotalMinutes / 5);
            }
            else
            {
                KeyValuePair<string, int> cmd = Commands.First(x => x.Key == Command);
                cmd.Value = cmd.Value + 1;
            }
        }
        private int BlockID = 0;
    }

    public class KeyValuePair<TKey, TValue>
    {
        public KeyValuePair()
        {

        }
        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key;
        public TValue Value;
    }
}
