using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    internal enum ResponseCode { OK = 0, Ready = 1, None = 2, Avaliable = 100, Not_Implemented = 501, Not_Avaliable = 503, Forbiden = 403, Not_Found = 404, Error = 500 }

    public interface NetworkCommand
    {
        string Command { get; }
    }
}
