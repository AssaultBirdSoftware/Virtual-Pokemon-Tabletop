using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    public enum Commands { Data = 0, Response = 1, SetBufferSize = 2, Enable_SSL = 3, Dissable_SSL = 4 }
    public enum ResponseCode { OK = 0, Not_Avaliable = 503, Forbiden = 403, Not_Found = 404, Error = 500 }

    public class NetworkData_Data
    {
        public NetworkData_Data(Commands _Command, ResponseCode _Response, object _Data = null)
        {
            Command = _Command;
            Response = _Response;
            Data = _Data;
        }

        public Commands Command { get; set; }
        public ResponseCode Response { get; set; }
        public object Data { get; set; }
    }
}
