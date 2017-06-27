using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Server.TCP
{
    /// <summary>
    /// Thrown when an attempt to change a property when the server is running
    /// </summary>
    public class ServerAlreadyRunningException : Exception
    {
        public ServerAlreadyRunningException() : base() { }
    }
}
