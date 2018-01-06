using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Networking.Server.Command_Handeler
{
    #region Delegates
    public delegate void Command_Callback(object Data, TCP_ClientNode Client);
    #endregion

    public class Command
    {

        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        public Command(string _Name, Type T)
        {
            Name = _Name;
            DataType = T;
        }
        public bool Rate_Enable = false;
        public int Rate_Limit = 180;

        public string Name { get; set; }// Command Name
        public Type DataType { get; set; }// Command Data Type
        public event Command_Callback Command_Executed;// Event invoked when the command is executed

        internal void Invoke(object Data, TCP_ClientNode Client)
        {
            Command_Executed?.Invoke(Data, Client);
        }
    }
}