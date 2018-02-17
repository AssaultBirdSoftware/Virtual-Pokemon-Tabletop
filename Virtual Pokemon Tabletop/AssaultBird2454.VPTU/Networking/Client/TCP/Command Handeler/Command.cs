using AssaultBird2454.VPTU.Networking.Client.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.Command_Handeler
{
    public delegate Data.Response Command_Callback(object Data, bool Waiting);

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
        internal void Invoke(object Data, TCP_Client Client)
        {
            Data.Response response = Command_Executed?.Invoke(Data, ((Data.NetworkCommand)Data).Waiting);

            if (((Data.NetworkCommand)Data).Waiting)
            {
                ((Data.NetworkCommand)Data).Response = response.Code;
                Client.SendData(Data);
            }
        }
    }
}