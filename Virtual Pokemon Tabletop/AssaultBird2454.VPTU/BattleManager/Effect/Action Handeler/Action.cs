using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;

namespace AssaultBird2454.VPTU.BattleManager.Effect.Action_Handeler
{
    #region Delegates
    public delegate void Action_Callback();
    #endregion

    public class Action
    {
        public event Action_Callback Action_Executed;// Event invoked when the command is executed

        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        public Action(string _Name, Type T)
        {
            Name = _Name;
            DataType = T;
        }

        public string Name { get; set; }// Command Name
        public Type DataType { get; set; }// Command Data Type

        internal void Invoke()
        {
            Action_Executed?.Invoke();
        }
    }
}