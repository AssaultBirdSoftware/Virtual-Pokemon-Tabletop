using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Data
{
    /// <summary>
    /// 1xx -> All (State)
    /// 2xx -> All (Response)
    /// 3xx -> Errors
    /// 4xx -> Accessability
    /// 
    /// 399 -> Custom Error
    /// </summary>
    public enum ResponseCode
    {
        Processing = 100,
        OK = 200, Ready = 201, Avaliable = 202, Failed = 203,
        Error = 300, Not_Implemented = 301, Not_Avaliable = 302, RateLimitHit = 303, Custom_Error = 399,
        Forbiden = 403, Not_Found = 404,
    }

    public class NetworkCommand
    {
        public string Command { get; set; }
        public bool Waiting { get; }
        public string Waiting_Code { get; }
        public ResponseCode Response { get; set; }

        public object Data { get; set; }
    }

    public class Response
    {
        public ResponseCode Code { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
    }
}