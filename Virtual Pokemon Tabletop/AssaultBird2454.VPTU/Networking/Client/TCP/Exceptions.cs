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

    /// <summary>
    /// Thrown when an attempt to send invalid network data was made
    /// </summary>
    public class NotNetworkDataException : Exception
    {
        public NotNetworkDataException() : base("The object you tried to send does not extend \"AssaultBird2454.VPTU.Networking.Data.NetworkCommand\"") { }
    }
}
