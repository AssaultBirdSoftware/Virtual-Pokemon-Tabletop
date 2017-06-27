using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Client.TCP
{
    /// <summary>
    /// Thrown when an attempt to change a property when the client is connected
    /// </summary>
    public class ClientAlreadyRunningException : Exception
    {
        public ClientAlreadyRunningException() : base() { }
    }
}
