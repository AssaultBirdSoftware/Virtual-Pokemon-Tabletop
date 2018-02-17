using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect.Action_Handeler
{
    #region Exceptions
    /// <summary>
    /// Thrown when an attempt to create an action was made when an action already exists with the name
    /// </summary>
    public class ActionNameTakenException : Exception
    {
        public ActionNameTakenException(string Name) : base("The Action's Name \"" + Name + "\" is taken... Use another name for that action") { }
    }
    #endregion
    #region Delegates
    public delegate void ActionEvent(string Command);
    #endregion

    public class ActionHandeler
    {
        /// <summary>
        /// An event that is fired when a command is registered
        /// </summary>
        public event ActionEvent ActionRegistered;

        /// <summary>
        /// An event that is fired when a command is unregistered
        /// </summary>
        public event ActionEvent ActionUnRegistered;

        private Dictionary<string, Action> Actions;

        public ActionHandeler()
        {
            Actions = new Dictionary<string, Action>();
        }

        /// <summary>
        /// Register an action
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="CommandName">The Name of the command</param>
        /// <param name="Callback">A an action or method to execute when this command is executed</param>
        /// <exception cref="CommandNameTakenException"/>
        public void RegisterAction<T>(string CommandName)
        {
            if (HasActionName(CommandName))
            {
                throw new ActionNameTakenException(CommandName);// Command with that name exists, Throw Exception
            }

            Action cmd = new Action(CommandName, typeof(T));
            Actions.Add(CommandName, cmd);// Add the command to the command list

            ActionRegistered?.Invoke(CommandName);// Fire Event
        }

        /// <summary>
        /// Checks if the handeler has the command
        /// </summary>
        /// <param name="Name">The name of the command being checked</param>
        /// <returns>if the command exists</returns>
        public bool HasActionName(string Name)
        {
            return Actions.ContainsKey(Name);// Gets the command by searching
        }

        public void DeleteAction(string Name)
        {
            try
            {
                Actions.Remove(Name);// Removes the command
                ActionUnRegistered?.Invoke(Name);
            }
            catch { /* Does not exist, dont not matter */ }
        }

        public Action GetAction(string Name)
        {
            return Actions.First(x => x.Key.ToLower() == Name.ToLower()).Value;
        }

        internal void InvokeAction(/* Add Variables to allow for actions to execute correctly */)
        {
            try
            {
                var DataForm = new { Command = "" };

                var ActionData = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType("", DataForm);// Deserializes an interface for command pharsing

                Action cmd = Actions.First(x => x.Key == ActionData.Command).Value;// Gets the command by searching
                cmd.Invoke(/* Add Variables to allow for actions to execute correctly */);
            }
            catch (Exception ex)
            {
                 
            }
        }
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
