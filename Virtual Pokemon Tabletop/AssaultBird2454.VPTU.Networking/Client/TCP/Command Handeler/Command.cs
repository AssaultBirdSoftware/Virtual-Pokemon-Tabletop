using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.Command_Handeler
{
    public class Command
    {
        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        /// <param name="_Callback">Callback</param>
        public Command(string _Name, Type T, Action<object> _Callback)
        {
            Name = _Name;// Command Name
            DataType = T;// Command Data type
            Callback = _Callback;// Callback
        }

        public string Name { get; set; }// Command Name
        public Type DataType { get; set; }// Command Data Type
        private Action<object> Callback { get; set; }// Callback

        /// <summary>
        /// Invokes the callback
        /// </summary>
        /// <param name="Data">The Data to send the callback</param>
        internal void Invoke(object Data)
        {
            Callback.Invoke(Data);// Invokes the callback
        }
    }
}