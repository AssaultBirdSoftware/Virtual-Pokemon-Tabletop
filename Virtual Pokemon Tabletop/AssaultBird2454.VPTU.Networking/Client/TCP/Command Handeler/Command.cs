using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.Command_Handeler
{
    public delegate void Command_Callback(object Data);

    public class Command
    {
        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        public Command(string _Name, Type T)
        {
            Name = _Name;// Command Name
            DataType = T;// Command Data type
        }

        public string Name { get; set; }// Command Name
        public Type DataType { get; set; }// Command Data Type
        public event Command_Callback Command_Executed;

        /// <summary>
        /// Invokes the callback
        /// </summary>
        /// <param name="Data">The Data to send the callback</param>
        internal void Invoke(object Data)
        {
            Command_Executed?.Invoke(Data);// Invokes the callback
        }
    }
}